#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Groups, newest first.
    /// </summary>
    public class GroupList
    {
        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<Group> Items { get; set; } = default!;

        /// <summary>
        /// Pass as before to read the next, older page.
        /// </summary>
        [JsonProperty("next_before", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? NextBefore { get; set; }
    }
}