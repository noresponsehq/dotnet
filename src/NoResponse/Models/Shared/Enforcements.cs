#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>
    /// Retained customer enforcement report rows created in [from,to), one row per account/risk_version/flow/action. A later applied report can replace failed at the original created_at; retries are not additional attempts here. applied_coverage divides reported_applied by report rows, including explicit action none; it is not independently verified execution, all requested actions, or prevented fraud. Unreported actions are unknown.
    /// </summary>
    public class Enforcements
    {
        [JsonProperty("population", Required = Newtonsoft.Json.Required.Always)]
        public string Population { get; } = "retained_enforcement_reports_by_created_at";

        [JsonProperty("denominator", Required = Newtonsoft.Json.Required.Always)]
        public long Denominator { get; set; } = default!;

        [JsonProperty("accounts", Required = Newtonsoft.Json.Required.Always)]
        public long Accounts { get; set; } = default!;

        [JsonProperty("reported_applied", Required = Newtonsoft.Json.Required.Always)]
        public long ReportedApplied { get; set; } = default!;

        [JsonProperty("reported_failed", Required = Newtonsoft.Json.Required.Always)]
        public long ReportedFailed { get; set; } = default!;

        /// <summary>
        /// Counts only; a zero denominator means no eligible retained records and does not imply a zero rate.
        /// </summary>
        [JsonProperty("applied_coverage", Required = Newtonsoft.Json.Required.Always)]
        public Coverage AppliedCoverage { get; set; } = new();
    }
}