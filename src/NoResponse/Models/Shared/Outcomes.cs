#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>
    /// Accounts whose current completed assessment was created in the selected window and remains retained at as_of. The effective account feedback head is observed at as_of, including explicit unknown reports. Customer-reported labels and unlabelled accounts have separate counts. Superseded labels are excluded.
    /// </summary>
    public class Outcomes
    {
        [JsonProperty("population", Required = Newtonsoft.Json.Required.Always)]
        public string Population { get; } = "retained_current_accounts_by_current_assessment_created_at";

        [JsonProperty("denominator", Required = Newtonsoft.Json.Required.Always)]
        public long Denominator { get; set; } = default!;

        [JsonProperty("reported", Required = Newtonsoft.Json.Required.Always)]
        public long Reported { get; set; } = default!;

        [JsonProperty("unreported", Required = Newtonsoft.Json.Required.Always)]
        public long Unreported { get; set; } = default!;

        /// <summary>
        /// Counts only; a zero denominator means no eligible retained records and does not imply a zero rate.
        /// </summary>
        [JsonProperty("label_coverage", Required = Newtonsoft.Json.Required.Always)]
        public Coverage LabelCoverage { get; set; } = new();

        /// <summary>
        /// Current customer-reported labels, not independently established ground truth. Missing keys mean zero. Unknown is an explicit report; absent or expired feedback is unreported.
        /// </summary>
        [JsonProperty("by_outcome", Required = Newtonsoft.Json.Required.Always)]
        public OutcomesByOutcome ByOutcome { get; set; } = new();
    }
}