#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Immutable settings snapshots, newest first. history_complete is false when earlier versions predate capture. A restored version is published with a fresh version number. History is removed with the tenant.
    /// </summary>
    public class PolicyHistory
    {
        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<PolicyHistoryItem> Items { get; set; } = default!;

        [JsonProperty("has_more", Required = Newtonsoft.Json.Required.Always)]
        public bool HasMore { get; set; } = default!;

        [JsonProperty("next_before_version", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? NextBeforeVersion { get; set; }

        [JsonProperty("history_complete", Required = Newtonsoft.Json.Required.Always)]
        public bool HistoryComplete { get; set; } = default!;
    }
}