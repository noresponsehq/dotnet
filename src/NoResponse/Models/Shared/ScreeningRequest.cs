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
    /// Screens the group's people no screening has reached.
    /// </summary>
    public class ScreeningRequest
    {
        [JsonProperty("depth", Required = Newtonsoft.Json.Required.Always)]
        public ScreeningDepth Depth { get; set; } = default!;

        /// <summary>
        /// The platforms to search. Leave it out for a full search: every platform a connector serves in the environment when the screening starts, which the screening then lists.
        /// </summary>
        [JsonProperty("platforms", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<string>? Platforms { get; set; }
    }
}