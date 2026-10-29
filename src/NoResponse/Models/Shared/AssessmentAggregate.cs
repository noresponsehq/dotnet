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
    /// Body of the 200 answer to POST /v1/assessments/aggregate: one entry per bucket of [from,to), empty buckets included. Counts are of first answers and stay when an assessment or account is deleted or erased; search returns only assessments still retained.
    /// </summary>
    public class AssessmentAggregate
    {
        /// <summary>
        /// Version of the body shape.
        /// </summary>
        [JsonProperty("schema_version", Required = Newtonsoft.Json.Required.Always)]
        public string SchemaVersion { get; } = "1";

        [JsonProperty("from", Required = Newtonsoft.Json.Required.Always)]
        public string From { get; set; } = default!;

        [JsonProperty("to", Required = Newtonsoft.Json.Required.Always)]
        public string To { get; set; } = default!;

        /// <summary>
        /// The period each count covers: an hour, or a day or a week (from Monday) in time_zone. Hour buckets are whole UTC hours in any time zone.
        /// </summary>
        [JsonProperty("bucket", Required = Newtonsoft.Json.Required.Always)]
        public Bucket Bucket { get; set; } = default!;

        [JsonProperty("group_by", Required = Newtonsoft.Json.Required.Always)]
        public List<AggregateDimension> GroupBy { get; set; } = default!;

        [JsonProperty("metrics", Required = Newtonsoft.Json.Required.Always)]
        public List<AssessmentAggregateMetric> Metrics { get; set; } = default!;

        /// <summary>
        /// The time zone whose local midnight started each day and week bucket, as requested; UTC when the request named none.
        /// </summary>
        [JsonProperty("time_zone", Required = Newtonsoft.Json.Required.Always)]
        public string TimeZone { get; set; } = default!;

        /// <summary>
        /// The calling workspace's configured retention period in days. Default 365 (12 months), which is also the maximum: a workspace may only shorten it, to at least 1 day. Each record expires from its own created_at; see assessment expires_at.
        /// </summary>
        [JsonProperty("retention_days", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? RetentionDays { get; set; } = 365;

        /// <summary>
        /// true when more groups had counts than limit allowed.
        /// </summary>
        [JsonProperty("groups_truncated", Required = Newtonsoft.Json.Required.Always)]
        public bool GroupsTruncated { get; set; } = default!;

        /// <summary>
        /// Captured in the same database snapshot as the counts. Capture start times describe when counting began, not the earliest creation time counted: an older assessment may first answer later. Older outcomes are not inferred to be absent. Erased detail cannot be returned by drill-down even while its anonymous count remains.
        /// </summary>
        [JsonProperty("coverage", Required = Newtonsoft.Json.Required.DisallowNull)]
        public AssessmentAggregateCoverage? Coverage { get; set; }

        [JsonProperty("buckets", Required = Newtonsoft.Json.Required.Always)]
        public List<AggregateBucket> Buckets { get; set; } = default!;
    }
}