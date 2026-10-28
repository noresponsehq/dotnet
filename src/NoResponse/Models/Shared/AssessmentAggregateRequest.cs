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
    /// Count this environment's assessments created in [from,to) by bucket, from counts kept as each assessment is first answered, not by scanning assessments. Each assessment counts once, by its first answer (the answer served when it was first answered); later revisions and late results do not count it again, and late_flips counts its later account decision changes. from and to start a bucket (an hour, a UTC day, or a week from Monday 00:00 UTC), cover at most 366 days (31 for hour buckets) and from is inside the retention period. The filters are POST /v1/assessments/search's and describe the first answer, so a cell drills down to search with the same filters, revision first and the cell's bucket as from and to. Counts hold no identifier and stay when an assessment or account is deleted or erased; they expire with retention and go with the workspace. Reasons are counted by code and impact band, not by weight. No Idempotency-Key.
    /// </summary>
    public class AssessmentAggregateRequest
    {
        /// <summary>
        /// The first bucket's start.
        /// </summary>
        [JsonProperty("from", Required = Newtonsoft.Json.Required.Always)]
        public string From { get; set; } = default!;

        /// <summary>
        /// The end, excluded: the start of the bucket after the last.
        /// </summary>
        [JsonProperty("to", Required = Newtonsoft.Json.Required.Always)]
        public string To { get; set; } = default!;

        /// <summary>
        /// The period each count covers, in UTC; a week starts on Monday.
        /// </summary>
        [JsonProperty("bucket", Required = Newtonsoft.Json.Required.Always)]
        public Bucket Bucket { get; set; } = default!;

        /// <summary>
        /// At most two dimensions to count by, of which at most one is reason, lane, country or asn. An assessment counts once in each of its reasons and incomplete lanes, so those groups can add up to more than the bucket's count.
        /// </summary>
        [JsonProperty("group_by", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<AggregateDimension>? GroupBy { get; set; }

        /// <summary>
        /// count: each bucket's count and groups. score_histogram: first answers by flow and 5-point score bucket. late_flips: changes of an assessment's account decision for its flow after its first answer, by from, to and delay.
        /// </summary>
        [JsonProperty("metrics", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<AssessmentAggregateRequestMetric>? Metrics { get; set; }

        /// <summary>
        /// The most groups answered: the largest over the whole range, then by value. groups_truncated says when there were more.
        /// </summary>
        [JsonProperty("limit", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Limit { get; set; } = 20;

        /// <summary>
        /// The event an assessment is made for: account_creation (a user creates an account), login (a user returns to an existing account) or purchase (a payment or a credit grant). The account's risk is reported with the decision for this flow.
        /// </summary>
        [JsonProperty("flow", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Flow? Flow { get; set; }

        /// <summary>
        /// complete: final; nothing is still running. A lane that failed or missed its 10 s deadline stays listed in incomplete_sources, and confidence is lower. partial: a recommendation exists and at least one lane is still running or did not settle (see incomplete_sources). pending: nothing usable before the deadline; no recommendation yet, poll or wait for the webhook.
        /// </summary>
        [JsonProperty("status", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Status? Status { get; set; }

        /// <summary>
        /// The recommended action: allow, step_up or review. Your application applies it.
        /// </summary>
        [JsonProperty("recommendation", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Recommendation? Recommendation { get; set; }

        /// <summary>
        /// An assessment's own decision: block when its stored risk.block is true, otherwise its recommendation. It is not the account's decision or held-admission state.
        /// </summary>
        [JsonProperty("decision", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Decision? Decision { get; set; }

        /// <summary>
        /// Which answer of each assessment the filters describe: latest, the latest stored revision, or first, the first answer, as served when the assessment was first answered (POST /v1/assessments/aggregate counts first answers). A cell of an aggregate drills down to POST /v1/assessments/search with the same filters, revision first and the cell's bucket as from and to.
        /// </summary>
        [JsonProperty("revision", Required = Newtonsoft.Json.Required.DisallowNull)]
        public AnswerRevision? Revision { get; set; }
    }
}