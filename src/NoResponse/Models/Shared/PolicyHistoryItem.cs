#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class PolicyHistoryItem
    {
        /// <summary>
        /// One environment's settings: how long its assessments are kept, and for each flow the risk thresholds that decide it and the depth a request that names none is served at, and the sources it refuses. Live and test each have their own. A flow without thresholds of its own uses account_creation's (inherits_account_creation). Every change is recorded in the audit log as settings.updated, with who made it.
        /// </summary>
        [JsonProperty("settings", Required = Newtonsoft.Json.Required.Always)]
        public Models.Shared.Settings Settings { get; set; } = new();

        /// <summary>
        /// Capture time, not a backfilled activation time. Absent only for the current configuration before its first capture.
        /// </summary>
        [JsonProperty("recorded_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? RecordedAt { get; set; }

        [JsonProperty("change_reason", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ChangeReason { get; set; }

        [JsonProperty("restored_from", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? RestoredFrom { get; set; }

        /// <summary>
        /// Immutable publication provenance remains in policy history after evaluation evidence expires. SHA-256 values identify the frozen manifest, evaluator semantic input, cohort, and canonical result separately.
        /// </summary>
        [JsonProperty("evaluation", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Publication? Evaluation { get; set; }
    }
}