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
    /// Screens the group's people no screening has reached and everyone on watch or escalated.
    /// </summary>
    public class ScreeningRequest
    {
        [JsonProperty("depth", Required = Newtonsoft.Json.Required.Always)]
        public ScreeningDepth Depth { get; set; } = default!;

        [JsonProperty("platforms", Required = Newtonsoft.Json.Required.Always)]
        public List<HandlePlatform> Platforms { get; set; } = default!;
    }
}