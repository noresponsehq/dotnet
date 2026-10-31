#nullable enable
namespace NoResponse.LateResults
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Nodes;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using NoResponse.Models.Errors;
    using NoResponse.Models.Shared;
    using NoResponse.WebhookVerification;

    /// <summary>Runs one action in an identity provider. Build one with LateResults.IdentityProvider or LateResults.CustomProvider.</summary>
    public interface IActionProvider
    {
        /// <summary>clerk, workos, auth0 or custom.</summary>
        string Name { get; }

        /// <summary>Take the action and return any other action that had to run with it (Auth0 restricts to end a session).</summary>
        Task<IReadOnlyList<string>> RunAsync(string action, string userId, CancellationToken cancellationToken = default);
    }

    /// <summary>Where the account is in your flow, from GetAccountState.</summary>
    /// <param name="State">onboarding, provisional, active, restricted or rejected.</param>
    /// <param name="UserId">The provider's user id. Defaults to the account id, as when you assess with the provider's user id.</param>
    /// <param name="EnforcedRiskVersion">The risk version you last enforced (a result's RiskVersion that you stored). An account at or below it is skipped.</param>
    public sealed record AccountStateAnswer(string State, string? UserId = null, long? EnforcedRiskVersion = null);

    /// <summary>The policy for one flow.</summary>
    /// <param name="Reinstate">Lift a restriction once the flow's decision is allow again. Off by default.</param>
    public sealed record FlowPolicy(bool Reinstate = false);

    /// <summary>One recorded enforcement.</summary>
    public sealed record Enforced(string Flow, string Action, string Status, string? ErrorCode = null);

    /// <summary>
    /// What was done. Skipped says why nothing was: not_actionable (not account.risk_changed, or a test event),
    /// account_gone (erased), unknown_user (GetAccountState returned null) or already_enforced.
    /// </summary>
    public sealed record EnforcementResult
    {
        /// <summary>The verified event, from HandleRiskChangeAsync. Answer an endpoint.verification with its challenge.</summary>
        public WebhookEvent? Event { get; init; }

        public Account? Account { get; init; }

        public string? State { get; init; }

        /// <summary>The risk version acted on. Store it and return it as EnforcedRiskVersion.</summary>
        public long? RiskVersion { get; init; }

        public string? Skipped { get; init; }

        /// <summary>What was recorded, in order.</summary>
        public IReadOnlyList<Enforced> Enforcements { get; init; } = Array.Empty<Enforced>();
    }

    /// <summary>The provider refused an action or could not be reached (status 0). Answer the webhook with a 5xx so it is delivered again.</summary>
    public sealed class ProviderActionException : Exception
    {
        public ProviderActionException(string provider, int status, string message, string? code = null, Exception? inner = null) : base(message, inner)
        {
            Provider = provider;
            Status = status;
            Code = code;
        }

        public string Provider { get; }

        public int Status { get; }

        /// <summary>The provider's error code, such as Auth0's errorCode "insufficient_scope", when it sent one.</summary>
        public string? Code { get; }
    }

    public sealed class EnforcementOptions
    {
        public required IActionProvider Provider { get; init; }

        /// <summary>The account's state in your flow; null for an account you do not hold.</summary>
        public required Func<Account, Task<AccountStateAnswer?>> GetAccountState { get; init; }

        /// <summary>Per flow (account_creation, login, purchase).</summary>
        public IReadOnlyDictionary<string, FlowPolicy>? Flows { get; init; }

        /// <summary>Replay window, as for Webhook.ConstructEvent.</summary>
        public TimeSpan? Tolerance { get; init; }

        /// <summary>The clock, as for Webhook.ConstructEvent.</summary>
        public DateTimeOffset? Now { get; init; }

        /// <summary>
        /// Serializes enforcement of one account across your workers. Hold an exclusive lock on the account id, await
        /// the function, store the result's RiskVersion and any state change, then release the lock. An exception must
        /// propagate. Inside the lock the handler reads the account's risk and your state again, so a stale event or
        /// listed account cannot undo a newer decision. Without it, overlapping calls can act on an old decision after
        /// a newer one.
        /// </summary>
        public Func<string, Func<Task<EnforcementResult>>, Task>? WithAccountLock { get; init; }
    }

    public sealed class IdentityProviderOptions
    {
        /// <summary>Clerk secret key, WorkOS API key or Auth0 Management API token.</summary>
        public required string Token { get; init; }

        /// <summary>Auth0 tenant domain, such as example.eu.auth0.com.</summary>
        public string? Domain { get; init; }

        /// <summary>The admin API's base URL, in place of the table's (or of Domain).</summary>
        public string? BaseUrl { get; init; }

        public HttpClient? HttpClient { get; init; }
    }

    /// <summary>
    /// Late enforcement. Generated by tools/generate_overlays.py from tools/late_results/actions.json; do not edit.
    ///
    /// account.risk_changed only wakes the handler. HandleRiskChangeAsync verifies the delivery, reads the account's
    /// current risk (GET /v1/accounts/{account_id}), asks your GetAccountState where the account is in your own flow,
    /// and takes the actions the table names for that state and the decision of its flow: onboarding and provisional
    /// (account_creation review or block) fail onboarding or reject activation and revoke sessions, and at allow activate;
    /// active login review
    /// revokes sessions and block restricts and revokes; restricted login allow unrestricts when Flows["login"].Reinstate.
    /// Every action, or none, is recorded with Accounts.RecordEnforcementAsync at the risk version read, and a refused
    /// action is recorded as failed and thrown. No action deletes a user. The same table and vectors
    /// (tests/fixtures/late-results-vectors.json) run in every SDK.
    /// </summary>
    public static class LateResults
    {
        private static readonly JsonObject Table = JsonNode.Parse("{\"lifecycle\":{\"onboarding\":{\"flow\":\"account_creation\",\"on\":{\"allow\":[\"activate\"],\"review\":[\"fail_onboarding\",\"revoke_sessions\"],\"block\":[\"fail_onboarding\",\"revoke_sessions\"]}},\"provisional\":{\"flow\":\"account_creation\",\"on\":{\"allow\":[\"activate\"],\"review\":[\"reject_activation\",\"revoke_sessions\"],\"block\":[\"reject_activation\",\"revoke_sessions\"]}},\"active\":{\"flow\":\"login\",\"on\":{\"review\":[\"revoke_sessions\"],\"block\":[\"restrict\",\"revoke_sessions\"]}},\"restricted\":{\"flow\":\"login\",\"on\":{},\"reinstate\":{\"allow\":[\"unrestrict\"]}},\"rejected\":{\"flow\":\"account_creation\",\"on\":{}}},\"providers\":{\"clerk\":{\"base_url\":\"https://api.clerk.com/v1\",\"actions\":{\"fail_onboarding\":[{\"call\":{\"method\":\"POST\",\"path\":\"/users/{user}/ban\"}}],\"reject_activation\":[{\"call\":{\"method\":\"POST\",\"path\":\"/users/{user}/ban\"}}],\"activate\":[],\"restrict\":[{\"call\":{\"method\":\"POST\",\"path\":\"/users/{user}/ban\"}}],\"unrestrict\":[{\"call\":{\"method\":\"POST\",\"path\":\"/users/{user}/unban\"}}],\"revoke_sessions\":[{\"each\":{\"list\":{\"method\":\"GET\",\"path\":\"/sessions\",\"query\":{\"user_id\":\"{user}\",\"status\":\"active\",\"limit\":\"100\"}},\"page\":{\"kind\":\"offset\",\"param\":\"offset\",\"size\":100},\"where\":{\"status\":\"active\"}},\"call\":{\"method\":\"POST\",\"path\":\"/sessions/{item}/revoke\"}}]}},\"workos\":{\"base_url\":\"https://api.workos.com\",\"actions\":{\"fail_onboarding\":[{\"call\":{\"method\":\"PUT\",\"path\":\"/user_management/users/{user}\",\"body\":{\"metadata\":{\"noresponse_state\":\"onboarding_failed\"}}}}],\"reject_activation\":[{\"call\":{\"method\":\"PUT\",\"path\":\"/user_management/users/{user}\",\"body\":{\"metadata\":{\"noresponse_state\":\"activation_rejected\"}}}}],\"activate\":[],\"restrict\":[{\"call\":{\"method\":\"PUT\",\"path\":\"/user_management/users/{user}\",\"body\":{\"metadata\":{\"noresponse_state\":\"restricted\"}}}}],\"unrestrict\":[{\"call\":{\"method\":\"PUT\",\"path\":\"/user_management/users/{user}\",\"body\":{\"metadata\":{\"noresponse_state\":null}}}}],\"revoke_sessions\":[{\"each\":{\"list\":{\"method\":\"GET\",\"path\":\"/user_management/users/{user}/sessions\",\"query\":{\"limit\":\"100\"}},\"page\":{\"kind\":\"cursor\",\"param\":\"after\",\"next\":[\"list_metadata\",\"after\"]},\"where\":{\"status\":\"active\"}},\"call\":{\"method\":\"POST\",\"path\":\"/user_management/sessions/revoke\",\"body\":{\"session_id\":\"{item}\"}}}]}},\"auth0\":{\"base_url\":\"https://{domain}/api/v2\",\"actions\":{\"fail_onboarding\":[{\"call\":{\"method\":\"PATCH\",\"path\":\"/users/{user}\",\"body\":{\"blocked\":true}}}],\"reject_activation\":[{\"call\":{\"method\":\"PATCH\",\"path\":\"/users/{user}\",\"body\":{\"blocked\":true}}}],\"activate\":[],\"restrict\":[{\"call\":{\"method\":\"PATCH\",\"path\":\"/users/{user}\",\"body\":{\"blocked\":true}}}],\"unrestrict\":[{\"call\":{\"method\":\"PATCH\",\"path\":\"/users/{user}\",\"body\":{\"blocked\":false}}}],\"revoke_sessions\":[{\"call\":{\"method\":\"DELETE\",\"path\":\"/users/{user}/sessions\"},\"fallback\":{\"status\":403,\"code\":\"feature_not_enabled\",\"reports\":\"restrict\",\"steps\":[{\"call\":{\"method\":\"PATCH\",\"path\":\"/users/{user}\",\"body\":{\"blocked\":true}}}]}},{\"call\":{\"method\":\"DELETE\",\"path\":\"/users/{user}/refresh-tokens\"},\"fallback\":{\"status\":403,\"code\":\"feature_not_enabled\",\"steps\":[{\"each\":{\"list\":{\"method\":\"GET\",\"path\":\"/device-credentials\",\"query\":{\"user_id\":\"{user}\",\"type\":\"refresh_token\",\"per_page\":\"100\"}},\"page\":{\"kind\":\"index\",\"param\":\"page\",\"size\":100}},\"call\":{\"method\":\"DELETE\",\"path\":\"/device-credentials/{item}\"}},{\"each\":{\"list\":{\"method\":\"GET\",\"path\":\"/device-credentials\",\"query\":{\"user_id\":\"{user}\",\"type\":\"rotating_refresh_token\",\"per_page\":\"100\"}},\"page\":{\"kind\":\"index\",\"param\":\"page\",\"size\":100}},\"call\":{\"method\":\"DELETE\",\"path\":\"/device-credentials/{item}\"}}]}}]}}},\"limits\":{\"retries\":4,\"budget_ms\":3000,\"backoff_ms\":250,\"max_pages\":100}}")!.AsObject();
        private static readonly JsonObject Limits = Table["limits"]!.AsObject();
        private static readonly Regex Slug = new("^[a-z0-9_.-]+$");

        /// <summary>
        /// Verify an account.risk_changed delivery and enforce the account's current risk. A delivery that fails
        /// verification throws WebhookVerificationException before anything is read; any other event, and a test
        /// event, does nothing.
        /// </summary>
        public static async Task<EnforcementResult> HandleRiskChangeAsync<TValues>(
            NoResponseClient client,
            byte[] payload,
            IEnumerable<KeyValuePair<string, TValues>> headers,
            IReadOnlyDictionary<string, string> secrets,
            EnforcementOptions options,
            CancellationToken cancellationToken = default)
            where TValues : IEnumerable<string?>
        {
            var webhookEvent = Webhook.ConstructEvent(payload, headers, secrets, options.Tolerance, options.Now);
            var changed = webhookEvent.AccountRiskChangedEvent;
            if (webhookEvent.Type.Value != "account.risk_changed" || changed == null || changed.Test == true)
            {
                return new EnforcementResult { Event = webhookEvent, Skipped = "not_actionable" };
            }
            var result = await UnderAccountLockAsync(client, changed.Data.Account.Id, options, cancellationToken);
            return result with { Event = webhookEvent };
        }

        /// <summary>Read the account's current risk and enforce it, inside WithAccountLock when you gave one.</summary>
        private static async Task<EnforcementResult> UnderAccountLockAsync(NoResponseClient client, string accountId, EnforcementOptions options, CancellationToken cancellationToken)
        {
            async Task<EnforcementResult> Enforce()
            {
                Account account;
                try
                {
                    account = (await client.Accounts.GetAsync(accountId, cancellationToken: cancellationToken)).Result;
                }
                catch (NotFoundException)
                {
                    return new EnforcementResult { Skipped = "account_gone" };
                }
                return await EnforceAsReadAsync(client, account, options, cancellationToken);
            }
            if (options.WithAccountLock == null) return await Enforce();
            EnforcementResult? result = null;
            await options.WithAccountLock(accountId, async () => result = await Enforce());
            return result ?? throw new InvalidOperationException("WithAccountLock returned without running enforce");
        }

        /// <inheritdoc cref="HandleRiskChangeAsync{TValues}"/>
        public static Task<EnforcementResult> HandleRiskChangeAsync(
            NoResponseClient client,
            byte[] payload,
            IEnumerable<KeyValuePair<string, string>> headers,
            IReadOnlyDictionary<string, string> secrets,
            EnforcementOptions options,
            CancellationToken cancellationToken = default) =>
            HandleRiskChangeAsync(client, payload, headers.Select(h => new KeyValuePair<string, string?[]>(h.Key, new string?[] { h.Value })), secrets, options, cancellationToken);

        /// <summary>
        /// Enforce one account: the account a handler fetched, or one from Accounts.ListChangedAsync with unenforced
        /// true when catching up. With WithAccountLock the account is read again inside the lock; without it the
        /// account is enforced as given.
        /// </summary>
        public static Task<EnforcementResult> EnforceAccountAsync(NoResponseClient client, Account account, EnforcementOptions options, CancellationToken cancellationToken = default) =>
            options.WithAccountLock != null
                ? UnderAccountLockAsync(client, account.Id, options, cancellationToken)
                : EnforceAsReadAsync(client, account, options, cancellationToken);

        private static async Task<EnforcementResult> EnforceAsReadAsync(NoResponseClient client, Account account, EnforcementOptions options, CancellationToken cancellationToken)
        {
            var answer = await options.GetAccountState(account);
            if (answer == null) return new EnforcementResult { Account = account, Skipped = "unknown_user" };
            var state = answer.State;
            var rule = Table["lifecycle"]![state]?.AsObject() ?? throw new ArgumentException($"unknown account state {state}");
            var riskVersion = account.RiskVersion;
            if (answer.EnforcedRiskVersion is long enforced && riskVersion <= enforced)
            {
                return new EnforcementResult { Account = account, State = state, RiskVersion = riskVersion, Skipped = "already_enforced" };
            }
            var flow = (string)rule["flow"]!;
            var decision = DecisionOf(account.DecisionsByFlow, flow);
            var reinstate = options.Flows != null && options.Flows.TryGetValue(flow, out var policy) && policy.Reinstate;
            var actions = (rule["on"]![decision] ?? (reinstate ? rule["reinstate"]?[decision] : null))?.AsArray().Select(a => (string)a!).ToList() ?? new List<string>();
            var provider = options.Provider;
            var enforcements = new List<Enforced>();

            async Task Record(string action, string status, string? errorCode = null)
            {
                var body = new EnforcementRequest
                {
                    RiskVersion = riskVersion,
                    Flow = flow,
                    Action = action,
                    StateBefore = state,
                    Status = status,
                    Provider = provider.Name,
                    ErrorCode = errorCode,
                };
                await client.Accounts.RecordEnforcementAsync(account.Id, body, cancellationToken: cancellationToken);
                enforcements.Add(new Enforced(flow, action, status, errorCode));
            }

            if (actions.Count == 0)
            {
                // A decision that needs nothing, allow too, still counts as handled, so the account leaves the unenforced feed.
                await Record("none", "applied");
                return new EnforcementResult { Account = account, State = state, RiskVersion = riskVersion, Enforcements = enforcements };
            }
            var recorded = new HashSet<string>();
            foreach (var action in actions)
            {
                IReadOnlyList<string> alsoRan;
                try
                {
                    alsoRan = await provider.RunAsync(action, string.IsNullOrEmpty(answer.UserId) ? account.Id : answer.UserId, cancellationToken);
                }
                catch (Exception error) when (error is not OperationCanceledException)
                {
                    await Record(action, "failed", FailureCode(provider.Name, error));
                    throw;
                }
                foreach (var ran in alsoRan.Append(action))
                {
                    if (recorded.Add(ran)) await Record(ran, "applied");
                }
            }
            return new EnforcementResult { Account = account, State = state, RiskVersion = riskVersion, Enforcements = enforcements };
        }

        private static string DecisionOf(AccountDecisionsByFlow decisions, string flow) => flow switch
        {
            "account_creation" => decisions.AccountCreation.Value,
            "login" => decisions.Login.Value,
            "purchase" => decisions.Purchase.Value,
            _ => throw new ArgumentException($"unknown flow {flow}"),
        };

        /// <summary>The error_code of a failed action: the provider's code, else its HTTP status, as provider.code.</summary>
        private static string FailureCode(string provider, Exception error)
        {
            var refused = error as ProviderActionException;
            var code = (refused?.Code ?? (error.Data["code"] as string))?.ToLowerInvariant();
            var suffix = code != null && Slug.IsMatch(code)
                ? code
                : refused == null ? "error" : refused.Status == 0 ? "unreachable" : $"http_{refused.Status}";
            var full = $"{provider}.{suffix}";
            return full.Length > 128 ? full.Substring(0, 128) : full;
        }

        /// <summary>Clerk, WorkOS or Auth0 (clerk, workos or auth0), run from the shared action table.</summary>
        public static IActionProvider IdentityProvider(string name, IdentityProviderOptions options)
        {
            var spec = Table["providers"]![name]?.AsObject() ?? throw new ArgumentException($"unknown identity provider {name}", nameof(name));
            if (options == null || string.IsNullOrEmpty(options.Token)) throw new ArgumentException($"{name} needs Token", nameof(options));
            var baseUrl = options.BaseUrl ?? (string)spec["base_url"]!;
            if (baseUrl.Contains("{domain}"))
            {
                if (string.IsNullOrEmpty(options.Domain)) throw new ArgumentException($"{name} needs Domain or BaseUrl", nameof(options));
                baseUrl = baseUrl.Replace("{domain}", options.Domain);
            }
            return new TableProvider(name, spec, baseUrl.TrimEnd('/'), options.Token, options.HttpClient ?? SharedHttp.Value);
        }

        /// <summary>Your own user store: one function per action you support, keyed by action. An action without one fails.</summary>
        public static IActionProvider CustomProvider(IReadOnlyDictionary<string, Func<string, Task>> handlers) => new Custom(handlers);

        private static readonly Lazy<HttpClient> SharedHttp = new(() => new HttpClient { Timeout = TimeSpan.FromSeconds(10) });

        private sealed class Custom : IActionProvider
        {
            private readonly IReadOnlyDictionary<string, Func<string, Task>> handlers;

            public Custom(IReadOnlyDictionary<string, Func<string, Task>> handlers) => this.handlers = handlers;

            public string Name => "custom";

            public async Task<IReadOnlyList<string>> RunAsync(string action, string userId, CancellationToken cancellationToken = default)
            {
                if (!handlers.TryGetValue(action, out var handler)) throw new InvalidOperationException($"CustomProvider has no {action} handler");
                await handler(userId);
                return Array.Empty<string>();
            }
        }

        private sealed class Context
        {
            public required string Name { get; init; }
            public required string Base { get; init; }
            public required string Token { get; init; }
            public required HttpClient Http { get; init; }
            public required string User { get; init; }
            public double WaitedMs { get; set; }
        }

        private sealed class TableProvider : IActionProvider
        {
            private readonly JsonObject spec;
            private readonly string baseUrl;
            private readonly string token;
            private readonly HttpClient http;

            public TableProvider(string name, JsonObject spec, string baseUrl, string token, HttpClient http)
            {
                Name = name;
                this.spec = spec;
                this.baseUrl = baseUrl;
                this.token = token;
                this.http = http;
            }

            public string Name { get; }

            public Task<IReadOnlyList<string>> RunAsync(string action, string userId, CancellationToken cancellationToken = default)
            {
                if (string.IsNullOrEmpty(userId)) throw new ArgumentException("user id must be a non-empty string", nameof(userId));
                var steps = spec["actions"]![action]?.AsArray() ?? throw new ArgumentException($"unknown action {action}", nameof(action));
                var ctx = new Context { Name = Name, Base = baseUrl, Token = token, Http = http, User = userId };
                return RunStepsAsync(steps, ctx, cancellationToken);
            }
        }

        private static async Task<IReadOnlyList<string>> RunStepsAsync(JsonArray steps, Context ctx, CancellationToken cancellationToken)
        {
            var alsoRan = new List<string>();
            foreach (var node in steps)
            {
                var step = node!.AsObject();
                var call = step["call"]!.AsObject();
                if (step["each"] is JsonObject each)
                {
                    foreach (var item in await CollectAsync(each, ctx, cancellationToken)) await SendAsync(call, ctx, item, null, cancellationToken);
                    continue;
                }
                try
                {
                    await SendAsync(call, ctx, null, null, cancellationToken);
                }
                catch (ProviderActionException error) when (
                    step["fallback"] is JsonObject fallback && error.Status == (int)fallback["status"]! && error.Code == (string?)fallback["code"])
                {
                    var fb = step["fallback"]!.AsObject();
                    alsoRan.AddRange(await RunStepsAsync(fb["steps"]!.AsArray(), ctx, cancellationToken));
                    if (fb["reports"] is JsonNode reports) alsoRan.Add((string)reports!);
                }
            }
            return alsoRan;
        }

        /// <summary>Every matching item id across the list's pages, collected before any is acted on.</summary>
        private static async Task<List<string>> CollectAsync(JsonObject each, Context ctx, CancellationToken cancellationToken)
        {
            var list = each["list"]!.AsObject();
            var page = each["page"]!.AsObject();
            var where = each["where"]?.AsObject();
            var kind = (string)page["kind"]!;
            var param = (string)page["param"]!;
            var size = page["size"] is JsonNode s ? (int)s : 0;
            var ids = new List<string>();
            string? cursor = null;
            var maxPages = (int)Limits["max_pages"]!;
            for (var index = 0; index < maxPages; index++)
            {
                var query = new List<KeyValuePair<string, string>>();
                foreach (var (key, value) in list["query"]?.AsObject() ?? new JsonObject()) query.Add(new(key, (string)value!));
                if (kind == "offset") query.Add(new(param, (index * size).ToString(CultureInfo.InvariantCulture)));
                else if (kind == "index") query.Add(new(param, index.ToString(CultureInfo.InvariantCulture)));
                else if (cursor != null) query.Add(new(param, cursor));
                var body = await SendAsync(list, ctx, null, query, cancellationToken);
                var items = body as JsonArray ?? (body as JsonObject)?["data"] as JsonArray ?? new JsonArray();
                foreach (var entry in items)
                {
                    if (entry is not JsonObject record) continue;
                    if (record["id"] is not JsonValue idValue || !idValue.TryGetValue<string>(out var id) || id.Length == 0) continue;
                    var matches = where == null || where.All(w => JsonNode.DeepEquals(record[w.Key], w.Value));
                    if (matches) ids.Add(id);
                }
                if (kind == "cursor")
                {
                    JsonNode? next = body;
                    foreach (var key in page["next"]!.AsArray()) next = (next as JsonObject)?[(string)key!];
                    if (next is not JsonValue nextValue || !nextValue.TryGetValue<string>(out var following) || following.Length == 0) break;
                    cursor = following;
                }
                else if (items.Count < size) break;
            }
            return ids;
        }

        private static JsonNode? Fill(JsonNode? value, string user, string? item) => value switch
        {
            JsonValue v when v.TryGetValue<string>(out var text) => JsonValue.Create(text.Replace("{user}", user).Replace("{item}", item ?? "")),
            JsonArray array => new JsonArray(array.Select(entry => Fill(entry, user, item)).ToArray()),
            JsonObject obj => new JsonObject(obj.Select(p => new KeyValuePair<string, JsonNode?>(p.Key, Fill(p.Value, user, item)))),
            null => null,
            _ => value.DeepClone(),
        };

        private static double? Number(string? text) =>
            double.TryParse(text?.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out var value) && double.IsFinite(value) ? value : null;

        /// <summary>How long a 429 asks to wait, in milliseconds.</summary>
        private static double RateLimitWaitMs(HttpResponseMessage response, int retry)
        {
            var after = Number(Header(response, "Retry-After"));
            if (after is double seconds && seconds >= 0) return seconds * 1000;
            var reset = Number(Header(response, "X-RateLimit-Reset"));
            if (reset is double at) return Math.Max(0, at * 1000 - DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
            return (double)Limits["backoff_ms"]! * Math.Pow(2, retry);
        }

        private static string? Header(HttpResponseMessage response, string name) =>
            response.Headers.TryGetValues(name, out var values) ? values.FirstOrDefault() : null;

        /// <summary>The provider's error code: Auth0's errorCode, WorkOS's code or Clerk's errors[0].code.</summary>
        private static string? ErrorCode(string text)
        {
            try
            {
                if (JsonNode.Parse(text) is not JsonObject body) return null;
                var code = body["errorCode"] ?? body["code"] ?? (body["errors"] as JsonArray)?.FirstOrDefault()?["code"];
                return code is JsonValue v && v.TryGetValue<string>(out var s) && s.Length > 0 ? s : null;
            }
            catch (JsonException)
            {
                return null;
            }
        }

        /// <summary>One admin API call. A 404 is done (null); a 429 is waited out inside the action's budget; any other failure throws.</summary>
        private static async Task<JsonNode?> SendAsync(JsonObject request, Context ctx, string? item, List<KeyValuePair<string, string>>? query, CancellationToken cancellationToken)
        {
            var method = (string)request["method"]!;
            var template = (string)request["path"]!;
            var path = template.Replace("{user}", Uri.EscapeDataString(ctx.User)).Replace("{item}", Uri.EscapeDataString(item ?? ""));
            query ??= (request["query"]?.AsObject() ?? new JsonObject()).Select(p => new KeyValuePair<string, string>(p.Key, (string)p.Value!)).ToList();
            var search = string.Join("&", query.Select(p =>
                Uri.EscapeDataString(p.Key) + "=" + Uri.EscapeDataString(p.Value.Replace("{user}", ctx.User).Replace("{item}", item ?? ""))));
            var url = ctx.Base + path + (search.Length > 0 ? "?" + search : "");
            var body = request["body"] is JsonNode template2 ? Fill(template2, ctx.User, item)!.ToJsonString() : null;
            var label = $"{ctx.Name} {method} {template}";
            var retries = (int)Limits["retries"]!;
            var budget = (double)Limits["budget_ms"]!;
            for (var retry = 0; ; retry++)
            {
                using var message = new HttpRequestMessage(new HttpMethod(method), url);
                message.Headers.TryAddWithoutValidation("Authorization", "Bearer " + ctx.Token);
                message.Headers.TryAddWithoutValidation("Accept", "application/json");
                if (body != null) message.Content = new StringContent(body, Encoding.UTF8, "application/json");
                HttpResponseMessage response;
                string text;
                try
                {
                    response = await ctx.Http.SendAsync(message, cancellationToken);
                    text = await response.Content.ReadAsStringAsync(cancellationToken);
                }
                catch (HttpRequestException error)
                {
                    throw new ProviderActionException(ctx.Name, 0, $"{label} failed: {error.Message}", null, error);
                }
                catch (OperationCanceledException error) when (!cancellationToken.IsCancellationRequested)
                {
                    // HttpClient reports its own timeout as a cancellation; the provider was unreachable.
                    throw new ProviderActionException(ctx.Name, 0, $"{label} timed out", null, error);
                }
                using (response)
                {
                    var status = (int)response.StatusCode;
                    if (status == 429 && retry < retries)
                    {
                        var wait = RateLimitWaitMs(response, retry);
                        if (ctx.WaitedMs + wait <= budget)
                        {
                            ctx.WaitedMs += wait;
                            await Task.Delay(TimeSpan.FromMilliseconds(wait), cancellationToken);
                            continue;
                        }
                    }
                    if (status == 404) return null;
                    if (status < 200 || status >= 300) throw new ProviderActionException(ctx.Name, status, $"{label} answered {status}", ErrorCode(text));
                    if (text.Length == 0) return null;
                    try
                    {
                        return JsonNode.Parse(text);
                    }
                    catch (JsonException)
                    {
                        return null;
                    }
                }
            }
        }
    }
}
