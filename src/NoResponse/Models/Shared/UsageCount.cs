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

    public class UsageCount
    {
        [JsonProperty("environment", Required = Newtonsoft.Json.Required.Always)]
        public AuditSettingsEnvironment Environment { get; set; } = default!;

        [JsonProperty("day", Required = Newtonsoft.Json.Required.Always)]
        public string Day { get; set; } = default!;

        /// <summary>
        /// assessments: assessments admitted. webhook_deliveries: events sent to endpoints, one per event per endpoint. key_usage_events: key-usage events accepted.
        /// </summary>
        [JsonProperty("metric", Required = Newtonsoft.Json.Required.Always)]
        public Metric Metric { get; set; } = default!;

        /// <summary>
        /// assessments only.
        /// </summary>
        [JsonProperty("flow", Required = Newtonsoft.Json.Required.DisallowNull)]
        public UsageCountFlow? Flow { get; set; }

        /// <summary>
        /// assessments only.
        /// </summary>
        [JsonProperty("depth", Required = Newtonsoft.Json.Required.DisallowNull)]
        public UsageCountDepth? Depth { get; set; }

        [JsonProperty("count", Required = Newtonsoft.Json.Required.Always)]
        public long Count { get; set; } = default!;
    }
}