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
    /// Read audit retention and available history.
    /// </summary>
    public class AuditSettings
    {
        [JsonProperty("environment", Required = Newtonsoft.Json.Required.Always)]
        public AuditSettingsEnvironment Environment { get; set; } = default!;

        [JsonProperty("retention_days", Required = Newtonsoft.Json.Required.Always)]
        public long RetentionDays { get; set; } = default!;

        [JsonProperty("oldest_available_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? OldestAvailableAt { get; set; }

        [JsonProperty("capture", Required = Newtonsoft.Json.Required.Always)]
        public string Capture { get; } = "available_events_only";
    }
}