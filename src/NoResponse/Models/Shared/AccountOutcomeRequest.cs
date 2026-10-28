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
    /// Record a factual report, not an enforcement action. A known outcome replaces the effective report. unknown alone records uncertainty without changing it; unknown with supersedes explicitly retracts it. supersedes must name the current effective report or the request answers 409 outcome_conflict. Reports remain append-only. Idempotency-Key is required. observed_at defaults to receipt time and cannot be over five minutes ahead.
    /// </summary>
    public class AccountOutcomeRequest
    {
        /// <summary>
        /// What the provider later learned about the assessed account. confirmed_abuse, account_takeover and chargeback report abuse; false_positive and legitimate withdraw an earlier abuse report of this workspace for the assessment; unknown is recorded only. Outcomes are append-only and the latest one recorded for an assessment wins.
        /// </summary>
        [JsonProperty("outcome", Required = Newtonsoft.Json.Required.Always)]
        public Outcome Outcome { get; set; } = default!;

        [JsonProperty("observed_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ObservedAt { get; set; }

        /// <summary>
        /// Your own opaque reference for the outcome, such as a case or ticket id. Stored as given and not interpreted; keep direct identifiers out of it.
        /// </summary>
        [JsonProperty("external_ref", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ExternalRef { get; set; }

        /// <summary>
        /// Opaque, server-assigned id of one recorded outcome: 'out_' and a 26-character Crockford base32 ULID. Unique within the workspace.
        /// </summary>
        [JsonProperty("supersedes", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Supersedes { get; set; }
    }
}