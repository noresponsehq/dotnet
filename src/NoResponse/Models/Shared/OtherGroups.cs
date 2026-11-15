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
        /// How many other groups include this person, of which items lists the 20 most recent.
        /// </summary>
        [JsonProperty("total", Required = Newtonsoft.Json.Required.Always)]
        public long Total { get; set; } = default!;

        /// <summary>
        /// This person's memberships in the 20 most recent other groups, newest group first.
        /// </summary>
        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<OtherGroup> Items { get; set; } = default!;
    }
}