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
    /// The one risk decision for this event. score is 0 (safe) to 100 (risky), set by the reasons that fired under a hand-set, versioned heuristic policy. Each reason carries its direction and impact band; the policy's weights are not published. Every answer with a recommendation carries it, and the top-level recommendation equals risk.recommendation. It names no source or provider.
    /// </summary>
    public class Risk
    {
        [JsonProperty("score", Required = Newtonsoft.Json.Required.Always)]
        public long Score { get; set; } = default!;

        [JsonProperty("band", Required = Newtonsoft.Json.Required.Always)]
        public Band Band { get; set; } = default!;

        /// <summary>
        /// The recommended action: allow, step_up or review. Your application applies it.
        /// </summary>
        [JsonProperty("recommendation", Required = Newtonsoft.Json.Required.Always)]
        public Recommendation Recommendation { get; set; } = default!;

        /// <summary>
        /// true when the workspace's block threshold is set and score reached it. A provider hook can deny the account creation on it; the provider decides and enforces. It does not change recommendation. It is false while blocking is off, the default, and a body from before the flag existed omits it: read absent as false.
        /// </summary>
        [JsonProperty("block", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? Block { get; set; }

        /// <summary>
        /// The block threshold this answer used: block is true exactly when score is at least this value. Absent while blocking is off. NoResponse sets it per workspace on request, at or above the review threshold.
        /// </summary>
        [JsonProperty("block_threshold", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? BlockThreshold { get; set; }

        /// <summary>
        /// The score at which this answer's flow starts to recommend step_up. Absent from an answer stored before it was recorded.
        /// </summary>
        [JsonProperty("step_up_threshold", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? StepUpThreshold { get; set; }

        /// <summary>
        /// The score at which this answer's flow starts to recommend review. An answer nothing answered recommends review below it, with confidence unavailable. Absent from an answer stored before it was recorded.
        /// </summary>
        [JsonProperty("review_threshold", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? ReviewThreshold { get; set; }

        /// <summary>
        /// How much of the planned evidence answered; it says nothing about risk.
        /// </summary>
        [JsonProperty("confidence", Required = Newtonsoft.Json.Required.Always)]
        public RiskConfidence Confidence { get; set; } = default!;

        /// <summary>
        /// Version of the hand-authored heuristic policy that produced the recommendation, for example 'risk-2026-10.7'. A workspace threshold override adds a +t&lt;N&gt; suffix, e.g. 'risk-2026-10.7+t3'.
        /// </summary>
        [JsonProperty("policy_version", Required = Newtonsoft.Json.Required.Always)]
        public string PolicyVersion { get; set; } = default!;

        /// <summary>
        /// Labels the v0 policy as hand-authored and not statistically validated.
        /// </summary>
        [JsonProperty("policy_basis", Required = Newtonsoft.Json.Required.Always)]
        public string PolicyBasis { get; } = "heuristic_unvalidated";

        /// <summary>
        /// Every reason that fired: risk-raising reasons first, then neutral, then risk-lowering, each strongest impact band first and then by code. The policy's numeric weights are kept private, so a reason carries only its direction and impact band.
        /// </summary>
        [JsonProperty("reasons", Required = Newtonsoft.Json.Required.Always)]
        public List<RiskReason> Reasons { get; set; } = default!;

        /// <summary>
        /// A few typed facts per identifier. Every fact is optional: absent means the evidence did not answer and makes no claim, while false or 0 is a definite answer. Lanes that do not exist yet leave their members absent.
        /// </summary>
        [JsonProperty("summary", Required = Newtonsoft.Json.Required.DisallowNull)]
        public RiskSummary? Summary { get; set; }

        /// <summary>
        /// Counts of planned source-identifier pairs by evidence outcome, deduplicated across signals and incomplete entries. These counts do not measure identity agreement or prove that processing is still running. A lane that was not searched is not an answered source.
        /// </summary>
        [JsonProperty("coverage", Required = Newtonsoft.Json.Required.Always)]
        public RiskCoverage Coverage { get; set; } = new();

        /// <summary>
        /// Detailed view only (response_mode=detailed, which needs the detailed scope); a standard answer leaves it out. Merged observations without provider, source, origin, raw breach record, value or score.
        /// </summary>
        [JsonProperty("details", Required = Newtonsoft.Json.Required.DisallowNull)]
        public RiskDetails? Details { get; set; }
    }
}