#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The answer of POST /v1/accounts/{account_id}/enforcements: one enforcement of one of the calling workspace's accounts, kept once per (account, risk_version, flow, action). The first report answers 201 with the new record. A repeat answers 200 with the record as first stored, whatever the repeat's state_before, status, provider or error_code. The one exception is a stored failed record followed by an applied report: the record becomes applied, keeps its id and created_at, takes the applied report's state_before and provider, drops error_code and answers 201, so a successful retry after a failure is recorded. Only an applied record at the account's current risk_version takes the account out of GET /v1/accounts?unenforced=true.
    /// </summary>
    public class Enforcement
    {
        /// <summary>
        /// Opaque, server-assigned id of one recorded enforcement: 'enf_' and 26 Crockford base32 characters. Unique within the workspace.
        /// </summary>
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        /// <summary>
        /// The account_id of the request path.
        /// </summary>
        [JsonProperty("account_id", Required = Newtonsoft.Json.Required.Always)]
        public string AccountId { get; set; } = default!;

        /// <summary>
        /// The account's risk_version the workspace acted on. It can be older than the account's current risk_version: such a record is kept and does not count for unenforced.
        /// </summary>
        [JsonProperty("risk_version", Required = Newtonsoft.Json.Required.Always)]
        public long RiskVersion { get; set; } = default!;

        /// <summary>
        /// The event an assessment is made for: account_creation (a user creates an account), login (a user returns to an existing account) or purchase (a payment or a credit grant). The account's risk is reported with the decision for this flow.
        /// </summary>
        [JsonProperty("flow", Required = Newtonsoft.Json.Required.Always)]
        public Flow Flow { get; set; } = default!;

        /// <summary>
        /// What the workspace did in its identity provider: revoke_sessions (signed the account out everywhere), restrict (limited what the account can do), unrestrict (lifted an earlier restriction), fail_onboarding (stopped an account that was still onboarding), reject_activation (refused to activate a provisional account) or none (decided to take no action).
        /// </summary>
        [JsonProperty("action", Required = Newtonsoft.Json.Required.Always)]
        public EnforcementAction Action { get; set; } = default!;

        /// <summary>
        /// The account's state in the workspace's identity provider just before the action: onboarding, provisional (created, not yet activated), active, restricted or rejected.
        /// </summary>
        [JsonProperty("state_before", Required = Newtonsoft.Json.Required.Always)]
        public EnforcementState StateBefore { get; set; } = default!;

        /// <summary>
        /// applied when the identity provider accepted the action, failed when it refused it or could not be reached. Only applied counts as enforced.
        /// </summary>
        [JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public EnforcementStatus Status { get; set; } = default!;

        /// <summary>
        /// The identity provider the action ran in; custom for the workspace's own user store.
        /// </summary>
        [JsonProperty("provider", Required = Newtonsoft.Json.Required.Always)]
        public EnforcementProvider Provider { get; set; } = default!;

        /// <summary>
        /// Why a failed action failed, as a short code of the workspace's choosing (lower-case letters, digits, '_', '.' and '-'), such as the identity provider's error code. Allowed only with status failed: with status applied it answers 400 invalid_request. Keep personal data out of it.
        /// </summary>
        [JsonProperty("error_code", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ErrorCode { get; set; }

        /// <summary>
        /// When NoResponse first recorded this enforcement, in UTC. A failed record that becomes applied keeps it.
        /// </summary>
        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;
    }
}