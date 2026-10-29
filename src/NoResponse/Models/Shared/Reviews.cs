#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>
    /// Reviews created in [from,to) whose review and current account assessment remain retained. Status and assignment are current at as_of, not a transition history. label_coverage numerator is resolved reviews labeled benign or abuse; denominator is resolved reviews. Inconclusive is not a known outcome. Review conclusions do not change account feedback or enforcement.
    /// </summary>
    public class Reviews
    {
        [JsonProperty("population", Required = Newtonsoft.Json.Required.Always)]
        public string Population { get; } = "retained_reviews_by_created_at";

        [JsonProperty("denominator", Required = Newtonsoft.Json.Required.Always)]
        public long Denominator { get; set; } = default!;

        [JsonProperty("open", Required = Newtonsoft.Json.Required.Always)]
        public long Open { get; set; } = default!;

        [JsonProperty("in_progress", Required = Newtonsoft.Json.Required.Always)]
        public long InProgress { get; set; } = default!;

        [JsonProperty("resolved", Required = Newtonsoft.Json.Required.Always)]
        public long Resolved { get; set; } = default!;

        [JsonProperty("assigned", Required = Newtonsoft.Json.Required.Always)]
        public long Assigned { get; set; } = default!;

        [JsonProperty("unassigned", Required = Newtonsoft.Json.Required.Always)]
        public long Unassigned { get; set; } = default!;

        /// <summary>
        /// Counts only; a zero denominator means no eligible retained records and does not imply a zero rate.
        /// </summary>
        [JsonProperty("label_coverage", Required = Newtonsoft.Json.Required.Always)]
        public Coverage LabelCoverage { get; set; } = new();

        /// <summary>
        /// Current resolution of each resolved retained review; prior resolutions and reopened reviews do not count. Missing keys mean zero. These investigation conclusions are independent of account feedback.
        /// </summary>
        [JsonProperty("conclusions", Required = Newtonsoft.Json.Required.Always)]
        public ReviewsConclusions Conclusions { get; set; } = new();
    }
}