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
    /// One repeatable-read snapshot of retained request diagnostics or webhook attempts. Capture can have gaps and these counts are not a billing ledger. Unattributed keys include older requests and member requests. Queue state is current retained work, not a delivery success denominator.
    /// </summary>
    public class OperationalAggregate
    {
        [JsonProperty("schema_version", Required = Newtonsoft.Json.Required.Always)]
        public string SchemaVersion { get; } = "1";

        [JsonProperty("source", Required = Newtonsoft.Json.Required.Always)]
        public Source Source { get; set; } = default!;

        [JsonProperty("from", Required = Newtonsoft.Json.Required.Always)]
        public string From { get; set; } = default!;

        [JsonProperty("to", Required = Newtonsoft.Json.Required.Always)]
        public string To { get; set; } = default!;

        [JsonProperty("as_of", Required = Newtonsoft.Json.Required.Always)]
        public string AsOf { get; set; } = default!;

        [JsonProperty("retained_from", Required = Newtonsoft.Json.Required.Always)]
        public string RetainedFrom { get; set; } = default!;

        [JsonProperty("retention_days", Required = Newtonsoft.Json.Required.Always)]
        public long RetentionDays { get; set; } = default!;

        [JsonProperty("bucket", Required = Newtonsoft.Json.Required.Always)]
        public RequestAggregateRequestBucket Bucket { get; set; } = default!;

        [JsonProperty("capture", Required = Newtonsoft.Json.Required.Always)]
        public string Capture { get; } = "best_effort";

        [JsonProperty("population", Required = Newtonsoft.Json.Required.Always)]
        public string Population { get; } = "retained_attempts";

        [JsonProperty("totals", Required = Newtonsoft.Json.Required.Always)]
        public OperationalAggregateTotals Totals { get; set; } = new();

        /// <summary>
        /// Nonempty UTC buckets in ascending order. Counts include all retained matching attempts, without the log page limit.
        /// </summary>
        [JsonProperty("buckets", Required = Newtonsoft.Json.Required.Always)]
        public List<OperationalAggregateBucket> Buckets { get; set; } = default!;

        /// <summary>
        /// Current retained delivery queue, independent of the requested attempt window and status filter. Endpoint filter applies. Does not prove customer enforcement execution.
        /// </summary>
        [JsonProperty("queue", Required = Newtonsoft.Json.Required.DisallowNull)]
        public OperationalAggregateQueue? Queue { get; set; }
    }
}