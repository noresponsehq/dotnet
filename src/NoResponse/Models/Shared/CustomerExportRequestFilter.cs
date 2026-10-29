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

    public class CustomerExportRequestFilter
    {
        [JsonProperty("action", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Action { get; set; }

        [JsonProperty("actor", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Actor { get; set; }

        [JsonProperty("target_type", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? TargetType { get; set; }

        [JsonProperty("target_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? TargetId { get; set; }

        [JsonProperty("environment", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Models.Shared.Environment? Environment { get; set; }

        [JsonProperty("outcome", Required = Newtonsoft.Json.Required.DisallowNull)]
        public CustomerExportRequestFilterOutcome? Outcome { get; set; }

        [JsonProperty("since", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Since { get; set; }

        [JsonProperty("until", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Until { get; set; }
    }
}