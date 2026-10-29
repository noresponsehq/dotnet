#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    public class PolicyEvaluationRequest
    {
        [JsonProperty("expected_policy_version", Required = Newtonsoft.Json.Required.Always)]
        public long ExpectedPolicyVersion { get; set; } = default!;

        [JsonProperty("from", Required = Newtonsoft.Json.Required.Always)]
        public string From { get; set; } = default!;

        [JsonProperty("to", Required = Newtonsoft.Json.Required.Always)]
        public string To { get; set; } = default!;

        [JsonProperty("candidates", Required = Newtonsoft.Json.Required.Always)]
        public List<Thresholds> Candidates { get; set; } = default!;

        [JsonProperty("costs", Required = Newtonsoft.Json.Required.Always)]
        public PolicyEvaluationRequestCosts Costs { get; set; } = new();

        [JsonProperty("limits", Required = Newtonsoft.Json.Required.Always)]
        public PolicyEvaluationRequestLimits Limits { get; set; } = new();

        [JsonProperty("label_collection", Required = Newtonsoft.Json.Required.Always)]
        public string LabelCollection { get; set; } = default!;

        [JsonProperty("split_seed", Required = Newtonsoft.Json.Required.Always)]
        public string SplitSeed { get; set; } = default!;
    }
}