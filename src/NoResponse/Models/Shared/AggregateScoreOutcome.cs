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
    /// First answers of one flow whose risk.score is in [score_min, score_max] and whose assessment's latest reported outcome (the latest received) is outcome. Counted when the first answer is counted and moved when a later outcome wins; outcomes reported before the first answer was counted are counted with it. An outcome reported before this count existed is counted only once another outcome is reported for the assessment.
    /// </summary>
    public class AggregateScoreOutcome
    {
        /// <summary>
        /// The event an assessment is made for: account_creation (a user creates an account), login (a user returns to an existing account) or purchase (a payment or a credit grant). The account's risk is reported with the decision for this flow.
        /// </summary>
        [JsonProperty("flow", Required = Newtonsoft.Json.Required.Always)]
        public Flow Flow { get; set; } = default!;

        /// <summary>
        /// A multiple of 5.
        /// </summary>
        [JsonProperty("score_min", Required = Newtonsoft.Json.Required.Always)]
        public long ScoreMin { get; set; } = default!;

        [JsonProperty("score_max", Required = Newtonsoft.Json.Required.Always)]
        public long ScoreMax { get; set; } = default!;

        /// <summary>
        /// What the provider later learned about the assessed account. confirmed_abuse, account_takeover and chargeback report abuse; false_positive and legitimate withdraw an earlier abuse report of this workspace for the assessment; unknown is recorded only. Outcomes are append-only and the latest one recorded for an assessment wins.
        /// </summary>
        [JsonProperty("outcome", Required = Newtonsoft.Json.Required.Always)]
        public Outcome Outcome { get; set; } = default!;

        [JsonProperty("count", Required = Newtonsoft.Json.Required.Always)]
        public long Count { get; set; } = default!;
    }
}