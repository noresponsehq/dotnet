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

    public class Signal
    {
        /// <summary>
        /// Signal kind, for example account_presence or breach_exposure. New kinds may appear; ignore any you do not know.
        /// </summary>
        [JsonProperty("type", Required = Newtonsoft.Json.Required.Always)]
        public string Type { get; set; } = default!;

        /// <summary>
        /// Which submitted identifier a signal concerns. ip is a selector of breach_exposure only.
        /// </summary>
        [JsonProperty("identifier", Required = Newtonsoft.Json.Required.Always)]
        public IdentifierKind Identifier { get; set; } = default!;

        /// <summary>
        /// Per-signal outcome. A source that timed out, was blocked or errored reports blocked, unavailable or inconclusive with a reason and is listed in incomplete_sources; not_found is a definite absence.
        /// </summary>
        [JsonProperty("outcome", Required = Newtonsoft.Json.Required.Always)]
        public SignalOutcome Outcome { get; set; } = default!;

        /// <summary>
        /// Opaque, tenant-scoped projection of the source that produced a signal. It stays stable for your tenant while the projection key is unchanged and reveals no upstream provider, feed, breach table or source record.
        /// </summary>
        [JsonProperty("source_id", Required = Newtonsoft.Json.Required.Always)]
        public string SourceId { get; set; } = default!;

        /// <summary>
        /// Why a signal or source did not produce a definite found/not_found. timed_out means the source missed the request deadline; a continuation may still settle it. deferred means the (source, identifier) pair is async-only and was not attempted on the synchronous path; a continuation settles it.
        /// </summary>
        [JsonProperty("reason", Required = Newtonsoft.Json.Required.DisallowNull)]
        public FailureReason? Reason { get; set; }

        /// <summary>
        /// Opaque pointer to evidence NoResponse holds for this signal. It carries no breach value, credential or page body.
        /// </summary>
        [JsonProperty("evidence_ref", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? EvidenceRef { get; set; }

        /// <summary>
        /// When the evidence was first observed.
        /// </summary>
        [JsonProperty("observed_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ObservedAt { get; set; }

        /// <summary>
        /// When the source was last queried for this identifier.
        /// </summary>
        [JsonProperty("last_checked_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? LastCheckedAt { get; set; }

        /// <summary>
        /// Freshness class of the evidence behind a signal. unknown whenever no evidence was obtained.
        /// </summary>
        [JsonProperty("freshness", Required = Newtonsoft.Json.Required.Always)]
        public Freshness Freshness { get; set; } = default!;

        /// <summary>
        /// Facts about the number only: line type, carrier and country. The upstream provider, its scores and its verdicts stay internal.
        /// </summary>
        [JsonProperty("enrichment", Required = Newtonsoft.Json.Required.DisallowNull)]
        public PhoneEnrichment? Enrichment { get; set; }
    }
}