#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Changes only named settings, atomically. Every successful write advances policy_version and retains an immutable settings snapshot. expected_policy_version optionally guards against stale edits; restore always requires it.
    /// </summary>
    public class SettingsRequest
    {
        [JsonProperty("retention_days", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? RetentionDays { get; set; }

        /// <summary>
        /// Replaces the refused sources; [] refuses none. A change applies to new assessments within a minute.
        /// </summary>
        [JsonProperty("refused_sources", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<string>? RefusedSources { get; set; }

        /// <summary>
        /// Replaces the daily screening cap, in minor units of USD; null removes the cap. Needs settings.write only, like retention_days.
        /// </summary>
        [JsonProperty("screening_daily_cap", Required = Newtonsoft.Json.Required.Default, NullValueHandling = NullValueHandling.Include)]
        public OptionalNullable<long?> ScreeningDailyCap { get; set; }
        public bool ShouldSerializeScreeningDailyCap() => ScreeningDailyCap.IsSet;

        /// <summary>
        /// A change to enrichment. A field left out keeps the value in force; max_spend or deadline null removes the bound.
        /// </summary>
        [JsonProperty("enrichment", Required = Newtonsoft.Json.Required.DisallowNull)]
        public EnrichmentChange? Enrichment { get; set; }

        /// <summary>
        /// Use account_creation or its deprecated alias signup, not both.
        /// </summary>
        [JsonProperty("flows", Required = Newtonsoft.Json.Required.DisallowNull)]
        public SettingsRequestFlows? Flows { get; set; }

        /// <summary>
        /// Compare-and-set guard. A stale version returns 409 policy_version_conflict before any change; every settings write including retention advances the version.
        /// </summary>
        [JsonProperty("expected_policy_version", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? ExpectedPolicyVersion { get; set; }

        [JsonProperty("change_reason", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ChangeReason { get; set; }
    }
}