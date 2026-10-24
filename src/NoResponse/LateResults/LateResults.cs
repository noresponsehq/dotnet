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
    using NoResponse.WebhookVerification;

    /// <summary>What to do to the user. Ban also ends every session.</summary>
    public enum LateResultAction
    {
        None,
        RevokeSessions,
        Ban,
    }

    /// <summary>The action for each account decision key (allow, step_up, review, block). A key you leave out does nothing.</summary>
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

    /// <summary>
    /// A provider that reports the action that ran. HandleAsync calls ActAsync when the provider implements it,
    /// and BanAsync or RevokeSessionsAsync otherwise. A provider that has to do more than it was asked implements it:
    /// LateResults.Auth0 returns Ban when the tenant's plan has no sessions API and it blocked the user to end their sessions.
    /// </summary>
    public interface ILateResultActor : ILateResultProvider
    {
        /// <summary>Take <paramref name="action"/> (RevokeSessions or Ban) and return the action that ran.</summary>
        Task<LateResultAction> ActAsync(LateResultAction action, string userId, CancellationToken cancellationToken = default);
    }

    /// <summary>Thrown when the provider's admin API refuses a call. Answer the webhook with a 5xx so NoResponse retries it.</summary>
    public sealed class ProviderActionException : Exception
    {
        public ProviderActionException(string provider, int status, string message) : this(provider, status, message, null) { }

        public ProviderActionException(string provider, int status, string message, string? code) : base(message)
        {
            Provider = provider;
            Status = status;
            Code = code;
        }

        public string Provider { get; }

        public int Status { get; }

        /// <summary>The provider's error code from the response body, such as Auth0's errorCode "insufficient_scope", when it sent one.</summary>
        public string? Code { get; }
    }

    public sealed class LateResultOptions
    {
        /// <summary>Account decision to action. Defaults to LateResults.DefaultPolicy.</summary>
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
    /// <param name="Event">The verified event. Answer an endpoint.verification by returning its challenge as a WebhookVerificationResponse.</param>
    /// <param name="Action">
    /// The action that ran. None when the policy, the event or the user id called for nothing. It can be stronger than the
    /// policy's action when the provider had to fall back: LateResults.Auth0 reports Ban when it blocked the user because the
    /// tenant's plan cannot end sessions.
    /// </param>
    /// <param name="UserId">The provider user id acted on, when an action was taken.</param>
    public sealed record LateResult(WebhookEvent Event, LateResultAction Action, string? UserId = null);

    /// <summary>
    /// Late-result handlers. Hand-written; not generated.
    /// An assessment can be revised after the account creation was allowed: a slow source settles and the
    /// account's decision rises. NoResponse sends the new version as a signed assessment.updated webhook.
    /// HandleAsync verifies that delivery with Webhook.ConstructEvent, picks an action from your policy
    /// and acts through the auth provider's admin API. It never acts on an unverified event or a test event.
    /// Every provider call treats a 404 (user or session already gone) as done, so a repeated delivery is safe.
    /// </summary>
    public static class LateResults
    {
        /// <summary>Revoke sessions on review or block; do nothing on allow or step_up. Banning is opt-in.</summary>
        public static readonly IReadOnlyDictionary<string, LateResultAction> DefaultPolicy = new Dictionary<string, LateResultAction>
        {
            ["allow"] = LateResultAction.None,
            ["step_up"] = LateResultAction.None,
            ["review"] = LateResultAction.RevokeSessions,
            ["block"] = LateResultAction.RevokeSessions,
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
        /// The action your policy names for a verified event, keyed by account.risk.decision. Only a served assessment
        /// that is no longer pending and whose account has a decision can act: a pending assessment carries an earlier
        /// assessment's decision, which you have already acted on, and a test event from the send-test-event call never acts.
        /// </summary>
        /// <exception cref="ArgumentException">The policy holds an unknown action.</exception>
        public static LateResultAction Action(WebhookEvent webhookEvent, IReadOnlyDictionary<string, LateResultAction>? policy = null)
        {
            policy ??= DefaultPolicy;
            string? decision;
            if (webhookEvent.AccountRiskChangedEvent?.Data is { } changed)
            {
                // The event is sent when any flow's decision changes; sessions are about the login
                // decision, so an unchanged login decision acts on nothing.
                decision = changed.Account.DecisionsByFlow.Login.Value;
                if (changed.PreviousDecisionsByFlow?.Login?.Value == decision) return LateResultAction.None;
            }
            else if (webhookEvent.AssessmentUpdatedEvent is { Test: not true, Data.Assessment: { } assessment } && !Status.Pending.Equals(assessment.Status))
                decision = assessment.Account?.Risk.Decision?.Value;
            else
                return LateResultAction.None;
            if (decision is not ("allow" or "step_up" or "review" or "block"))
            {
                return LateResultAction.None;
            }
            if (!policy.TryGetValue(decision, out var action))
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
            if (action == LateResultAction.None)
            {
                return new LateResult(webhookEvent, LateResultAction.None);
            }
            var accountId = webhookEvent.AccountRiskChangedEvent?.Data.Account.Id
                ?? webhookEvent.AssessmentUpdatedEvent!.Data.Assessment!.Detail.AccountId;
            var userId = options.UserId is null ? accountId : options.UserId(accountId, webhookEvent);
            if (string.IsNullOrEmpty(userId))
            {
                return new LateResult(webhookEvent, LateResultAction.None);
            }
            var ran = action;
            if (provider is ILateResultActor actor)
            {
                ran = await actor.ActAsync(action, userId, cancellationToken).ConfigureAwait(false);
            }
            else if (action == LateResultAction.Ban)
            {
                await provider.BanAsync(userId, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                await provider.RevokeSessionsAsync(userId, cancellationToken).ConfigureAwait(false);
            }
            return new LateResult(webhookEvent, ran, userId);
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
        /// blocked: true (PATCH /users/{id}), then does the same. The token needs update:users, delete:sessions,
        /// delete:refresh_tokens, read:device_credentials and delete:device_credentials.
        /// A 404 on any call (user already gone) counts as done, so a repeated delivery is safe.
        /// Auth0 offers the sessions and refresh-token endpoints on Enterprise plans only; on other plans they answer
        /// 403 feature_not_enabled. Then refresh tokens are deleted one by one through /device-credentials (types
        /// refresh_token and rotating_refresh_token), which every plan has. A login session cannot be ended, so revoking
        /// blocks the user before deleting the refresh tokens, and ActAsync reports Ban. Unblock the user in the
        /// dashboard to undo it. Any other refusal, such as 403 insufficient_scope, throws.
        /// A 429 from the Management API is waited out and retried: Retry-After, else X-RateLimit-Reset, else 250 ms
        /// doubling, at most 4 retries a call and 3 s of waiting a revoke or ban. Past that the 429 throws as
        /// ProviderActionException.
        /// </summary>
        public static ILateResultProvider Auth0(string token, string? domain = null, string? baseUrl = null, HttpClient? httpClient = null)
        {
            if (string.IsNullOrEmpty(token)) throw new ArgumentException("Auth0 needs a token", nameof(token));
            var root = baseUrl ?? (string.IsNullOrEmpty(domain) ? null : $"https://{domain}/api/v2");
            if (root is null) throw new ArgumentException("Auth0 needs a domain or a base URL", nameof(domain));
            return new Auth0Provider(new Caller("auth0", root, token, httpClient, retryRateLimits: true));
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

        /// <summary>The error code in a provider's error body: Auth0's errorCode, or a top-level code.</summary>
        private static string? ErrorCode(string text)
        {
            try
            {
                if (JToken.Parse(text) is not JObject body) return null;
                foreach (var field in new[] { "errorCode", "code" })
                {
                    if (body[field] is JValue { Type: JTokenType.String } code && !string.IsNullOrEmpty((string?)code)) return (string?)code;
                }
            }
            catch (Newtonsoft.Json.JsonException)
            {
            }
            return null;
        }

        /// <summary>
        /// Rate limits. Auth0 answers 429 when the Management API rate limit is hit, and a 429 means it did not act, so
        /// the call is safe to send again, a DELETE included. One revoke or ban waits at most RateLimitBudget in all,
        /// and each call retries at most RateLimitRetries times: it waits Retry-After seconds, else until
        /// X-RateLimit-Reset (Unix seconds), else 250 ms, 500 ms, 1 s, 2 s. A wait that would pass the budget is not
        /// started: the 429 throws at once as ProviderActionException, the webhook answers 5xx and NoResponse
        /// delivers it again later. The budget keeps the handler inside the 5 s a delivery has to be answered.
        /// </summary>
        private const int RateLimitRetries = 4;
        private static readonly TimeSpan RateLimitBudget = TimeSpan.FromSeconds(3);
        private static readonly TimeSpan RateLimitBackoff = TimeSpan.FromMilliseconds(250);

        /// <summary>How long a 429 asks to wait.</summary>
        private static TimeSpan RateLimitWait(HttpResponseMessage response, int retry)
        {
            var after = Number(Header(response, "Retry-After"));
            // Capped at an hour so a huge header cannot overflow; anything past the budget gives up anyway.
            if (after is { } delay && delay >= 0) return TimeSpan.FromSeconds(Math.Min(delay, 3600));
            var reset = Number(Header(response, "X-RateLimit-Reset"));
            if (reset is { } at)
            {
                var seconds = at - DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() / 1000.0;
                return TimeSpan.FromSeconds(Math.Clamp(seconds, 0, 3600));
            }
            return TimeSpan.FromMilliseconds(RateLimitBackoff.TotalMilliseconds * Math.Pow(2, retry));
        }

        private static string? Header(HttpResponseMessage response, string name)
        {
            if (response.Headers.TryGetValues(name, out var values) || response.Content.Headers.TryGetValues(name, out values))
            {
                foreach (var value in values) return value.Trim();
            }
            return null;
        }

        private static double? Number(string? text) =>
            double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var value) && !double.IsNaN(value) && !double.IsInfinity(value) ? value : null;

        private sealed class Caller
        {
            private readonly string name;
            private readonly string baseUrl;
            private readonly string credential;
            private readonly string authorization;
            private readonly HttpClient client;
            private readonly bool retryRateLimits;
            private TimeSpan waited = TimeSpan.Zero;

            public Caller(string name, string baseUrl, string credential, HttpClient? client, bool retryRateLimits = false)
            {
                this.name = name;
                this.baseUrl = baseUrl.TrimEnd('/');
                this.credential = credential;
                authorization = "Bearer " + credential;
                this.client = client ?? SharedClient;
                this.retryRateLimits = retryRateLimits;
            }

            /// <summary>A caller with the same settings and its own rate-limit budget, for one action.</summary>
            public Caller Fresh() => new Caller(name, baseUrl, credential, client, retryRateLimits);

            public async Task<JToken?> SendAsync(HttpMethod method, string path, IEnumerable<KeyValuePair<string, string>>? query, JObject? body, bool allow404, CancellationToken cancellationToken)
            {
                var url = new StringBuilder(baseUrl).Append(path);
                var separator = '?';
                foreach (var pair in query ?? Array.Empty<KeyValuePair<string, string>>())
                {
                    url.Append(separator).Append(Uri.EscapeDataString(pair.Key)).Append('=').Append(Uri.EscapeDataString(pair.Value));
                    separator = '&';
                }
                var target = new Uri(url.ToString());
                for (var retry = 0; ; retry++)
                {
                    using var request = new HttpRequestMessage(method, target);
                    request.Headers.TryAddWithoutValidation("Authorization", authorization);
                    request.Headers.TryAddWithoutValidation("Accept", "application/json");
                    if (body is not null)
                    {
                        request.Content = new StringContent(body.ToString(Newtonsoft.Json.Formatting.None), Encoding.UTF8, "application/json");
                    }
                    using var response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
                    var text = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                    if ((int)response.StatusCode == 429 && retryRateLimits && retry < RateLimitRetries)
                    {
                        var wait = RateLimitWait(response, retry);
                        if (waited + wait <= RateLimitBudget)
                        {
                            waited += wait;
                            await Task.Delay(wait, cancellationToken).ConfigureAwait(false);
                            continue;
                        }
                    }
                    return Answer(method, path, response, text, allow404);
                }
            }

            private JToken? Answer(HttpMethod method, string path, HttpResponseMessage response, string text, bool allow404)
            {
                if (allow404 && response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                if (!response.IsSuccessStatusCode)
                {
                    throw new ProviderActionException(name, (int)response.StatusCode, $"{name} {method.Method} {path} answered {(int)response.StatusCode}", ErrorCode(text));
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

        private sealed class Auth0Provider : ILateResultActor
        {
            private const int Page = 100;
            private readonly Caller call;

            public Auth0Provider(Caller call) { this.call = call; }

            public string Name => "auth0";

            public async Task RevokeSessionsAsync(string userId, CancellationToken cancellationToken = default)
            {
                await RevokeAsync(userId, cancellationToken).ConfigureAwait(false);
            }

            public async Task BanAsync(string userId, CancellationToken cancellationToken = default)
            {
                var user = Segment(userId);
                var call = this.call.Fresh();
                await BlockAsync(call, user, cancellationToken).ConfigureAwait(false);
                // The block already ends a session the plan cannot delete.
                await DeleteSessionsAsync(call, user, cancellationToken).ConfigureAwait(false);
                await DeleteRefreshTokensAsync(call, userId, user, cancellationToken).ConfigureAwait(false);
            }

            public async Task<LateResultAction> ActAsync(LateResultAction action, string userId, CancellationToken cancellationToken = default)
            {
                if (action == LateResultAction.Ban)
                {
                    await BanAsync(userId, cancellationToken).ConfigureAwait(false);
                    return LateResultAction.Ban;
                }
                return await RevokeAsync(userId, cancellationToken).ConfigureAwait(false);
            }

            private async Task<LateResultAction> RevokeAsync(string userId, CancellationToken cancellationToken)
            {
                var user = Segment(userId);
                var call = this.call.Fresh();
                var ended = await DeleteSessionsAsync(call, user, cancellationToken).ConfigureAwait(false);
                // Without the sessions API a login session survives; blocking is the only way to end it.
                if (!ended)
                {
                    await BlockAsync(call, user, cancellationToken).ConfigureAwait(false);
                }
                await DeleteRefreshTokensAsync(call, userId, user, cancellationToken).ConfigureAwait(false);
                return ended ? LateResultAction.RevokeSessions : LateResultAction.Ban;
            }

            /// <summary>Auth0 answers 403 feature_not_enabled when the tenant's plan does not include an endpoint.</summary>
            private static bool NotOnPlan(ProviderActionException e) => e.Status == 403 && e.Code == "feature_not_enabled";

            private static Task BlockAsync(Caller call, string user, CancellationToken cancellationToken) =>
                call.SendAsync(HttpMethod.Patch, $"/users/{user}", null, new JObject { ["blocked"] = true }, true, cancellationToken);

            /// <summary>End every session; false when the plan has no sessions API.</summary>
            private static async Task<bool> DeleteSessionsAsync(Caller call, string user, CancellationToken cancellationToken)
            {
                try
                {
                    await call.SendAsync(HttpMethod.Delete, $"/users/{user}/sessions", null, null, true, cancellationToken).ConfigureAwait(false);
                    return true;
                }
                catch (ProviderActionException e) when (NotOnPlan(e))
                {
                    return false;
                }
            }

            private static async Task DeleteRefreshTokensAsync(Caller call, string userId, string user, CancellationToken cancellationToken)
            {
                try
                {
                    await call.SendAsync(HttpMethod.Delete, $"/users/{user}/refresh-tokens", null, null, true, cancellationToken).ConfigureAwait(false);
                    return;
                }
                catch (ProviderActionException e) when (NotOnPlan(e))
                {
                }
                // Collect every id before deleting any, so the pages do not shift underneath.
                var ids = new List<string>();
                foreach (var type in new[] { "refresh_token", "rotating_refresh_token" })
                {
                    for (var page = 0; page < 10; page++)
                    {
                        var query = new[]
                        {
                            new KeyValuePair<string, string>("user_id", userId),
                            new KeyValuePair<string, string>("type", type),
                            new KeyValuePair<string, string>("page", page.ToString(CultureInfo.InvariantCulture)),
                            new KeyValuePair<string, string>("per_page", Page.ToString(CultureInfo.InvariantCulture)),
                        };
                        var listed = await call.SendAsync(HttpMethod.Get, "/device-credentials", query, null, true, cancellationToken).ConfigureAwait(false) as JArray;
                        foreach (var credential in listed ?? new JArray())
                        {
                            if (credential["id"] is JValue { Type: JTokenType.String } id && !string.IsNullOrEmpty((string?)id))
                            {
                                ids.Add((string)id!);
                            }
                        }
                        if ((listed?.Count ?? 0) < Page) break;
                    }
                }
                foreach (var id in ids)
                {
                    await call.SendAsync(HttpMethod.Delete, $"/device-credentials/{Uri.EscapeDataString(id)}", null, null, true, cancellationToken).ConfigureAwait(false);
                }
            }
        }
    }
}
