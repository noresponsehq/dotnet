#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// List your private and shared review filters.
    /// </summary>
    public class ReviewViewList
    {
        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<ReviewView> Items { get; set; } = default!;
    }
}