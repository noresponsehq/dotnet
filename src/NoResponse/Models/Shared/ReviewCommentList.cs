#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Read retained review comments.
    /// </summary>
    public class ReviewCommentList
    {
        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<ReviewComment> Items { get; set; } = default!;

        [JsonProperty("next_cursor", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? NextCursor { get; set; }
    }
}