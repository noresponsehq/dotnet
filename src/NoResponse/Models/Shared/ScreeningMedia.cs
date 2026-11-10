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
    /// One image or video a collector read, with the post that carried it.
    /// </summary>
    public class ScreeningMedia
    {
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        [JsonProperty("kind", Required = Newtonsoft.Json.Required.Always)]
        public string Kind { get; } = "media";

        [JsonProperty("post_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? PostId { get; set; }

        [JsonProperty("media_type", Required = Newtonsoft.Json.Required.Always)]
        public MediaType MediaType { get; set; } = default!;

        /// <summary>
        /// Where the platform serves the image or video.
        /// </summary>
        [JsonProperty("media_url", Required = Newtonsoft.Json.Required.Always)]
        public string MediaUrl { get; set; } = default!;

        /// <summary>
        /// The caption or alt text.
        /// </summary>
        [JsonProperty("text", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Text { get; set; }

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