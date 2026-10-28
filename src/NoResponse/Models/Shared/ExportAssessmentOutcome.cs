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
    /// Structured fields of a retained per-assessment outcome. Legacy notes remain private as promised by the existing outcome contract.
    /// </summary>
    public class ExportAssessmentOutcome
    {
        /// <summary>
        /// Opaque, server-assigned id of one recorded outcome: 'out_' and a 26-character Crockford base32 ULID. Unique within the workspace.
        /// </summary>
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        /// <summary>
        /// Opaque, server-assigned assessment id: 'asm_' and a 26-character Crockford base32 ULID. Unique within the workspace; another workspace's assessment id answers 404.
        /// </summary>
        [JsonProperty("assessment_id", Required = Newtonsoft.Json.Required.Always)]
        public string AssessmentId { get; set; } = default!;

        /// <summary>
        /// What the provider later learned about the assessed account. confirmed_abuse, account_takeover and chargeback report abuse; false_positive and legitimate withdraw an earlier abuse report of this workspace for the assessment; unknown is recorded only. Outcomes are append-only and the latest one recorded for an assessment wins.
        /// </summary>
        [JsonProperty("outcome", Required = Newtonsoft.Json.Required.Always)]
        public Outcome Outcome { get; set; } = default!;

        [JsonProperty("observed_at", Required = Newtonsoft.Json.Required.Always)]
        public string ObservedAt { get; set; } = default!;

        [JsonProperty("received_at", Required = Newtonsoft.Json.Required.Always)]
        public string ReceivedAt { get; set; } = default!;

        /// <summary>
        /// Your own opaque reference for the outcome, such as a case or ticket id. Stored as given and not interpreted; keep direct identifiers out of it.
        /// </summary>
        [JsonProperty("external_ref", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ExternalRef { get; set; }
    }
}