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
    /// One environment's settings: how long its assessments are kept, and for each flow the risk thresholds that decide it and the depth a request that names none is served at, and the sources it refuses. Live and test each have their own. A flow without thresholds of its own uses account_creation's (inherits_account_creation). Every change is recorded in the audit log as settings.updated, with who made it.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// The workspace id, which is also its live environment's workspace id; the test environment's is the same id with :test appended. A workspace created through the API is tnt_ and a 26-character ULID.
        /// </summary>
        [JsonProperty("workspace_id", Required = Newtonsoft.Json.Required.Always)]
        public string WorkspaceId { get; set; } = default!;

        /// <summary>
        /// The environment the key belongs to. A live key assesses with live sources; a test key answers from the deterministic simulator.
        /// </summary>
        [JsonProperty("environment", Required = Newtonsoft.Json.Required.Always)]
        public KeyEnvironment Environment { get; set; } = default!;

        /// <summary>
        /// Days an assessment is kept after it is created, 1 to 365 (default 365). Shortening hides older assessments at once.
        /// </summary>
        [JsonProperty("retention_days", Required = Newtonsoft.Json.Required.Always)]
        public long RetentionDays { get; set; } = default!;

        /// <summary>
        /// How many times this environment's policy has changed; 0 while the defaults apply. A served risk.policy_version ends in +t&lt;policy_version&gt; once it is above 0.
        /// </summary>
        [JsonProperty("policy_version", Required = Newtonsoft.Json.Required.Always)]
        public long PolicyVersion { get; set; } = default!;

        /// <summary>
        /// Who last changed the policy: the member's external user id for a change made through the console, the prefix (nrk_&lt;environment&gt;_&lt;key id&gt;) of the management key that made it, or NoResponse. Absent while the defaults apply.
        /// </summary>
        [JsonProperty("updated_by", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? UpdatedBy { get; set; }

        /// <summary>
        /// When the policy last changed. Absent while the defaults apply.
        /// </summary>
        [JsonProperty("updated_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? UpdatedAt { get; set; }

        [JsonProperty("flows", Required = Newtonsoft.Json.Required.Always)]
        public SettingsFlows Flows { get; set; } = new();

        /// <summary>
        /// The sources this environment refuses, sorted; empty, the default, refuses none. An assessment leaves a refused source out: its coverage is missing (an identifier with no other source lists coverage-unavailable as unavailable) rather than not_found, and a stored result of a refused source is not reused.
        /// </summary>
        [JsonProperty("refused_sources", Required = Newtonsoft.Json.Required.Always)]
        public List<SourceId> RefusedSources { get; set; } = default!;
    }
}