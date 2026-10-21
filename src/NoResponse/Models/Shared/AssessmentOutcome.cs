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
    /// The 202 answer of POST /v1/assessments/{assessment_id}/outcomes. A replay with the same Idempotency-Key and body answers the same record.
    /// </summary>
    public class AssessmentOutcome
    {
        /// <summary>
        /// Opaque, server-assigned id of one recorded outcome: 'out_' and a 26-character Crockford base32 ULID. Unique within the tenant.
        /// </summary>
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        /// <summary>
        /// Opaque, server-assigned assessment id: 'asm_' and a 26-character Crockford base32 ULID. Unique within the tenant; another tenant's assessment id answers 404.
        /// </summary>
        [JsonProperty("assessment_id", Required = Newtonsoft.Json.Required.Always)]
        public string AssessmentId { get; set; } = default!;

        /// <summary>
        /// What the provider later learned about the assessed account. confirmed_abuse, account_takeover and chargeback report abuse; false_positive and legitimate withdraw an earlier abuse report of this tenant for the assessment; unknown is recorded only. Outcomes are append-only and the latest one recorded for an assessment wins.
        /// </summary>
        [JsonProperty("outcome", Required = Newtonsoft.Json.Required.Always)]
        public Outcome Outcome { get; set; } = default!;

        /// <summary>
        /// When NoResponse first recorded the outcome.
        /// </summary>
        [JsonProperty("received_at", Required = Newtonsoft.Json.Required.Always)]
        public string ReceivedAt { get; set; } = default!;
    }
}