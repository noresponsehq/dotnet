namespace NoResponse.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using NoResponse.LateResults;
    using NoResponse.Models.Shared;
    using NoResponse.Webhooks;
    using Xunit;

    public class LateResultsTests
    {
        private const string Secret = "late-result-secret";
        private const string Key = "whk_sdktest01";
        private const long NowSeconds = 1791968402;
        private static readonly DateTimeOffset Now = DateTimeOffset.FromUnixTimeSeconds(NowSeconds);
        private static readonly Dictionary<string, string> Secrets = new() { [Key] = Secret };

        private static readonly JObject StepUp = (JObject)WebhookTests.Fixture("valid/webhook-assessment-updated.json")["data"]!;
        private static readonly JObject Review = WithDecision(StepUp, "review");
        private static readonly JObject Pending = WebhookTests.Fixture("valid/assessment-view-pending.json");
        private static readonly JObject Expired = WebhookTests.Fixture("valid/webhook-assessment-expired.json");

        private static LateResultOptions Options(IReadOnlyDictionary<string, LateResultAction>? policy = null, Func<string, WebhookEvent, string?>? userId = null, DateTimeOffset? now = null) =>
            new() { Now = now ?? Now, Policy = policy, UserId = userId };

        private static (byte[] Raw, List<KeyValuePair<string, IEnumerable<string>>> Headers) Delivery(JObject data, string? accountId = null, bool test = false)
        {
            var copy = (JObject)data.DeepClone();
            if (accountId is not null) copy["detail"]!["account_id"] = accountId;
            var body = new JObject { ["id"] = "evt_01JA2ZB4N5P6Q7R8S9T0VWXYZ2", ["type"] = "assessment.updated", ["created_at"] = "2026-10-18T11:00:04Z", ["data"] = copy };
            if (test) body["test"] = true;
            var raw = Encoding.UTF8.GetBytes(body.ToString(Newtonsoft.Json.Formatting.None));
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(Secret));
            var digest = Convert.ToHexString(hmac.ComputeHash(Encoding.ASCII.GetBytes(NowSeconds + ".").Concat(raw).ToArray())).ToLowerInvariant();
            var headers = new Dictionary<string, string>
            {
                ["NR-Webhook-Id"] = "evt_01JA2ZB4N5P6Q7R8S9T0VWXYZ2",
                ["NR-Webhook-Timestamp"] = NowSeconds.ToString(),
                ["NR-Webhook-Key-Id"] = Key,
                ["NR-Webhook-Signature"] = "v1=" + digest,
            };
            return (raw, headers.Select(h => new KeyValuePair<string, IEnumerable<string>>(h.Key, new[] { h.Value })).ToList());
        }

        private static WebhookEvent Event(JObject data)
        {
            var (raw, headers) = Delivery(data);
            return Webhook.ConstructEvent(raw, headers, Secrets, now: Now);
        }

        private static JObject WithDecision(JObject assessment, string decision)
        {
            var data = (JObject)assessment.DeepClone();
            data["account"]!["risk"]!["decision"] = decision;
            foreach (var flow in new[] { "signup", "login", "purchase" }) data["account"]!["decisions_by_flow"]![flow] = decision;
            return data;
        }

        private static WebhookEvent Served(string status, string? decision)
        {
            var data = decision is null ? (JObject)StepUp.DeepClone() : WithDecision(StepUp, decision);
            data["status"] = status;
            data["detail"]!["status"] = status;
            if (decision is null) data.Remove("account");
            return Event(data);
        }

        private sealed record Call(string Method, string Path, Dictionary<string, string> Query, string? Auth, string? Accept, string? ContentType, string? Body);

        /// <summary>A local HTTP server on 127.0.0.1 standing in for a provider's admin API. It records each request and answers from Route.</summary>
        private sealed class MockProvider : IDisposable
        {
            private readonly HttpListener listener = new();
            private readonly Func<Call, (int Status, string? Body)?> route;
            private readonly Task loop;
            public List<Call> Calls { get; } = new();
            public string Url { get; }

            public MockProvider(Func<Call, (int Status, string? Body)?> route)
            {
                this.route = route;
                var probe = new TcpListener(IPAddress.Loopback, 0);
                probe.Start();
                var port = ((IPEndPoint)probe.LocalEndpoint).Port;
                probe.Stop();
                Url = $"http://127.0.0.1:{port}";
                listener.Prefixes.Add(Url + "/");
                listener.Start();
                loop = Task.Run(Serve);
            }

            private async Task Serve()
            {
                while (listener.IsListening)
                {
                    HttpListenerContext context;
                    try { context = await listener.GetContextAsync(); }
                    catch (Exception) { return; }
                    var request = context.Request;
                    string? body = null;
                    if (request.HasEntityBody)
                    {
                        using var reader = new StreamReader(request.InputStream, request.ContentEncoding);
                        body = await reader.ReadToEndAsync();
                    }
                    var raw = request.RawUrl!;
                    var split = raw.IndexOf('?');
                    var path = split < 0 ? raw : raw.Substring(0, split);
                    var query = new Dictionary<string, string>();
                    if (split >= 0)
                    {
                        foreach (var pair in raw.Substring(split + 1).Split('&', StringSplitOptions.RemoveEmptyEntries))
                        {
                            var eq = pair.IndexOf('=');
                            query[Uri.UnescapeDataString(pair.Substring(0, eq))] = Uri.UnescapeDataString(pair.Substring(eq + 1));
                        }
                    }
                    var call = new Call(request.HttpMethod, path, query, request.Headers["Authorization"], request.Headers["Accept"], request.ContentType, body);
                    lock (Calls) Calls.Add(call);
                    var (status, text) = route(call) ?? (404, "{\"error\":\"not found\"}");
                    context.Response.StatusCode = status;
                    context.Response.ContentType = "application/json";
                    var bytes = Encoding.UTF8.GetBytes(text ?? "");
                    context.Response.ContentLength64 = bytes.Length;
                    await context.Response.OutputStream.WriteAsync(bytes);
                    context.Response.Close();
                }
            }

            public List<Call> Snapshot() { lock (Calls) return Calls.ToList(); }

            public void Clear() { lock (Calls) Calls.Clear(); }

            public void Dispose()
            {
                listener.Close();
                loop.Wait(TimeSpan.FromSeconds(5));
            }
        }

        [Fact]
        public void DefaultPolicyRevokesOnReviewOrBlockAndNothingElse()
        {
            Assert.Equal(LateResultAction.RevokeSessions, LateResults.Action(Served("complete", "review")));
            Assert.Equal(LateResultAction.RevokeSessions, LateResults.Action(Served("complete", "block")));
            Assert.Equal(LateResultAction.None, LateResults.Action(Served("complete", null)));
            Assert.Equal(LateResultAction.None, LateResults.Action(Served("pending", "review")));
            Assert.Equal(LateResultAction.None, LateResults.Action(Served("complete", "step_up")));
            Assert.Equal(LateResultAction.None, LateResults.Action(Served("partial", "allow")));
            Assert.Equal(LateResultAction.None, LateResults.Action(Event(Pending)));
            Assert.Equal(LateResultAction.None, LateResults.Action(Event((JObject)Expired["data"]!)));
            Assert.Equal(LateResultAction.None, LateResults.Action(new WebhookEvent("evt_x", "endpoint.verification", "x", new EndpointVerification("whe_x", "c"))));
            Assert.Equal(LateResultAction.None, LateResults.Action(new WebhookEvent("evt_x", "account.deleted", "x", new AccountDeleted("acct_1", 1, "x"))));
            var stepUpBan = new Dictionary<string, LateResultAction> { ["step_up"] = LateResultAction.Ban };
            Assert.Equal(LateResultAction.Ban, LateResults.Action(Served("complete", "step_up"), stepUpBan));
            Assert.Equal(LateResultAction.None, LateResults.Action(Served("complete", "review"), stepUpBan));
            Assert.Throws<ArgumentException>(() => LateResults.Action(Served("complete", "review"), new Dictionary<string, LateResultAction> { ["review"] = (LateResultAction)99 }));
            Assert.Throws<ArgumentException>(() => LateResults.Parse("delete"));
            Assert.Equal(new[] { "none", "revoke_sessions", "ban" }, new[] { LateResultAction.None, LateResultAction.RevokeSessions, LateResultAction.Ban }.Select(LateResults.Wire));
            Assert.Equal(LateResultAction.None, LateResults.DefaultPolicy["allow"]);
            Assert.Equal(LateResultAction.None, LateResults.DefaultPolicy["step_up"]);
            Assert.Equal(LateResultAction.RevokeSessions, LateResults.DefaultPolicy["review"]);
            Assert.Equal(LateResultAction.RevokeSessions, LateResults.DefaultPolicy["block"]);
            Assert.Equal(4, LateResults.DefaultPolicy.Count);
        }

        [Fact]
        public async Task ClerkReviewRevokesEachActiveSessionAndABanPolicyBansTheUser()
        {
            using var mock = new MockProvider(c =>
            {
                if (c.Method == "GET" && c.Path == "/v1/sessions")
                {
                    return (200, c.Query["offset"] == "0" ? "[{\"id\":\"sess_1\",\"status\":\"active\"},{\"id\":\"sess_2\",\"status\":\"active\"}]" : "[]");
                }
                if (c.Method == "POST" && System.Text.RegularExpressions.Regex.IsMatch(c.Path, "^/v1/sessions/sess_\\d/revoke$"))
                {
                    return (200, "{\"id\":\"" + c.Path.Split('/')[3] + "\",\"status\":\"revoked\"}");
                }
                if (c.Method == "POST" && c.Path == "/v1/users/user_2EXAMPLE/ban") return (200, "{\"id\":\"user_2EXAMPLE\",\"banned\":true}");
                return null;
            });
            var clerk = LateResults.Clerk("sk_test_mock", mock.Url + "/v1");
            var (raw, headers) = Delivery(Review, "user_2EXAMPLE");
            var result = await LateResults.HandleAsync(raw, headers, Secrets, clerk, Options());
            Assert.Equal(LateResultAction.RevokeSessions, result.Action);
            Assert.Equal("user_2EXAMPLE", result.UserId);
            Assert.Equal((string)Review["id"]!, Assert.IsType<Assessment>(result.Event.Data).Id);
            var calls = mock.Snapshot();
            Assert.Equal(new[] { "GET /v1/sessions", "POST /v1/sessions/sess_1/revoke", "POST /v1/sessions/sess_2/revoke" }, calls.Select(c => $"{c.Method} {c.Path}"));
            Assert.Equal(new Dictionary<string, string> { ["user_id"] = "user_2EXAMPLE", ["status"] = "active", ["limit"] = "100", ["offset"] = "0" }, calls[0].Query);
            Assert.All(calls, c => Assert.Equal("Bearer sk_test_mock", c.Auth));
            Assert.All(calls, c => Assert.Equal("application/json", c.Accept));

            mock.Clear();
            var banned = await LateResults.HandleAsync(raw, headers, Secrets, clerk, Options(new Dictionary<string, LateResultAction> { ["review"] = LateResultAction.Ban }));
            Assert.Equal(LateResultAction.Ban, banned.Action);
            Assert.Equal(new[] { "POST /v1/users/user_2EXAMPLE/ban" }, mock.Snapshot().Select(c => $"{c.Method} {c.Path}"));
        }

        [Fact]
        public async Task LogOnlyLogsWhatItWouldRevokeOrBanAndCallsNothing()
        {
            var lines = new List<string>();
            var logOnly = LateResults.LogOnly(lines.Add);
            Assert.Equal("log_only", logOnly.Name);
            var (raw, headers) = Delivery(Review, "usr_8f2k1");
            var revoked = await LateResults.HandleAsync(raw, headers, Secrets, logOnly, Options());
            Assert.Equal(LateResultAction.RevokeSessions, revoked.Action);
            Assert.Equal("usr_8f2k1", revoked.UserId);
            var banned = await LateResults.HandleAsync(raw, headers, Secrets, logOnly, Options(new Dictionary<string, LateResultAction> { ["review"] = LateResultAction.Ban }));
            Assert.Equal(LateResultAction.Ban, banned.Action);
            Assert.Equal(new[]
            {
                "NoResponse late result (log only): would revoke every session of user usr_8f2k1",
                "NoResponse late result (log only): would ban user usr_8f2k1 and end their sessions",
            }, lines);
            var (testRaw, testHeaders) = Delivery(Review, "usr_8f2k1", test: true);
            Assert.Equal(LateResultAction.None, (await LateResults.HandleAsync(testRaw, testHeaders, Secrets, logOnly, Options())).Action);
            Assert.Equal(2, lines.Count);
            await Assert.ThrowsAsync<ArgumentException>(() => logOnly.BanAsync(""));
        }

        [Fact]
        public async Task WorkOSRevokePagesThroughTheSessionsAndBanRevokesThenDeletesTheUser()
        {
            using var mock = new MockProvider(c =>
            {
                if (c.Method == "GET" && c.Path == "/user_management/users/user_01WORKOS/sessions")
                {
                    return !c.Query.ContainsKey("after")
                        ? (200, "{\"object\":\"list\",\"data\":[{\"id\":\"session_A\",\"status\":\"active\"},{\"id\":\"session_X\",\"status\":\"revoked\"}],\"list_metadata\":{\"before\":null,\"after\":\"session_X\"}}")
                        : (200, "{\"object\":\"list\",\"data\":[{\"id\":\"session_B\",\"status\":\"active\"}],\"list_metadata\":{\"before\":\"session_B\",\"after\":null}}");
                }
                if (c.Method == "POST" && c.Path == "/user_management/sessions/revoke") return (200, "{\"id\":\"" + JObject.Parse(c.Body!)["session_id"] + "\",\"status\":\"revoked\"}");
                if (c.Method == "DELETE" && c.Path == "/user_management/users/user_01WORKOS") return (202, null);
                return null;
            });
            var workos = LateResults.WorkOS("sk_test_workos", mock.Url);
            var (raw, headers) = Delivery(StepUp, "acct_7f3a9c");
            var result = await LateResults.HandleAsync(raw, headers, Secrets, workos, Options(
                new Dictionary<string, LateResultAction> { ["step_up"] = LateResultAction.Ban },
                (accountId, _) => accountId == "acct_7f3a9c" ? "user_01WORKOS" : null));
            Assert.Equal(LateResultAction.Ban, result.Action);
            Assert.Equal("user_01WORKOS", result.UserId);
            var calls = mock.Snapshot();
            Assert.Equal(
                new[]
                {
                    "GET /user_management/users/user_01WORKOS/sessions limit=100",
                    "GET /user_management/users/user_01WORKOS/sessions limit=100,after=session_X",
                    "POST /user_management/sessions/revoke {\"session_id\":\"session_A\"}",
                    "POST /user_management/sessions/revoke {\"session_id\":\"session_B\"}",
                    "DELETE /user_management/users/user_01WORKOS ",
                },
                calls.Select(c => $"{c.Method} {c.Path} {(c.Body ?? string.Join(",", c.Query.Select(q => q.Key + "=" + q.Value)))}"));
            Assert.All(calls, c => Assert.Equal("Bearer sk_test_workos", c.Auth));
            Assert.All(calls.Where(c => c.Body is not null), c => Assert.StartsWith("application/json", c.ContentType));
        }

        [Fact]
        public async Task Auth0ReviewDeletesSessionsAndRefreshTokensBanBlocksFirstAndThePipeIsEscaped()
        {
            const string id = "auth0|64abc0de1234567890abcdef";
            const string escaped = "auth0%7C64abc0de1234567890abcdef";
            using var mock = new MockProvider(c =>
            {
                if (c.Path == $"/api/v2/users/{escaped}/sessions" && c.Method == "DELETE") return (202, null);
                if (c.Path == $"/api/v2/users/{escaped}/refresh-tokens" && c.Method == "DELETE") return (202, null);
                if (c.Path == $"/api/v2/users/{escaped}" && c.Method == "PATCH") return (200, "{\"user_id\":\"" + id + "\",\"blocked\":true}");
                return null;
            });
            var auth0 = LateResults.Auth0("mgmt_token", baseUrl: mock.Url + "/api/v2");
            var (raw, headers) = Delivery(Review, id);
            var result = await LateResults.HandleAsync(raw, headers, Secrets, auth0, Options());
            Assert.Equal(LateResultAction.RevokeSessions, result.Action);
            Assert.Equal(new[] { $"DELETE /api/v2/users/{escaped}/sessions", $"DELETE /api/v2/users/{escaped}/refresh-tokens" }, mock.Snapshot().Select(c => $"{c.Method} {c.Path}"));

            mock.Clear();
            await LateResults.HandleAsync(raw, headers, Secrets, auth0, Options(new Dictionary<string, LateResultAction> { ["review"] = LateResultAction.Ban }));
            var calls = mock.Snapshot();
            Assert.Equal(
                new[] { $"PATCH /api/v2/users/{escaped} {{\"blocked\":true}}", $"DELETE /api/v2/users/{escaped}/sessions null", $"DELETE /api/v2/users/{escaped}/refresh-tokens null" },
                calls.Select(c => $"{c.Method} {c.Path} {c.Body ?? "null"}"));
            Assert.All(calls, c => Assert.Equal("Bearer mgmt_token", c.Auth));
        }

        [Fact]
        public async Task AnUnverifiedQuietOrUnmappedEventMakesNoProviderCallAndAProviderRefusalThrows()
        {
            using var mock = new MockProvider(c => c.Method == "DELETE" ? (403, "{\"error\":\"insufficient_scope\"}") : null);
            var auth0 = LateResults.Auth0("mgmt_token", baseUrl: mock.Url + "/api/v2");
            var (raw, headers) = Delivery(Review, "auth0|abc");
            var wrong = await Assert.ThrowsAsync<WebhookVerificationException>(() => LateResults.HandleAsync(raw, headers, new Dictionary<string, string> { [Key] = "wrong" }, auth0, Options()));
            Assert.Equal(WebhookErrorCode.InvalidSignature, wrong.Code);
            await Assert.ThrowsAsync<WebhookVerificationException>(() => LateResults.HandleAsync(raw.Concat(new[] { (byte)' ' }).ToArray(), headers, Secrets, auth0, Options()));
            var late = await Assert.ThrowsAsync<WebhookVerificationException>(() => LateResults.HandleAsync(raw, headers, Secrets, auth0, Options(now: Now.AddSeconds(301))));
            Assert.Equal(WebhookErrorCode.InvalidTimestamp, late.Code);
            foreach (var data in new[] { StepUp, Pending, (JObject)Expired["data"]! })
            {
                var d = Delivery(data);
                Assert.Equal(LateResultAction.None, (await LateResults.HandleAsync(d.Raw, d.Headers, Secrets, auth0, Options())).Action);
            }
            Assert.Equal(LateResultAction.None, (await LateResults.HandleAsync(raw, headers, Secrets, auth0, Options(userId: (_, _) => null))).Action);
            Assert.Empty(mock.Snapshot());
            var refused = await Assert.ThrowsAsync<ProviderActionException>(() => LateResults.HandleAsync(raw, headers, Secrets, auth0, Options()));
            Assert.Equal(403, refused.Status);
            Assert.Equal("auth0", refused.Provider);
            Assert.DoesNotContain("insufficient_scope", refused.Message);
            Assert.DoesNotContain("mgmt_token", refused.Message);
            Assert.Single(mock.Snapshot());
        }

        [Fact]
        public async Task ATestEventIsFlaggedAndMakesNoProviderCallEvenUnderABanPolicyWithAResolver()
        {
            var sample = (JObject)WebhookTests.Fixture("valid/webhook-assessment-updated-test.json")["data"]!;
            using var mock = new MockProvider(_ => (200, "[]"));
            var clerk = LateResults.Clerk("sk_test_mock", mock.Url + "/v1");
            var banAll = new Dictionary<string, LateResultAction> { ["allow"] = LateResultAction.Ban, ["step_up"] = LateResultAction.Ban, ["review"] = LateResultAction.Ban };
            var (raw, headers) = Delivery(sample, test: true);
            var result = await LateResults.HandleAsync(raw, headers, Secrets, clerk, Options(banAll, (_, _) => "user_2EXAMPLE"));
            Assert.Equal(LateResultAction.None, result.Action);
            Assert.True(result.Event.Test);
            Assert.Null(result.UserId);
            Assert.Empty(mock.Snapshot());

            // The same body without the flag would ban, and an ordinary delivery is not a test.
            var real = Delivery(sample);
            Assert.False(Webhook.ConstructEvent(real.Raw, real.Headers, Secrets, now: Now).Test);
            Assert.Equal(LateResultAction.Ban, (await LateResults.HandleAsync(real.Raw, real.Headers, Secrets, clerk, Options(banAll, (_, _) => "user_2EXAMPLE"))).Action);
        }

        [Fact]
        public async Task ARepeatedBanAfterTheUserIsGoneCompletesAndA500StillThrows()
        {
            var ban = new Dictionary<string, LateResultAction> { ["review"] = LateResultAction.Ban };
            using (var gone = new MockProvider(_ => (404, "{\"message\":\"not found\"}")))
            {
                // Every user-scoped endpoint answers 404: the first delivery already removed the user.
                var providers = new[]
                {
                    LateResults.WorkOS("sk_test_workos", gone.Url),
                    LateResults.Clerk("sk_test_mock", gone.Url + "/v1"),
                    LateResults.Auth0("mgmt_token", baseUrl: gone.Url + "/api/v2"),
                };
                var (raw, headers) = Delivery(Review, "user_GONE");
                for (var i = 0; i < 2; i++)
                {
                    foreach (var provider in providers)
                    {
                        Assert.Equal(LateResultAction.Ban, (await LateResults.HandleAsync(raw, headers, Secrets, provider, Options(ban))).Action);
                        Assert.Equal(LateResultAction.RevokeSessions, (await LateResults.HandleAsync(raw, headers, Secrets, provider, Options())).Action);
                    }
                }
                var lines = gone.Snapshot().Select(c => $"{c.Method} {c.Path}").ToList();
                Assert.Contains("GET /user_management/users/user_GONE/sessions", lines);
                Assert.Contains("DELETE /user_management/users/user_GONE", lines);
                Assert.Contains("GET /v1/sessions", lines);
                Assert.Contains("POST /v1/users/user_GONE/ban", lines);
                Assert.Contains("PATCH /api/v2/users/user_GONE", lines);
                Assert.Contains("DELETE /api/v2/users/user_GONE/refresh-tokens", lines);
            }
            using (var vanished = new MockProvider(c => c.Method == "GET" ? (200, "[{\"id\":\"sess_1\",\"status\":\"active\"}]") : (404, null)))
            {
                // A session that ends between the list and its revoke also counts as done.
                await LateResults.Clerk("sk_test_mock", vanished.Url + "/v1").RevokeSessionsAsync("user_2EXAMPLE");
                Assert.Equal(new[] { "GET /v1/sessions", "POST /v1/sessions/sess_1/revoke" }, vanished.Snapshot().Select(c => $"{c.Method} {c.Path}"));
            }
            using var broken = new MockProvider(_ => (500, "{\"error\":\"boom\"}"));
            foreach (var provider in new[] { LateResults.WorkOS("k", broken.Url), LateResults.Clerk("k", broken.Url + "/v1"), LateResults.Auth0("t", baseUrl: broken.Url + "/api/v2") })
            {
                var failed = await Assert.ThrowsAsync<ProviderActionException>(() => provider.BanAsync("user_X"));
                Assert.Equal(500, failed.Status);
            }
        }

        [Fact]
        public async Task WorkOSBanTreats404OnDeleteAsDoneAndEmptyIdsAreRejected()
        {
            using var mock = new MockProvider(c => c.Method == "GET" ? (200, "{\"data\":[],\"list_metadata\":{\"after\":null}}") : null);
            var workos = LateResults.WorkOS("k", mock.Url);
            await workos.BanAsync("user_gone");
            Assert.Equal(new[] { "GET /user_management/users/user_gone/sessions", "DELETE /user_management/users/user_gone" }, mock.Snapshot().Select(c => $"{c.Method} {c.Path}"));
            await Assert.ThrowsAsync<ArgumentException>(() => workos.BanAsync(""));
            Assert.Throws<ArgumentException>(() => LateResults.Clerk(""));
        }

        private const string FeatureNotEnabled = "{\"statusCode\":403,\"error\":\"Forbidden\",\"message\":\"This feature is not enabled for this tenant.\",\"errorCode\":\"feature_not_enabled\"}";

        /// <summary>
        /// An Auth0 tenant on a plan without the sessions API, as a Free tenant answers: both bulk endpoints refuse with
        /// 403 feature_not_enabled, while /device-credentials and PATCH /users/{id} work. It keeps the refresh tokens and
        /// the blocked flag, so a repeated delivery sees what the first left.
        /// </summary>
        private sealed class Auth0WithoutSessionsApi
        {
            private readonly string escaped;
            private readonly bool refreshTokensApi;
            public volatile bool Blocked;
            public readonly List<(string Id, string Type)> Credentials = new() { ("dcr_a", "refresh_token"), ("dcr_b", "refresh_token"), ("dcr_r", "rotating_refresh_token") };

            public Auth0WithoutSessionsApi(string escaped, bool refreshTokensApi = false)
            {
                this.escaped = escaped;
                this.refreshTokensApi = refreshTokensApi;
            }

            public int Left { get { lock (Credentials) return Credentials.Count; } }

            public (int Status, string? Body)? Route(Call c)
            {
                lock (Credentials)
                {
                    var user = $"/api/v2/users/{escaped}";
                    if (c.Method == "DELETE" && c.Path == user + "/sessions") return (403, FeatureNotEnabled);
                    if (c.Method == "DELETE" && c.Path == user + "/refresh-tokens")
                    {
                        if (!refreshTokensApi) return (403, FeatureNotEnabled);
                        Credentials.Clear();
                        return (202, null);
                    }
                    if (c.Method == "PATCH" && c.Path == user)
                    {
                        Blocked = (bool?)JObject.Parse(c.Body!)["blocked"] == true;
                        return (200, "{\"blocked\":" + (Blocked ? "true" : "false") + "}");
                    }
                    if (c.Method == "GET" && c.Path == "/api/v2/device-credentials")
                    {
                        var listed = new JArray(Credentials.Where(d => d.Type == c.Query["type"]).Select(d => new JObject { ["id"] = d.Id, ["type"] = d.Type }));
                        return (200, listed.ToString(Newtonsoft.Json.Formatting.None));
                    }
                    if (c.Method == "DELETE" && c.Path.StartsWith("/api/v2/device-credentials/", StringComparison.Ordinal))
                    {
                        var id = c.Path.Substring("/api/v2/device-credentials/".Length);
                        return Credentials.RemoveAll(d => d.Id == id) > 0 ? (204, null) : (404, "{\"errorCode\":\"inexistent_device_credential\"}");
                    }
                    return null;
                }
            }
        }

        [Fact]
        public async Task Auth0WithoutTheSessionsApiBlocksDeletesDeviceCredentialsAndReportsBan()
        {
            const string id = "auth0|64abc0de1234567890abcdef";
            const string escaped = "auth0%7C64abc0de1234567890abcdef";
            var tenant = new Auth0WithoutSessionsApi(escaped);
            using var mock = new MockProvider(tenant.Route);
            var auth0 = LateResults.Auth0("mgmt_token", baseUrl: mock.Url + "/api/v2");
            var (raw, headers) = Delivery(Review, id);

            var result = await LateResults.HandleAsync(raw, headers, Secrets, auth0, Options());
            Assert.Equal(LateResultAction.Ban, result.Action);
            Assert.Equal(id, result.UserId);
            Assert.True(tenant.Blocked);
            Assert.Equal(0, tenant.Left);
            var calls = mock.Snapshot();
            Assert.Equal(
                new[]
                {
                    $"DELETE /api/v2/users/{escaped}/sessions",
                    $"PATCH /api/v2/users/{escaped}",
                    $"DELETE /api/v2/users/{escaped}/refresh-tokens",
                    "GET /api/v2/device-credentials",
                    "GET /api/v2/device-credentials",
                    "DELETE /api/v2/device-credentials/dcr_a",
                    "DELETE /api/v2/device-credentials/dcr_b",
                    "DELETE /api/v2/device-credentials/dcr_r",
                },
                calls.Select(c => $"{c.Method} {c.Path}"));
            Assert.Equal("refresh_token", calls[3].Query["type"]);
            Assert.Equal("rotating_refresh_token", calls[4].Query["type"]);
            Assert.Equal(id, calls[3].Query["user_id"]);
            Assert.All(calls, c => Assert.Equal("Bearer mgmt_token", c.Auth));

            // The same delivery again: still blocked, nothing left to delete, same report.
            mock.Clear();
            Assert.Equal(LateResultAction.Ban, (await LateResults.HandleAsync(raw, headers, Secrets, auth0, Options())).Action);
            Assert.True(tenant.Blocked);
            Assert.DoesNotContain(mock.Snapshot(), c => c.Method == "DELETE" && c.Path.StartsWith("/api/v2/device-credentials/", StringComparison.Ordinal));

            // Ban blocks first, and the plan refusing the sessions endpoint is no failure.
            tenant.Blocked = false;
            lock (tenant.Credentials) tenant.Credentials.Add(("dcr_c", "refresh_token"));
            mock.Clear();
            var banned = await LateResults.HandleAsync(raw, headers, Secrets, auth0, Options(new Dictionary<string, LateResultAction> { ["review"] = LateResultAction.Ban }));
            Assert.Equal(LateResultAction.Ban, banned.Action);
            Assert.Equal("PATCH", mock.Snapshot()[0].Method);
            Assert.True(tenant.Blocked);
            Assert.Equal(0, tenant.Left);

            // Called directly, RevokeSessionsAsync takes the same fallback.
            tenant.Blocked = false;
            await auth0.RevokeSessionsAsync(id);
            Assert.True(tenant.Blocked);
        }

        [Fact]
        public async Task Auth0WithRefreshTokensButNoSessionsApiStillBlocksAndAScopeRefusalNeverFallsBack()
        {
            var (raw, headers) = Delivery(Review, "auth0|abc");
            var tenant = new Auth0WithoutSessionsApi("auth0%7Cabc", refreshTokensApi: true);
            using (var mock = new MockProvider(tenant.Route))
            {
                var result = await LateResults.HandleAsync(raw, headers, Secrets, LateResults.Auth0("t", baseUrl: mock.Url + "/api/v2"), Options());
                Assert.Equal(LateResultAction.Ban, result.Action);
                Assert.True(tenant.Blocked);
                Assert.DoesNotContain(mock.Snapshot(), c => c.Path == "/api/v2/device-credentials");
            }

            const string scope = "{\"statusCode\":403,\"error\":\"Forbidden\",\"message\":\"Insufficient scope\",\"errorCode\":\"insufficient_scope\"}";
            using var scoped = new MockProvider(c => c.Method == "DELETE" ? (403, scope) : (200, "{}"));
            var refused = await Assert.ThrowsAsync<ProviderActionException>(() => LateResults.HandleAsync(raw, headers, Secrets, LateResults.Auth0("t", baseUrl: scoped.Url + "/api/v2"), Options()));
            Assert.Equal(403, refused.Status);
            Assert.Equal("insufficient_scope", refused.Code);
            Assert.Equal(new[] { "DELETE /api/v2/users/auth0%7Cabc/sessions" }, scoped.Snapshot().Select(c => $"{c.Method} {c.Path}"));
        }

        private const string TooMany = "{\"statusCode\":429,\"error\":\"Too Many Requests\",\"message\":\"Global limit has been reached\",\"errorCode\":\"too_many_requests\"}";

        /// <summary>
        /// An HttpClient handler standing in for the Management API: it answers 429 once, with that entry's headers, to
        /// each call named in Limited (a GET by its type query) and passes everything else to the route.
        /// </summary>
        private sealed class RateLimitedHandler : System.Net.Http.HttpMessageHandler
        {
            private readonly Func<Call, (int Status, string? Body)?> route;
            public Dictionary<string, Dictionary<string, string>> Limited { get; }
            public List<string> Sent { get; } = new();

            public RateLimitedHandler(Dictionary<string, Dictionary<string, string>> limited, Func<Call, (int Status, string? Body)?> route)
            {
                Limited = limited;
                this.route = route;
            }

            protected override async Task<System.Net.Http.HttpResponseMessage> SendAsync(System.Net.Http.HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
            {
                var uri = request.RequestUri!;
                var query = new Dictionary<string, string>();
                foreach (var pair in uri.Query.TrimStart('?').Split('&', StringSplitOptions.RemoveEmptyEntries))
                {
                    var eq = pair.IndexOf('=');
                    query[Uri.UnescapeDataString(pair.Substring(0, eq))] = Uri.UnescapeDataString(pair.Substring(eq + 1));
                }
                var body = request.Content is null ? null : await request.Content.ReadAsStringAsync(cancellationToken);
                var call = new Call(request.Method.Method, uri.AbsolutePath, query, null, null, null, body);
                var key = call.Method == "GET" ? $"GET {query["type"]}" : $"{call.Method} {call.Path}";
                lock (Sent)
                {
                    Sent.Add(key);
                    if (Limited.Remove(key, out var headers))
                    {
                        var limited = new System.Net.Http.HttpResponseMessage((HttpStatusCode)429) { Content = new System.Net.Http.StringContent(TooMany) };
                        foreach (var (name, value) in headers) limited.Headers.TryAddWithoutValidation(name, value);
                        return limited;
                    }
                }
                var (status, text) = route(call) ?? (404, "{\"error\":\"not found\"}");
                return new System.Net.Http.HttpResponseMessage((HttpStatusCode)status) { Content = new System.Net.Http.StringContent(text ?? "") };
            }
        }

        [Fact]
        public async Task Auth0WaitsOutA429FromTheManagementApiAndRetriesTheCall()
        {
            var tenant = new Auth0WithoutSessionsApi("auth0%7Cabc");
            // One 429 each: Retry-After, an X-RateLimit-Reset already past, and no header (the backoff).
            var handler = new RateLimitedHandler(
                new Dictionary<string, Dictionary<string, string>>
                {
                    ["GET refresh_token"] = new() { ["Retry-After"] = "0" },
                    ["DELETE /api/v2/device-credentials/dcr_a"] = new() { ["X-RateLimit-Reset"] = (DateTimeOffset.UtcNow.ToUnixTimeSeconds() - 5).ToString(System.Globalization.CultureInfo.InvariantCulture) },
                    ["DELETE /api/v2/device-credentials/dcr_b"] = new(),
                },
                tenant.Route);
            using var client = new System.Net.Http.HttpClient(handler);
            var auth0 = (ILateResultActor)LateResults.Auth0("mgmt_token", baseUrl: "https://tenant.example/api/v2", httpClient: client);
            Assert.Equal(LateResultAction.Ban, await auth0.ActAsync(LateResultAction.RevokeSessions, "auth0|abc"));
            Assert.True(tenant.Blocked);
            Assert.Equal(0, tenant.Left);
            Assert.Empty(handler.Limited);
            Assert.Equal(2, handler.Sent.Count(k => k == "GET refresh_token"));
            Assert.Equal(2, handler.Sent.Count(k => k == "DELETE /api/v2/device-credentials/dcr_a"));
            Assert.Equal(2, handler.Sent.Count(k => k == "DELETE /api/v2/device-credentials/dcr_b"));
        }

        [Fact]
        public async Task Auth0ThrowsA429AtOnceWhenTheWaitItAsksForPassesTheBudget()
        {
            var handler = new RateLimitedHandler(
                new Dictionary<string, Dictionary<string, string>> { ["DELETE /api/v2/users/auth0%7Cabc/sessions"] = new() { ["Retry-After"] = "60" } },
                _ => (200, "{}"));
            using var client = new System.Net.Http.HttpClient(handler);
            var auth0 = (ILateResultActor)LateResults.Auth0("mgmt_token", baseUrl: "https://tenant.example/api/v2", httpClient: client);
            var started = System.Diagnostics.Stopwatch.StartNew();
            var limited = await Assert.ThrowsAsync<ProviderActionException>(() => auth0.ActAsync(LateResultAction.RevokeSessions, "auth0|abc"));
            Assert.Equal(429, limited.Status);
            Assert.Equal("too_many_requests", limited.Code);
            Assert.True(started.Elapsed < TimeSpan.FromSeconds(1));
            Assert.Equal(new[] { "DELETE /api/v2/users/auth0%7Cabc/sessions" }, handler.Sent);
        }

        private sealed class PlainProvider : ILateResultProvider
        {
            public List<string> Done { get; } = new();

            public string Name => "plain";

            public Task RevokeSessionsAsync(string userId, System.Threading.CancellationToken cancellationToken = default)
            {
                Done.Add("revoke " + userId);
                return Task.CompletedTask;
            }

            public Task BanAsync(string userId, System.Threading.CancellationToken cancellationToken = default)
            {
                Done.Add("ban " + userId);
                return Task.CompletedTask;
            }
        }

        [Fact]
        public async Task AProviderWithoutActReportsTheActionAskedFor()
        {
            var (raw, headers) = Delivery(Review, "u_1");
            var plain = new PlainProvider();
            Assert.Equal(LateResultAction.RevokeSessions, (await LateResults.HandleAsync(raw, headers, Secrets, plain, Options())).Action);
            Assert.Equal(LateResultAction.Ban, (await LateResults.HandleAsync(raw, headers, Secrets, plain, Options(new Dictionary<string, LateResultAction> { ["review"] = LateResultAction.Ban }))).Action);
            Assert.Equal(new[] { "revoke u_1", "ban u_1" }, plain.Done);
        }
    }
}
