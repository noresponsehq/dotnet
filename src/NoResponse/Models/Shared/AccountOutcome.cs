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
    /// Immutable acceptance receipt. risk_version is the account version after this report committed, not the version when a retry reads it. effective_outcome_id names the effective head at that write. The effective expiration may shorten under retention changes. A legitimate report or correction removes only the account-feedback contribution, not unrelated risk.
    /// </summary>
    public class AccountOutcome
    {
        /// <summary>
        /// Opaque, server-assigned id of one recorded outcome: 'out_' and a 26-character Crockford base32 ULID. Unique within the workspace.
        /// </summary>
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        /// <summary>
        /// Your own opaque identifier for the account being checked, scoped to your workspace. Use an id you mint, with no direct identifier (email, phone, name or national id) in it. '|' is accepted so an Auth0 user id such as 'auth0|64abc' passes unchanged; '@', '+', '$' and whitespace are refused by the pattern, as is an exact-shape JWT; a hex digest such as a SHA-256 is allowed, because hashing an internal id is a common pseudonymous scheme. A value such as '07700900123' or 'jane.doe.gmail.com' cannot be told apart from an opaque id. It travels in bodies, and in a URL only as the path segment of GET /v1/accounts/{account_id} and its /assessments. An assessment requested by a provider hook at pre_registration, which has no account yet, carries the server-issued account_id pre: followed by its assessment_id.
        /// </summary>
        [JsonProperty("account_id", Required = Newtonsoft.Json.Required.Always)]
        public string AccountId { get; set; } = default!;

        /// <summary>
        /// What the provider later learned about the assessed account. confirmed_abuse, account_takeover and chargeback report abuse; false_positive and legitimate withdraw an earlier abuse report of this workspace for the assessment; unknown is recorded only. Outcomes are append-only and the latest one recorded for an assessment wins.
        /// </summary>
        [JsonProperty("outcome", Required = Newtonsoft.Json.Required.Always)]
        public Outcome Outcome { get; set; } = default!;

        /// <summary>
        /// Opaque, server-assigned id of one recorded outcome: 'out_' and a 26-character Crockford base32 ULID. Unique within the workspace.
        /// </summary>
        [JsonProperty("supersedes", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Supersedes { get; set; }

        [JsonProperty("observed_at", Required = Newtonsoft.Json.Required.Always)]
        public string ObservedAt { get; set; } = default!;

        [JsonProperty("received_at", Required = Newtonsoft.Json.Required.Always)]
        public string ReceivedAt { get; set; } = default!;

        [JsonProperty("expires_at", Required = Newtonsoft.Json.Required.Always)]
        public string ExpiresAt { get; set; } = default!;

        /// <summary>
        /// Your own opaque reference for the outcome, such as a case or ticket id. Stored as given and not interpreted; keep direct identifiers out of it.
        /// </summary>
        [JsonProperty("external_ref", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ExternalRef { get; set; }

        [JsonProperty("actor", Required = Newtonsoft.Json.Required.Always)]
        public AccountOutcomeActor Actor { get; set; } = new();

        [JsonProperty("risk_version_before", Required = Newtonsoft.Json.Required.Always)]
        public long RiskVersionBefore { get; set; } = default!;

        [JsonProperty("risk_version", Required = Newtonsoft.Json.Required.Always)]
        public long RiskVersion { get; set; } = default!;

        /// <summary>
        /// Opaque, server-assigned id of one recorded outcome: 'out_' and a 26-character Crockford base32 ULID. Unique within the workspace.
        /// </summary>
        [JsonProperty("effective_outcome_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? EffectiveOutcomeId { get; set; }
    }
}