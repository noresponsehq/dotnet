#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    public class OperationalAggregateBucket
    {
        [JsonProperty("start", Required = Newtonsoft.Json.Required.Always)]
        public string Start { get; set; } = default!;

        [JsonProperty("attempts", Required = Newtonsoft.Json.Required.Always)]
        public long Attempts { get; set; } = default!;

        [JsonProperty("succeeded", Required = Newtonsoft.Json.Required.Always)]
        public long Succeeded { get; set; } = default!;

        [JsonProperty("failed", Required = Newtonsoft.Json.Required.Always)]
        public long Failed { get; set; } = default!;

        [JsonProperty("client_errors", Required = Newtonsoft.Json.Required.Always)]
        public long ClientErrors { get; set; } = default!;

        [JsonProperty("server_errors", Required = Newtonsoft.Json.Required.Always)]
        public long ServerErrors { get; set; } = default!;

        [JsonProperty("replays", Required = Newtonsoft.Json.Required.Always)]
        public long Replays { get; set; } = default!;

        [JsonProperty("incomplete_writes", Required = Newtonsoft.Json.Required.Always)]
        public long IncompleteWrites { get; set; } = default!;

        [JsonProperty("unattributed_keys", Required = Newtonsoft.Json.Required.Always)]
        public long UnattributedKeys { get; set; } = default!;

        /// <summary>
        /// Continuous percentile over retained attempts. Omitted when there are no captured samples.
        /// </summary>
        [JsonProperty("p50_ms", Required = Newtonsoft.Json.Required.DisallowNull)]
        public double? P50Ms { get; set; }

        /// <summary>
        /// Continuous percentile over retained attempts. Omitted when there are no captured samples.
        /// </summary>
        [JsonProperty("p95_ms", Required = Newtonsoft.Json.Required.DisallowNull)]
        public double? P95Ms { get; set; }

        /// <summary>
        /// Continuous percentile over retained attempts. Omitted when there are no captured samples.
        /// </summary>
        [JsonProperty("p99_ms", Required = Newtonsoft.Json.Required.DisallowNull)]
        public double? P99Ms { get; set; }
    }
}