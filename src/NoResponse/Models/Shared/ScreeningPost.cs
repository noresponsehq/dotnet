#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

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

        /// <summary>
        /// A place a post names, as the platform shows it.
        /// </summary>
        [JsonProperty("place", Required = Newtonsoft.Json.Required.DisallowNull)]
        public PostPlace? Place { get; set; }

        /// <summary>
        /// The handles of the accounts the post tags, on its own platform.
        /// </summary>
        [JsonProperty("tagged", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<string>? Tagged { get; set; }

        /// <summary>
        /// How many likes the platform shows for the post.
        /// </summary>
        [JsonProperty("likes", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Likes { get; set; }

        /// <summary>
        /// How many comments the platform shows for the post.
        /// </summary>
        [JsonProperty("comments", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Comments { get; set; }

        /// <summary>
        /// How many views or plays the platform shows for the post.
        /// </summary>
        [JsonProperty("views", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Views { get; set; }

        /// <summary>
        /// How many times the platform shows the post was shared or reposted.
        /// </summary>
        [JsonProperty("shares", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Shares { get; set; }

        /// <summary>
        /// The language the platform says the post is in, as the platform codes it, such as en or pt-BR.
        /// </summary>
        [JsonProperty("language", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Language { get; set; }

        /// <summary>
        /// true when the platform marks the post as edited after it went up. Absent when it does not mark it.
        /// </summary>
        [JsonProperty("edited", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? Edited { get; set; }

        /// <summary>
        /// true when the platform marks the post as a paid partnership or an ad. Absent when it does not mark it.
        /// </summary>
        [JsonProperty("sponsored", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? Sponsored { get; set; }

        /// <summary>
        /// true when the platform labels the post as made or changed with AI. Absent when it does not label it.
        /// </summary>
        [JsonProperty("ai_generated", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? AiGenerated { get; set; }

        /// <summary>
        /// The app or device the platform says the post was made with.
        /// </summary>
        [JsonProperty("posted_with", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? PostedWith { get; set; }

        /// <summary>
        /// Everything else the platform sent about the post, under the platform's own names and as it typed them: counts, ids of linked posts and accounts, links, media details, labels and notices. The fields above are not repeated here. Nothing that describes the account the screening read with, such as whether it liked or saved the post, is ever here.
        /// </summary>
        [JsonProperty("stated", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Dictionary<string, object>? Stated { get; set; }

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