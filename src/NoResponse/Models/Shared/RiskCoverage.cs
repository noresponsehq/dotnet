#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// Sources counted by state, not named.
    /// </summary>
    public class RiskCoverage
    {
        /// <summary>
        /// Sources that gave a definite answer.
        /// </summary>
        [JsonProperty("answered", Required = Newtonsoft.Json.Required.Always)]
        public long Answered { get; set; } = default!;

        /// <summary>
        /// Sources that could not answer.
        /// </summary>
        [JsonProperty("failed", Required = Newtonsoft.Json.Required.Always)]
        public long Failed { get; set; } = default!;

        /// <summary>
        /// Sources still being asked.
        /// </summary>
        [JsonProperty("pending", Required = Newtonsoft.Json.Required.Always)]
        public long Pending { get; set; } = default!;
    }
}