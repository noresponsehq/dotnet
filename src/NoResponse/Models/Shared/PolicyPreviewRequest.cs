#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>
    /// Side-effect-free threshold preview for at most 31 days. Retained first answers are evaluated at their exact frozen scores using the production threshold and hold rules. Changes to admission, source selection, depth or retention are not simulated.
    /// </summary>
    public class PolicyPreviewRequest
    {
        [JsonProperty("expected_policy_version", Required = Newtonsoft.Json.Required.Always)]
        public long ExpectedPolicyVersion { get; set; } = default!;

        [JsonProperty("from", Required = Newtonsoft.Json.Required.Always)]
        public string From { get; set; } = default!;

        [JsonProperty("to", Required = Newtonsoft.Json.Required.Always)]
        public string To { get; set; } = default!;

        [JsonProperty("limit", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Limit { get; set; } = 1000;

        [JsonProperty("flows", Required = Newtonsoft.Json.Required.Always)]
        public PolicyPreviewRequestFlows Flows { get; set; } = new();
    }
}