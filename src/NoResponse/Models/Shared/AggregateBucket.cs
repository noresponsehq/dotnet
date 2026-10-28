#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    public class AggregateBucket
    {
        [JsonProperty("start", Required = Newtonsoft.Json.Required.Always)]
        public string Start { get; set; } = default!;

        /// <summary>
        /// Assessments first answered whose creation falls in the bucket. With metric count.
        /// </summary>
        [JsonProperty("count", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Count { get; set; }

        /// <summary>
        /// With metric count and group_by: the bucket's counts of the groups answered, largest group first.
        /// </summary>
        [JsonProperty("groups", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<AggregateGroup>? Groups { get; set; }

        [JsonProperty("score_histogram", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<AggregateScoreCount>? ScoreHistogram { get; set; }

        [JsonProperty("late_flips", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<AggregateLateFlip>? LateFlips { get; set; }
    }
}