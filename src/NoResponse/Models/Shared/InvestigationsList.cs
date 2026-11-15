#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// List investigation history.
    /// </summary>
    public class InvestigationsList
    {
        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<Investigation> Items { get; set; } = default!;

        [JsonProperty("has_more", Required = Newtonsoft.Json.Required.Always)]
        public bool HasMore { get; set; } = default!;

        [JsonProperty("next_cursor", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public string? NextCursor { get; set; }
    }
}