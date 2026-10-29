#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Exact threshold transitions among eligible retained first answers, newest first within the limit. Partial older revisions without a frozen hold snapshot are reported as unsupported. Preview leaves account state, outcomes and source evidence unchanged.
    /// </summary>
    public class PolicyPreview
    {
        [JsonProperty("scope", Required = Newtonsoft.Json.Required.Always)]
        public string Scope { get; } = "retained_first_answer_thresholds";

        [JsonProperty("policy_version", Required = Newtonsoft.Json.Required.Always)]
        public long PolicyVersion { get; set; } = default!;

        [JsonProperty("from", Required = Newtonsoft.Json.Required.Always)]
        public string From { get; set; } = default!;

        [JsonProperty("to", Required = Newtonsoft.Json.Required.Always)]
        public string To { get; set; } = default!;

        [JsonProperty("retention_from", Required = Newtonsoft.Json.Required.Always)]
        public string RetentionFrom { get; set; } = default!;

        [JsonProperty("coverage", Required = Newtonsoft.Json.Required.Always)]
        public PolicyPreviewCoverage Coverage { get; set; } = new();

        [JsonProperty("changed", Required = Newtonsoft.Json.Required.Always)]
        public long Changed { get; set; } = default!;

        [JsonProperty("unchanged", Required = Newtonsoft.Json.Required.Always)]
        public long Unchanged { get; set; } = default!;

        [JsonProperty("transitions", Required = Newtonsoft.Json.Required.Always)]
        public List<PolicyPreviewTransition> Transitions { get; set; } = default!;
    }
}