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
    }
}