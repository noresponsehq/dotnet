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
    /// POST /v1/assessments/{assessment_id}/outcomes body: what the provider later learned about the account it assessed. The assessment is named by the path and the tenant by the API key, so neither is a body member. Idempotency-Key is required and binds the body as it does on POST /v1/assessments.
    /// </summary>
    public class AssessmentOutcomeRequest
    {
        /// <summary>
        /// What the provider later learned about the assessed account. confirmed_abuse, account_takeover and chargeback report abuse; false_positive and legitimate withdraw an earlier abuse report of this tenant for the assessment; unknown is recorded only. Outcomes are append-only and the latest one recorded for an assessment wins.
        /// </summary>
        [JsonProperty("outcome", Required = Newtonsoft.Json.Required.Always)]
        public Outcome Outcome { get; set; } = default!;

        /// <summary>
        /// When the provider observed the outcome. Defaults to when NoResponse received it.
        /// </summary>
        [JsonProperty("observed_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ObservedAt { get; set; }

        /// <summary>
        /// Your own opaque reference for the outcome, such as a case or ticket id. Stored as given and not interpreted; keep direct identifiers out of it.
        /// </summary>
        [JsonProperty("external_ref", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ExternalRef { get; set; }

        /// <summary>
        /// Free text for your own records, at most 500 characters. Stored only; it is left out of scoring and answers. Keep personal data out of it. Control characters other than tab and line breaks are refused.
        /// </summary>
        [JsonProperty("notes", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Notes { get; set; }
    }
}