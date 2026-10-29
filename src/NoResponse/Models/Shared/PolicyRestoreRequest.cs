#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// Publish a captured configuration as a new version. Version checks and publication are atomic. Longer retention does not revive expired records, and restored policy does not undo customer enforcement.
    /// </summary>
    public class PolicyRestoreRequest
    {
        [JsonProperty("version", Required = Newtonsoft.Json.Required.Always)]
        public long Version { get; set; } = default!;

        [JsonProperty("expected_policy_version", Required = Newtonsoft.Json.Required.Always)]
        public long ExpectedPolicyVersion { get; set; } = default!;

        [JsonProperty("change_reason", Required = Newtonsoft.Json.Required.Always)]
        public string ChangeReason { get; set; } = default!;
    }
}