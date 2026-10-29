#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// Current retained delivery queue, independent of the requested attempt window and status filter. Endpoint filter applies. Does not prove customer enforcement execution.
    /// </summary>
    public class OperationalAggregateQueue
    {
        [JsonProperty("pending", Required = Newtonsoft.Json.Required.Always)]
        public long Pending { get; set; } = default!;

        [JsonProperty("leased", Required = Newtonsoft.Json.Required.Always)]
        public long Leased { get; set; } = default!;

        [JsonProperty("dead_retained", Required = Newtonsoft.Json.Required.Always)]
        public long DeadRetained { get; set; } = default!;

        [JsonProperty("oldest_pending_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? OldestPendingAt { get; set; }
    }
}