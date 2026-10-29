#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class PolicyEvaluationPublishRequest
    {
        [JsonProperty("expected_policy_version", Required = Newtonsoft.Json.Required.Always)]
        public long ExpectedPolicyVersion { get; set; } = default!;

        [JsonProperty("manifest_sha256", Required = Newtonsoft.Json.Required.Always)]
        public string ManifestSha256 { get; set; } = default!;

        [JsonProperty("input_sha256", Required = Newtonsoft.Json.Required.Always)]
        public string InputSha256 { get; set; } = default!;

        [JsonProperty("result_sha256", Required = Newtonsoft.Json.Required.Always)]
        public string ResultSha256 { get; set; } = default!;

        [JsonProperty("candidate", Required = Newtonsoft.Json.Required.Always)]
        public Thresholds Candidate { get; set; } = new();

        [JsonProperty("change_reason", Required = Newtonsoft.Json.Required.Always)]
        public string ChangeReason { get; set; } = default!;
    }
}