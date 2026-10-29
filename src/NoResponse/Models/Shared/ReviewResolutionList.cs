#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Read immutable review resolution history.
    /// </summary>
    public class ReviewResolutionList
    {
        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<Resolution> Items { get; set; } = default!;

        [JsonProperty("next_before_version", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? NextBeforeVersion { get; set; }
    }
}