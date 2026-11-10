#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// What an analyser saw in one image that a finding rests on.
    /// </summary>
    public class ScreeningMediaObservation
    {
        /// <summary>
        /// The image, one of the finding's evidence.
        /// </summary>
        [JsonProperty("media_id", Required = Newtonsoft.Json.Required.Always)]
        public string MediaId { get; set; } = default!;

        /// <summary>
        /// What the analyser saw in the image that supports the finding.
        /// </summary>
        [JsonProperty("description", Required = Newtonsoft.Json.Required.Always)]
        public string Description { get; set; } = default!;

        /// <summary>
        /// Words the analyser read in the image, as it read them. They come from the image's pixels, not from a post's text, so they are not a quote and were not checked against the record.
        /// </summary>
        [JsonProperty("read_text", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ReadText { get; set; }
    }
}