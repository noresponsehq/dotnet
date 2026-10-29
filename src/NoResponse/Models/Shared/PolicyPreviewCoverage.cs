#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    public class PolicyPreviewCoverage
    {
        [JsonProperty("scanned", Required = Newtonsoft.Json.Required.Always)]
        public long Scanned { get; set; } = default!;

        [JsonProperty("eligible", Required = Newtonsoft.Json.Required.Always)]
        public long Eligible { get; set; } = default!;

        [JsonProperty("missing_score", Required = Newtonsoft.Json.Required.Always)]
        public long MissingScore { get; set; } = default!;

        [JsonProperty("unsupported", Required = Newtonsoft.Json.Required.Always)]
        public long Unsupported { get; set; } = default!;

        [JsonProperty("truncated", Required = Newtonsoft.Json.Required.Always)]
        public bool Truncated { get; set; } = default!;

        [JsonProperty("complete_retained_window", Required = Newtonsoft.Json.Required.Always)]
        public bool CompleteRetainedWindow { get; set; } = default!;

        /// <summary>
        /// Deleted, expired and uncaptured records cannot be reconstructed. Counts describe retained evidence and provide no causal estimate of fraud prevented.
        /// </summary>
        [JsonProperty("historical_population_known", Required = Newtonsoft.Json.Required.Always)]
        public bool HistoricalPopulationKnown { get; } = false;
    }
}