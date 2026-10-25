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
    /// POST /v1/accounts/{account_id}/enforcements body: what the workspace did to the account in its identity provider after reading the account's risk at risk_version. The account is named by the path and the workspace by the API key, so neither is a body member. An enforcement is the workspace's own action, not an outcome: what the workspace later learned about an assessment goes to POST /v1/assessments/{assessment_id}/outcomes. The record's key is (account, risk_version, flow, action), so a retry of the same report is safe and needs no Idempotency-Key.
    /// </summary>
    public class EnforcementRequest
    {
        /// <summary>
        /// The account's risk_version the workspace acted on, as read from GET /v1/accounts/{account_id}, GET /v1/accounts, an assessment answer or account.risk_changed. A version older than the account's current one is accepted and stored; a version newer than the current one answers 409 risk_version_ahead.
        /// </summary>
        [JsonProperty("risk_version", Required = Newtonsoft.Json.Required.Always)]
        public long RiskVersion { get; set; } = default!;

        /// <summary>
        /// The event an assessment is made for: account_creation (a user creates an account), login (a user returns to an existing account) or purchase (a payment or a credit grant). The account's risk is reported with the decision for this flow.
        /// </summary>
        [JsonProperty("flow", Required = Newtonsoft.Json.Required.Always)]
        public Flow Flow { get; set; } = default!;

        /// <summary>
        /// What the workspace did in its identity provider: revoke_sessions (signed the account out everywhere), restrict (limited what the account can do), unrestrict (lifted an earlier restriction), fail_onboarding (stopped an account that was still onboarding), reject_activation (refused to activate a provisional account), activate (let a held onboarding or provisional account in) or none (decided to take no action).
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
    }
}