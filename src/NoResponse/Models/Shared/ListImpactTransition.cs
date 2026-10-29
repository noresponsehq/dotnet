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

    public class ListImpactTransition
    {
        [JsonProperty("from", Required = Newtonsoft.Json.Required.Always)]
        public ListImpactTransitionFrom From { get; set; } = default!;

        [JsonProperty("to", Required = Newtonsoft.Json.Required.Always)]
        public ListImpactTransitionFrom To { get; set; } = default!;

        [JsonProperty("count", Required = Newtonsoft.Json.Required.Always)]
        public long Count { get; set; } = default!;
    }
}