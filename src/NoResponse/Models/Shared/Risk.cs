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
    /// The one risk decision for this event. score is 0 (safe) to 100 (risky): the sum of the weights of the reasons that fired, clamped to 0 to 100, from hand-set versioned weights. Every answer with a recommendation carries it, and the top-level recommendation equals risk.recommendation. It names no source or provider.
    /// </summary>
    public class Risk
    {
        [JsonProperty("score", Required = Newtonsoft.Json.Required.Always)]
        public long Score { get; set; } = default!;

        /// <summary>
        /// low 0-29, medium 30-59, high 60-84, critical 85-100.
        /// </summary>
        [JsonProperty("band", Required = Newtonsoft.Json.Required.Always)]
        public Band Band { get; set; } = default!;

        /// <summary>
        /// The recommended action: allow, step_up or review. Your application applies it.
        /// </summary>
        [JsonProperty("recommendation", Required = Newtonsoft.Json.Required.Always)]
        public Recommendation Recommendation { get; set; } = default!;

        /// <summary>
        /// How much of the planned evidence answered; it says nothing about risk.
        /// </summary>
        [JsonProperty("confidence", Required = Newtonsoft.Json.Required.Always)]
        public RiskConfidence Confidence { get; set; } = default!;

        /// <summary>
        /// Version of the hand-authored heuristic weights that produced the recommendation, for example 'risk-2026-10.7'. A tenant threshold override adds a +t&lt;N&gt; suffix, e.g. 'risk-2026-10.7+t3'.
        /// </summary>
        [JsonProperty("policy_version", Required = Newtonsoft.Json.Required.Always)]
        public string PolicyVersion { get; set; } = default!;

        /// <summary>
        /// Labels the v0 policy as hand-authored and not statistically validated.
        /// </summary>
        [JsonProperty("policy_basis", Required = Newtonsoft.Json.Required.Always)]
        public string PolicyBasis { get; } = "heuristic_unvalidated";

        /// <summary>
        /// Every reason that fired, highest weight first. The weights sum to score before clamping.
        /// </summary>
        [JsonProperty("reasons", Required = Newtonsoft.Json.Required.Always)]
        public List<RiskReason> Reasons { get; set; } = default!;

        /// <summary>
        /// A few typed facts per identifier. Every fact is optional: absent means the evidence did not answer and makes no claim, while false or 0 is a definite answer. Lanes that do not exist yet leave their members absent.
        /// </summary>
        [JsonProperty("summary", Required = Newtonsoft.Json.Required.DisallowNull)]
        public RiskSummary? Summary { get; set; }

        /// <summary>
        /// Sources counted by state, not named.
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