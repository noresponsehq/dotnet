#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

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
        public List<SourceId>? RefusedSources { get; set; }

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