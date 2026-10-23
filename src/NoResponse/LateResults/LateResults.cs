#nullable enable
namespace NoResponse.LateResults
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using NoResponse.Models.Shared;
    using NoResponse.Webhooks;

    /// <summary>What to do to the user. Ban also ends every session.</summary>
    public enum LateResultAction
    {
        None,
        RevokeSessions,
        Ban,
    }

    /// <summary>The action for each recommendation key (allow, step_up, review). A key you leave out does nothing.</summary>
    public sealed class LateResultPolicy : Dictionary<string, LateResultAction>
    {
        public LateResultPolicy() { }

        public LateResultPolicy(IDictionary<string, LateResultAction> entries) : base(entries) { }
    }

    /// <summary>The two admin actions for one auth provider. Build one with LateResults.Clerk, WorkOS or Auth0.</summary>
    public interface ILateResultProvider
    {
        string Name { get; }

        /// <summary>End every active session of the user.</summary>
        Task RevokeSessionsAsync(string userId, CancellationToken cancellationToken = default);

        /// <summary>Stop the user signing in again and end their sessions.</summary>
        Task BanAsync(string userId, CancellationToken cancellationToken = default);
    }

    /// <summary>Thrown when the provider's admin API refuses a call. Answer the webhook with a 5xx so NoResponse retries it.</summary>
    public sealed class ProviderActionException : Exception
    {
        public ProviderActionException(string provider, int status, string message) : base(message)
        {
            Provider = provider;
            Status = status;
        }

        public string Provider { get; }

        public int Status { get; }
    }

    public sealed class LateResultOptions
    {
        /// <summary>Recommendation to action. Defaults to LateResults.DefaultPolicy.</summary>
        public IReadOnlyDictionary<string, LateResultAction>? Policy { get; init; }

        /// <summary>
        /// The provider user id for the assessment's account id. Defaults to the account id itself,
        /// as when you assess with the provider's user id. Return null or empty to take no action.
        /// </summary>
        public Func<string, WebhookEvent, string?>? UserId { get; init; }

        /// <summary>Replay window, as for Webhook.ConstructEvent.</summary>
        public TimeSpan? Tolerance { get; init; }

        /// <summary>The clock, as for Webhook.ConstructEvent.</summary>
        public DateTimeOffset? Now { get; init; }
    }

    /// <summary>The outcome of HandleAsync.</summary>
    /// <param name="Event">The verified event. Answer an endpoint.verification with Webhook.VerificationResponse.</param>
    /// <param name="Action">The action taken. None when the policy, the event or the user id called for nothing.</param>
    /// <param name="UserId">The provider user id acted on, when an action was taken.</param>
    public sealed record LateResult(WebhookEvent Event, LateResultAction Action, string? UserId = null);

    /// <summary>
    /// Late-result handlers. Hand-written; not generated.
    /// An assessment can be revised after the signup was allowed: a slow source settles and the
    /// recommendation rises. NoResponse sends the new version as a signed assessment.updated webhook.
    /// HandleAsync verifies that delivery with Webhook.ConstructEvent, picks an action from your policy
    /// and acts through the auth provider's admin API. It never acts on an unverified event or a test event.
    /// Every provider call treats a 404 (user or session already gone) as done, so a repeated delivery is safe.
    /// </summary>
    public static class LateResults
    {
        /// <summary>Revoke sessions on review; do nothing on allow or step_up. Banning is opt-in.</summary>
        public static readonly IReadOnlyDictionary<string, LateResultAction> DefaultPolicy = new Dictionary<string, LateResultAction>
        {
            ["allow"] = LateResultAction.None,
            ["step_up"] = LateResultAction.None,
            ["review"] = LateResultAction.RevokeSessions,
        };

        /// <summary>The wire string for an action: none, revoke_sessions or ban.</summary>
        public static string Wire(LateResultAction action) => action switch
        {
            LateResultAction.None => "none",
            LateResultAction.RevokeSessions => "revoke_sessions",
            LateResultAction.Ban => "ban",
            _ => throw new ArgumentException($"unknown late result action {(int)action}", nameof(action)),
        };

        /// <summary>Parse a wire string into an action.</summary>
        public static LateResultAction Parse(string value) => value switch
        {
            "none" => LateResultAction.None,
            "revoke_sessions" => LateResultAction.RevokeSessions,
            "ban" => LateResultAction.Ban,
            _ => throw new ArgumentException($"unknown late result action {value}", nameof(value)),
        };

        /// <summary>
        /// The action your policy names for a verified event. Only a served assessment with a recommendation can act;
        /// a test event from the send-test-event call never does.
        /// </summary>
        /// <exception cref="ArgumentException">The policy holds an unknown action.</exception>
        public static LateResultAction Action(WebhookEvent webhookEvent, IReadOnlyDictionary<string, LateResultAction>? policy = null)
        {
            policy ??= DefaultPolicy;
            if (webhookEvent.Type != "assessment.updated" || webhookEvent.Test || webhookEvent.Data is not Assessment assessment)
            {
                return LateResultAction.None;
            }
            var recommendation = (assessment.Recommendation ?? assessment.Risk?.Recommendation)?.Value;
            if (recommendation is not ("allow" or "step_up" or "review"))
            {
                return LateResultAction.None;
            }
            if (!policy.TryGetValue(recommendation, out var action))
            {
                return LateResultAction.None;
            }
            Wire(action);
            return action;
        }

        /// <summary>
        /// Verify a webhook delivery and apply your policy to it. <paramref name="payload"/>, <paramref name="headers"/>,
        /// <paramref name="secrets"/> and the tolerance options are exactly what Webhook.ConstructEvent takes; a delivery
        /// that fails verification throws WebhookVerificationException before any provider call. Deduplicate on
        /// <c>result.Event.Id</c> and ignore versions older than one you have acted on.
        /// </summary>
        public static async Task<LateResult> HandleAsync<TValues>(
            byte[] payload,
            IEnumerable<KeyValuePair<string, TValues>> headers,
            IReadOnlyDictionary<string, string> secrets,
            ILateResultProvider provider,
            LateResultOptions? options = null,
            CancellationToken cancellationToken = default)
            where TValues : IEnumerable<string?>
        {
            options ??= new LateResultOptions();
            var webhookEvent = Webhook.ConstructEvent(payload, headers, secrets, options.Tolerance, options.Now);
            var action = Action(webhookEvent, options.Policy);
            if (action == LateResultAction.None || webhookEvent.Data is not Assessment assessment)
            {
                return new LateResult(webhookEvent, LateResultAction.None);
            }
            var userId = options.UserId is null ? assessment.AccountId : options.UserId(assessment.AccountId, webhookEvent);
            if (string.IsNullOrEmpty(userId))
            {
                return new LateResult(webhookEvent, LateResultAction.None);
            }
            if (action == LateResultAction.Ban)
            {
                await provider.BanAsync(userId, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                await provider.RevokeSessionsAsync(userId, cancellationToken).ConfigureAwait(false);
            }
            return new LateResult(webhookEvent, action, userId);
        }

        /// <summary>
        /// Clerk Backend API. Revoking lists the user's active sessions (GET /sessions?user_id&amp;status=active) and
        /// revokes each (POST /sessions/{id}/revoke). Ban is POST /users/{id}/ban, which revokes every session and blocks sign-in.
        /// A 404 on any call (user or session already gone) counts as done, so a repeated delivery is safe.
        /// </summary>
        public static ILateResultProvider Clerk(string secretKey, string? baseUrl = null, HttpClient? httpClient = null)
        {
            if (string.IsNullOrEmpty(secretKey)) throw new ArgumentException("Clerk needs a secret key", nameof(secretKey));
            return new ClerkProvider(new Caller("clerk", baseUrl ?? "https://api.clerk.com/v1", secretKey, httpClient));
        }

        /// <summary>
        /// WorkOS User Management. Revoking pages through GET /user_management/users/{id}/sessions and revokes each
        /// active one with POST /user_management/sessions/revoke. WorkOS has no block or ban, so Ban revokes the
        /// sessions and then deletes the user (DELETE /user_management/users/{id}); deletion is permanent.
        /// A 404 on any call (user or session already gone) counts as done, so a repeated delivery is safe.
        /// </summary>
        public static ILateResultProvider WorkOS(string apiKey, string? baseUrl = null, HttpClient? httpClient = null)
        {
            if (string.IsNullOrEmpty(apiKey)) throw new ArgumentException("WorkOS needs an API key", nameof(apiKey));
            return new WorkOSProvider(new Caller("workos", baseUrl ?? "https://api.workos.com", apiKey, httpClient));
        }

        /// <summary>
        /// Auth0 Management API at https://{domain}/api/v2 (or <paramref name="baseUrl"/>). Revoking deletes every session
        /// (DELETE /users/{id}/sessions) and refresh token (DELETE /users/{id}/refresh-tokens). Ban first sets
        /// blocked: true (PATCH /users/{id}), then does the same. The token needs update:users, delete:sessions and delete:refresh_tokens.
        /// A 404 on any call (user already gone) counts as done, so a repeated delivery is safe.
        /// </summary>
        public static ILateResultProvider Auth0(string token, string? domain = null, string? baseUrl = null, HttpClient? httpClient = null)
        {
            if (string.IsNullOrEmpty(token)) throw new ArgumentException("Auth0 needs a token", nameof(token));
            var root = baseUrl ?? (string.IsNullOrEmpty(domain) ? null : $"https://{domain}/api/v2");
            if (root is null) throw new ArgumentException("Auth0 needs a domain or a base URL", nameof(domain));
            return new Auth0Provider(new Caller("auth0", root, token, httpClient));
        }

        /// <summary>
        /// A provider for an app with its own login, or for trying the handler before you wire up a real one.
        /// Revoking and banning each log one line saying what they would do and call nothing: no request leaves
        /// your process. <paramref name="log"/> receives the line; by default it goes to standard output.
        /// Replace it with your own ILateResultProvider once you know what revoking and banning mean in your app.
        /// </summary>
        public static ILateResultProvider LogOnly(Action<string>? log = null) => new LogOnlyProvider(log ?? Console.WriteLine);

        private static readonly HttpClient SharedClient = new HttpClient();

        private static string Segment(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("user id must be a non-empty string", nameof(id));
            return Uri.EscapeDataString(id);
        }

        private static List<JToken> Sessions(JToken? body) => body switch
        {
            JArray array => new List<JToken>(array),
            JObject { } obj when obj["data"] is JArray data => new List<JToken>(data),
            _ => new List<JToken>(),
        };

        private static void CollectActive(IEnumerable<JToken> page, List<string> active)
        {
            foreach (var session in page)
            {
                if ((string?)session["status"] == "active" && session["id"] is JValue { Type: JTokenType.String } id && !string.IsNullOrEmpty((string?)id))
                {
                    active.Add((string)id!);
                }
            }
        }

        private sealed class Caller
        {
            private readonly string name;
            private readonly string baseUrl;
            private readonly string authorization;
            private readonly HttpClient client;

            public Caller(string name, string baseUrl, string credential, HttpClient? client)
            {
                this.name = name;
                this.baseUrl = baseUrl.TrimEnd('/');
                authorization = "Bearer " + credential;
                this.client = client ?? SharedClient;
            }

            public async Task<JToken?> SendAsync(HttpMethod method, string path, IEnumerable<KeyValuePair<string, string>>? query, JObject? body, bool allow404, CancellationToken cancellationToken)
            {
                var url = new StringBuilder(baseUrl).Append(path);
                var separator = '?';
                foreach (var pair in query ?? Array.Empty<KeyValuePair<string, string>>())
                {
                    url.Append(separator).Append(Uri.EscapeDataString(pair.Key)).Append('=').Append(Uri.EscapeDataString(pair.Value));
                    separator = '&';
                }
                using var request = new HttpRequestMessage(method, new Uri(url.ToString()));
                request.Headers.TryAddWithoutValidation("Authorization", authorization);
                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                if (body is not null)
                {
                    request.Content = new StringContent(body.ToString(Newtonsoft.Json.Formatting.None), Encoding.UTF8, "application/json");
                }
                using var response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
                var text = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                if (allow404 && response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                if (!response.IsSuccessStatusCode)
                {
                    throw new ProviderActionException(name, (int)response.StatusCode, $"{name} {method.Method} {path} answered {(int)response.StatusCode}");
                }
                if (string.IsNullOrWhiteSpace(text))
                {
                    return null;
                }
                try
                {
                    return JToken.Parse(text);
                }
                catch (Newtonsoft.Json.JsonException)
                {
                    return null;
                }
            }
        }

        private sealed class LogOnlyProvider : ILateResultProvider
        {
            private readonly Action<string> log;

            public LogOnlyProvider(Action<string> log) { this.log = log; }

            public string Name => "log_only";

            public Task RevokeSessionsAsync(string userId, CancellationToken cancellationToken = default)
            {
                Segment(userId);
                log($"NoResponse late result (log only): would revoke every session of user {userId}");
                return Task.CompletedTask;
            }

            public Task BanAsync(string userId, CancellationToken cancellationToken = default)
            {
                Segment(userId);
                log($"NoResponse late result (log only): would ban user {userId} and end their sessions");
                return Task.CompletedTask;
            }
        }

        private sealed class ClerkProvider : ILateResultProvider
        {
            private const int Page = 100;
            private readonly Caller call;

            public ClerkProvider(Caller call) { this.call = call; }

            public string Name => "clerk";

            public async Task RevokeSessionsAsync(string userId, CancellationToken cancellationToken = default)
            {
                Segment(userId);
                var active = new List<string>();
                for (var offset = 0; offset < Page * 1000; offset += Page)
                {
                    var query = new[]
                    {
                        new KeyValuePair<string, string>("user_id", userId),
                        new KeyValuePair<string, string>("status", "active"),
                        new KeyValuePair<string, string>("limit", Page.ToString(CultureInfo.InvariantCulture)),
                        new KeyValuePair<string, string>("offset", offset.ToString(CultureInfo.InvariantCulture)),
                    };
                    var page = Sessions(await call.SendAsync(HttpMethod.Get, "/sessions", query, null, true, cancellationToken).ConfigureAwait(false));
                    CollectActive(page, active);
                    if (page.Count < Page) break;
                }
                foreach (var id in active)
                {
                    await call.SendAsync(HttpMethod.Post, $"/sessions/{Uri.EscapeDataString(id)}/revoke", null, null, true, cancellationToken).ConfigureAwait(false);
                }
            }

            public async Task BanAsync(string userId, CancellationToken cancellationToken = default)
            {
                await call.SendAsync(HttpMethod.Post, $"/users/{Segment(userId)}/ban", null, null, true, cancellationToken).ConfigureAwait(false);
            }
        }

        private sealed class WorkOSProvider : ILateResultProvider
        {
            private readonly Caller call;

            public WorkOSProvider(Caller call) { this.call = call; }

            public string Name => "workos";

            public async Task RevokeSessionsAsync(string userId, CancellationToken cancellationToken = default)
            {
                var user = Segment(userId);
                var active = new List<string>();
                string? after = null;
                for (var round = 0; round < 1000; round++)
                {
                    var query = new List<KeyValuePair<string, string>> { new("limit", "100") };
                    if (!string.IsNullOrEmpty(after)) query.Add(new("after", after));
                    var body = await call.SendAsync(HttpMethod.Get, $"/user_management/users/{user}/sessions", query, null, true, cancellationToken).ConfigureAwait(false);
                    CollectActive(Sessions(body), active);
                    var next = body?["list_metadata"]?["after"];
                    if (next is not JValue { Type: JTokenType.String } || string.IsNullOrEmpty((string?)next)) break;
                    after = (string?)next;
                }
                foreach (var id in active)
                {
                    await call.SendAsync(HttpMethod.Post, "/user_management/sessions/revoke", null, new JObject { ["session_id"] = id }, true, cancellationToken).ConfigureAwait(false);
                }
            }

            public async Task BanAsync(string userId, CancellationToken cancellationToken = default)
            {
                await RevokeSessionsAsync(userId, cancellationToken).ConfigureAwait(false);
                await call.SendAsync(HttpMethod.Delete, $"/user_management/users/{Segment(userId)}", null, null, true, cancellationToken).ConfigureAwait(false);
            }
        }

        private sealed class Auth0Provider : ILateResultProvider
        {
            private readonly Caller call;

            public Auth0Provider(Caller call) { this.call = call; }

            public string Name => "auth0";

            public async Task RevokeSessionsAsync(string userId, CancellationToken cancellationToken = default)
            {
                var user = Segment(userId);
                await call.SendAsync(HttpMethod.Delete, $"/users/{user}/sessions", null, null, true, cancellationToken).ConfigureAwait(false);
                await call.SendAsync(HttpMethod.Delete, $"/users/{user}/refresh-tokens", null, null, true, cancellationToken).ConfigureAwait(false);
            }

            public async Task BanAsync(string userId, CancellationToken cancellationToken = default)
            {
                await call.SendAsync(HttpMethod.Patch, $"/users/{Segment(userId)}", null, new JObject { ["blocked"] = true }, true, cancellationToken).ConfigureAwait(false);
                await RevokeSessionsAsync(userId, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
