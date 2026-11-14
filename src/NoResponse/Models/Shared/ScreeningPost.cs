#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// One post a collector read, with its permalink and time.
    /// </summary>
    public class ScreeningPost
    {
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        [JsonProperty("kind", Required = Newtonsoft.Json.Required.Always)]
        public string Kind { get; } = "post";

        [JsonProperty("text", Required = Newtonsoft.Json.Required.Always)]
        public string Text { get; set; } = default!;

        [JsonProperty("reply_to", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ReplyTo { get; set; }

        /// <summary>
        /// The permalink of the post this one reposts. text then holds that post's author's words, not the person's.
        /// </summary>
        [JsonProperty("repost_of", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? RepostOf { get; set; }

        /// <summary>
        /// The permalink of the post this one quotes. text holds the person's own words alone.
        /// </summary>
        [JsonProperty("quote_of", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? QuoteOf { get; set; }

        [JsonProperty("profile_id", Required = Newtonsoft.Json.Required.Always)]
        public string ProfileId { get; set; } = default!;

        [JsonProperty("permalink", Required = Newtonsoft.Json.Required.Always)]
        public string Permalink { get; set; } = default!;

        [JsonProperty("posted_at", Required = Newtonsoft.Json.Required.Always)]
        public string PostedAt { get; set; } = default!;

        /// <summary>
        /// SHA-256 of the screenshot taken of a flagged item.
        /// </summary>
        [JsonProperty("screenshot_sha256", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ScreenshotSha256 { get; set; }

        /// <summary>
        /// When the screenshot was taken.
        /// </summary>
        [JsonProperty("captured_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? CapturedAt { get; set; }
    }
}