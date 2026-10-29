#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// List retained account reviews.
    /// </summary>
    public class ReviewList
    {
        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<Review> Items { get; set; } = default!;

        [JsonProperty("next_cursor", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? NextCursor { get; set; }
    }
}