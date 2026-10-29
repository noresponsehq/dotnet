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
    /// Three separately defined populations with independent denominators. Interpret each section within its own population; their totals do not form a conversion funnel. All counts and retention come from one snapshot. Requests above the population or query budget are refused without truncation.
    /// </summary>
    public class WorkspaceOperationsSummary
    {
        /// <summary>
        /// The workspace id, which is also its live environment's workspace id; the test environment's is the same id with :test appended. A workspace created through the API is tnt_ and a 26-character ULID.
        /// </summary>
        [JsonProperty("workspace_id", Required = Newtonsoft.Json.Required.Always)]
        public string WorkspaceId { get; set; } = default!;

        [JsonProperty("environment", Required = Newtonsoft.Json.Required.Always)]
        public AuditSettingsEnvironment Environment { get; set; } = default!;

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

        [JsonProperty("row_budget", Required = Newtonsoft.Json.Required.Always)]
        public long RowBudget { get; } = 100000;

        [JsonProperty("records_read", Required = Newtonsoft.Json.Required.Always)]
        public long RecordsRead { get; set; } = default!;

        [JsonProperty("truncated", Required = Newtonsoft.Json.Required.Always)]
        public bool Truncated { get; } = false;

        /// <summary>
        /// Accounts whose current completed assessment was created in the selected window and remains retained at as_of. The effective account feedback head is observed at as_of, including explicit unknown reports. Customer-reported labels and unlabelled accounts have separate counts. Superseded labels are excluded.
        /// </summary>
        [JsonProperty("outcomes", Required = Newtonsoft.Json.Required.Always)]
        public Outcomes Outcomes { get; set; } = new();

        /// <summary>
        /// Retained customer enforcement report rows created in [from,to), one row per account/risk_version/flow/action. A later applied report can replace failed at the original created_at; retries are not additional attempts here. applied_coverage divides reported_applied by report rows, including explicit action none; it is not independently verified execution, all requested actions, or prevented fraud. Unreported actions are unknown.
        /// </summary>
        [JsonProperty("enforcements", Required = Newtonsoft.Json.Required.Always)]
        public Enforcements Enforcements { get; set; } = new();

        /// <summary>
        /// Reviews created in [from,to) whose review and current account assessment remain retained. Status and assignment are current at as_of, not a transition history. label_coverage numerator is resolved reviews labeled benign or abuse; denominator is resolved reviews. Inconclusive is not a known outcome. Review conclusions do not change account feedback or enforcement.
        /// </summary>
        [JsonProperty("reviews", Required = Newtonsoft.Json.Required.Always)]
        public Models.Shared.Reviews Reviews { get; set; } = new();
    }
}