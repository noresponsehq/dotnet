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
    /// PATCH /v1/workspaces/{workspace_id}/settings/{environment} body. Changes only what it names, in one transaction; a policy change is one new policy_version. Needs the admin or owner role, or an API key with the management scope for its own environment.
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
    }
}