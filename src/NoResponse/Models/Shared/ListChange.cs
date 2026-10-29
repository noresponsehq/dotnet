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
    /// A committed bulk list change and bounded notification progress, available while propagation is pending and for 24 hours after all propagation completes. References count an account once per changed list entry and are not unique accounts. No endpoint delivery or customer enforcement is implied by completed.
    /// </summary>
    public class ListChange
    {
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;

        [JsonProperty("state", Required = Newtonsoft.Json.Required.Always)]
        public ListChangeState State { get; set; } = default!;

        [JsonProperty("added", Required = Newtonsoft.Json.Required.Always)]
        public List<ListEntry> Added { get; set; } = default!;

        [JsonProperty("removed", Required = Newtonsoft.Json.Required.Always)]
        public List<string> Removed { get; set; } = default!;

        [JsonProperty("pending_propagations", Required = Newtonsoft.Json.Required.Always)]
        public long PendingPropagations { get; set; } = default!;

        [JsonProperty("processed_account_references", Required = Newtonsoft.Json.Required.Always)]
        public long ProcessedAccountReferences { get; set; } = default!;
    }
}