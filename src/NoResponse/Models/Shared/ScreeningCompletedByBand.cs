#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// Screened people by the band of their most severe finding.
    /// </summary>
    public class ScreeningCompletedByBand
    {
        [JsonProperty("critical", Required = Newtonsoft.Json.Required.Always)]
        public long Critical { get; set; } = default!;

        [JsonProperty("high", Required = Newtonsoft.Json.Required.Always)]
        public long High { get; set; } = default!;

        [JsonProperty("moderate", Required = Newtonsoft.Json.Required.Always)]
        public long Moderate { get; set; } = default!;

        [JsonProperty("low", Required = Newtonsoft.Json.Required.Always)]
        public long Low { get; set; } = default!;

        [JsonProperty("info", Required = Newtonsoft.Json.Required.Always)]
        public long Info { get; set; } = default!;
    }
}