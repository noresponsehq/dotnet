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
        /// Deprecated: leave it out. Without it the screening is a full search: every platform a connector serves in the environment when the screening starts, which the screening then lists, each searched only with the identifiers its connector takes. A list sent here still limits the screening until a later contract release removes the field.
        /// </summary>
        [Obsolete("This field will be removed in a future release, please migrate away from it as soon as possible")]
        [JsonProperty("platforms", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<string>? Platforms { get; set; }
    }
}