#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>
    /// How many sources this search asked and how many answered, across the lookup stage (sources asked whether an account exists for an identifier) and the platform stage (profiles read on each platform). A source that answered may have found nothing; one that did not answer failed or ran out of time.
    /// </summary>
    public class ScreeningCoverageSources
    {
        /// <summary>
        /// Lookup sources plus platforms asked.
        /// </summary>
        [JsonProperty("asked", Required = Newtonsoft.Json.Required.Always)]
        public long Asked { get; set; } = default!;

        /// <summary>
        /// Lookup sources plus platforms that answered.
        /// </summary>
        [JsonProperty("answered", Required = Newtonsoft.Json.Required.Always)]
        public long Answered { get; set; } = default!;

        [JsonProperty("lookups", Required = Newtonsoft.Json.Required.Always)]
        public SourceCount Lookups { get; set; } = new();

        [JsonProperty("platforms", Required = Newtonsoft.Json.Required.Always)]
        public SourceCount Platforms { get; set; } = new();
    }
}