#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Where else in the environment the person appears, read live from those groups: deleting one removes it here.
    /// </summary>
    public class OtherGroups
    {
        /// <summary>
        /// How many people of other groups match, of which items lists the 20 most recent.
        /// </summary>
        [JsonProperty("total", Required = Newtonsoft.Json.Required.Always)]
        public long Total { get; set; } = default!;

        /// <summary>
        /// The 20 people of the most recent other groups, newest group first.
        /// </summary>
        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<OtherGroup> Items { get; set; } = default!;
    }
}