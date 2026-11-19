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

    public class StageTime
    {
        /// <summary>
        /// ai_typing and analysis appear only on searches recorded before the engine stopped analysing results.
        /// </summary>
        [JsonProperty("stage", Required = Newtonsoft.Json.Required.Always)]
        public Stage Stage { get; set; } = default!;

        [JsonProperty("ms", Required = Newtonsoft.Json.Required.Always)]
        public long Ms { get; set; } = default!;
    }
}