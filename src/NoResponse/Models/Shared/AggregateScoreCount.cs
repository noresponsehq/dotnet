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
    /// First answers of one flow whose risk.score is in [score_min, score_max]: 0-4, 5-9 up to 95-100. A pending answer has no score.
    /// </summary>
    public class AggregateScoreCount
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

        [JsonProperty("count", Required = Newtonsoft.Json.Required.Always)]
        public long Count { get; set; } = default!;
    }
}