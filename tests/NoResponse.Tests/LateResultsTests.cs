namespace NoResponse.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.Json.Nodes;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using NoResponse.LateResults;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using NoResponse.WebhookVerification;
    using Xunit;

    /// <summary>
    /// The shared late enforcement vectors through HandleRiskChangeAsync, plus what the vectors cannot say in JSON:
    /// verification, the custom provider and the credentials.
    /// </summary>
    public class LateResultsTests
    {
        private static readonly JsonObject Vectors = JsonNode.Parse(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "fixtures", "late-results-vectors.json")))!.AsObject();
        private static readonly Dictionary<string, string> Secrets = new() { [(string)Vectors["key_id"]!] = (string)Vectors["secret_utf8"]! };
        private static readonly DateTimeOffset Now = DateTimeOffset.FromUnixTimeSeconds((long)Vectors["now"]!);

        public static IEnumerable<object[]> Cases() =>
            Vectors["cases"]!.AsArray().Select(c => new object[] { (string)c!["name"]! });

        private static JsonObject Case(string name) =>
            Vectors["cases"]!.AsArray().Single(c => (string)c!["name"]! == name)!.AsObject();

        private static (byte[] Raw, Dictionary<string, string> Headers) Sign(JsonNode webhookEvent)
        {
            var raw = Encoding.UTF8.GetBytes(webhookEvent.ToJsonString());
            var stamp = ((long)Vectors["now"]!).ToString();
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes((string)Vectors["secret_utf8"]!));
            var digest = Convert.ToHexString(hmac.ComputeHash(Encoding.UTF8.GetBytes(stamp + ".").Concat(raw).ToArray())).ToLowerInvariant();
            return (raw, new Dictionary<string, string>
            {
                ["NR-Webhook-Id"] = (string)webhookEvent["id"]!,
                ["NR-Webhook-Timestamp"] = stamp,
                ["NR-Webhook-Key-Id"] = (string)Vectors["key_id"]!,
                ["NR-Webhook-Signature"] = "v1=" + digest,
            });
        }

        /// <summary>Method, path and query as an order-free set of decoded pairs.</summary>
        private static string Normalize(string method, Uri url)
        {
            var query = HttpUtility.ParseQueryString(url.Query);
            var pairs = query.AllKeys.SelectMany(k => query.GetValues(k)!.Select(v => $"{k}={v}")).OrderBy(p => p, StringComparer.Ordinal);
            return $"{method} {url.GetLeftPart(UriPartial.Path)}?{string.Join("&", pairs)}";
        }

        /// <summary>Serves the case's exchanges in order and fails on any other request.</summary>
        private sealed class Transcript : HttpMessageHandler
        {
            private readonly JsonArray exchanges;

            public Transcript(JsonArray exchanges) => this.exchanges = exchanges;

            public int Next { get; private set; }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var got = Normalize(request.Method.Method, request.RequestUri!);
                Assert.True(Next < exchanges.Count, $"unexpected request {got}");
                var expected = exchanges[Next]!;
                var want = expected["request"]!;
                Assert.Equal(Normalize((string)want["method"]!, new Uri((string)want["url"]!)), got);
                if (want.AsObject().ContainsKey("body"))
                {
                    var text = request.Content == null ? "" : await request.Content.ReadAsStringAsync(cancellationToken);
                    Assert.True(JsonNode.DeepEquals(want["body"], JsonNode.Parse(text)), $"body of request {Next}: {text}");
                }
                Next++;
                var answer = expected["response"]!;
                var status = (int)answer["status"]!;
                var response = new HttpResponseMessage((System.Net.HttpStatusCode)status) { RequestMessage = request };
                var type = "application/json";
                foreach (var (name, value) in answer["headers"]?.AsObject() ?? new JsonObject())
                {
                    if (name == "content-type") type = (string)value!;
                    else response.Headers.TryAddWithoutValidation(name, (string)value!);
                }
                var body = answer["body"];
                response.Content = body == null || status == 204 ? new ByteArrayContent(Array.Empty<byte>()) : new StringContent(body.ToJsonString(), Encoding.UTF8);
                if (body != null && status != 204) response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(type);
                return response;
            }

            public void Done() => Assert.Equal(exchanges.Count, Next);
        }

        /// <summary>The generated client's transport, sending through the same transcript.</summary>
        private sealed class Through : NoResponseHttpClient
        {
            private readonly HttpClient http;

            public Through(HttpClient http) => this.http = http;

            public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken? cancellationToken = null) =>
                http.SendAsync(request, cancellationToken ?? CancellationToken.None);
        }

        private static (NoResponseClient Client, HttpClient Http, Transcript Transcript) Fake(JsonArray exchanges)
        {
            var transcript = new Transcript(exchanges);
            var http = new HttpClient(transcript);
            var client = new NoResponseClient(apiKey: (string)Vectors["api_key"]!, serverUrl: (string)Vectors["api_base_url"]!, client: new Through(http));
            return (client, http, transcript);
        }

        [Theory]
        [MemberData(nameof(Cases))]
        public async Task Vector(string name)
        {
            var vector = Case(name);
            var (client, http, transcript) = Fake(vector["exchanges"]!.AsArray());
            var provider = LateResults.IdentityProvider((string)vector["provider"]!, new IdentityProviderOptions
            {
                Token = (string)Vectors["provider_token"]!,
                Domain = (string?)vector["domain"],
                HttpClient = http,
            });
            var flows = (vector["flows"]?.AsObject() ?? new JsonObject())
                .ToDictionary(f => f.Key, f => new FlowPolicy((bool?)f.Value!["reinstate"] ?? false));
            var (raw, headers) = Sign(vector["event"]!);
            var options = new EnforcementOptions
            {
                Provider = provider,
                Flows = flows,
                Now = Now,
                GetAccountState = account =>
                {
                    Assert.Equal((string)vector["event"]!["data"]!["account"]!["id"]!, account.Id);
                    var state = vector["state"];
                    return Task.FromResult(state == null
                        ? null
                        : new AccountStateAnswer((string)state["state"]!, (string?)state["user_id"], (long?)state["enforced_risk_version"]));
                },
            };
            if (vector["error"] is JsonObject error)
            {
                var refused = await Assert.ThrowsAsync<ProviderActionException>(() => LateResults.HandleRiskChangeAsync(client, raw, headers, Secrets, options));
                transcript.Done();
                Assert.Equal((string)error["provider"]!, refused.Provider);
                Assert.Equal((int)error["status"]!, refused.Status);
                Assert.Equal((string?)error["code"], refused.Code);
                return;
            }
            var result = await LateResults.HandleRiskChangeAsync(client, raw, headers, Secrets, options);
            transcript.Done();
            Assert.Equal((string?)vector["result"]!["skipped"], result.Skipped);
            var got = new JsonArray(result.Enforcements.Select(e =>
            {
                var item = new JsonObject { ["flow"] = e.Flow, ["action"] = e.Action, ["status"] = e.Status };
                if (e.ErrorCode != null) item["error_code"] = e.ErrorCode;
                return (JsonNode)item;
            }).ToArray());
            Assert.True(JsonNode.DeepEquals(vector["result"]!["enforcements"], got), got.ToJsonString());
        }

        [Fact]
        public void TheVectorsCoverEveryProviderStateAndSkip()
        {
            var cases = Vectors["cases"]!.AsArray().Select(c => c!.AsObject()).ToList();
            Assert.Equal(new[] { "auth0", "clerk", "workos" }, cases.Select(c => (string)c["provider"]!).Distinct().OrderBy(s => s));
            Assert.Equal(new[] { "active", "onboarding", "provisional", "rejected", "restricted" },
                cases.Where(c => c["state"] != null).Select(c => (string)c["state"]!["state"]!).Distinct().OrderBy(s => s));
            Assert.Equal(new[] { "account_gone", "already_enforced", "not_actionable", "unknown_user" },
                cases.Select(c => (string?)c["result"]!["skipped"]).OfType<string>().Distinct().OrderBy(s => s));
        }

        [Fact]
        public async Task ADeliveryThatFailsVerificationReadsAndActsOnNothing()
        {
            var (client, _, transcript) = Fake(new JsonArray());
            var (raw, headers) = Sign(Case("clerk_active_block_restricts_and_revokes")["event"]!);
            var options = new EnforcementOptions
            {
                Provider = LateResults.CustomProvider(new Dictionary<string, Func<string, Task>>()),
                Now = Now,
                GetAccountState = _ => throw new InvalidOperationException("not called"),
            };
            await Assert.ThrowsAsync<WebhookVerificationException>(() => LateResults.HandleRiskChangeAsync(client, raw.Append((byte)' ').ToArray(), headers, Secrets, options));
            transcript.Done();
        }

        private static JsonObject Record(string action, string status, string? errorCode)
        {
            var body = new JsonObject
            {
                ["risk_version"] = 9, ["flow"] = "login", ["action"] = action, ["state_before"] = "active", ["status"] = status, ["provider"] = "custom",
            };
            if (errorCode != null) body["error_code"] = errorCode;
            var stored = (JsonObject)body.DeepClone();
            stored["id"] = action == "restrict" ? "enf_01JA2ZF5N6P7Q8R9S0T1V2W3Z0" : "enf_01JA2ZF5N6P7Q8R9S0T1V2W3Z1";
            stored["account_id"] = "acct_7f3a9c";
            stored["created_at"] = "2026-10-23T09:00:06.000Z";
            return new JsonObject
            {
                ["request"] = new JsonObject { ["method"] = "POST", ["url"] = (string)Vectors["api_base_url"]! + "/v1/accounts/acct_7f3a9c/enforcements", ["body"] = body },
                ["response"] = new JsonObject { ["status"] = 201, ["body"] = stored },
            };
        }

        [Fact]
        public async Task ACustomProviderRunsYourFunctionsAndAMissingOneFailsTheAction()
        {
            var (client, _, transcript) = Fake(new JsonArray(Record("restrict", "applied", null), Record("revoke_sessions", "failed", "custom.error")));
            var ran = new List<string>();
            var provider = LateResults.CustomProvider(new Dictionary<string, Func<string, Task>>
            {
                ["restrict"] = userId =>
                {
                    ran.Add("restrict " + userId);
                    return Task.CompletedTask;
                },
            });
            var account = new Account
            {
                Id = "acct_7f3a9c",
                RiskVersion = 9,
                DecisionsByFlow = new AccountDecisionsByFlow { AccountCreation = AccountDecision.Block, Login = AccountDecision.Block, Purchase = AccountDecision.Block },
            };
            var options = new EnforcementOptions { Provider = provider, GetAccountState = _ => Task.FromResult<AccountStateAnswer?>(new AccountStateAnswer("active", "u_1")) };
            await Assert.ThrowsAsync<InvalidOperationException>(() => LateResults.EnforceAccountAsync(client, account, options));
            transcript.Done();
            Assert.Equal(new[] { "restrict u_1" }, ran);
        }

        [Fact]
        public void IdentityProviderNeedsItsCredentials()
        {
            Assert.Throws<ArgumentException>(() => LateResults.IdentityProvider("clerk", new IdentityProviderOptions { Token = "" }));
            Assert.Throws<ArgumentException>(() => LateResults.IdentityProvider("auth0", new IdentityProviderOptions { Token = "t" }));
            Assert.Throws<ArgumentException>(() => LateResults.IdentityProvider("okta", new IdentityProviderOptions { Token = "t" }));
        }
    }
}
