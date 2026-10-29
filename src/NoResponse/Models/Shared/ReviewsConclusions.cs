#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// Current resolution of each resolved retained review; prior resolutions and reopened reviews do not count. Missing keys mean zero. These investigation conclusions are independent of account feedback.
    /// </summary>
    public class ReviewsConclusions
    {
        [JsonProperty("benign", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Benign { get; set; }

        [JsonProperty("abuse", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Abuse { get; set; }

        [JsonProperty("inconclusive", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Inconclusive { get; set; }
    }
}