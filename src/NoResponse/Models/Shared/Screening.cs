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
    /// A screening and its progress.
    /// </summary>
    public class Screening
    {
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        [JsonProperty("group_id", Required = Newtonsoft.Json.Required.Always)]
        public string GroupId { get; set; } = default!;

        [JsonProperty("environment", Required = Newtonsoft.Json.Required.Always)]
        public AuditSettingsEnvironment Environment { get; set; } = default!;

        /// <summary>
        /// partial finished with some people or platforms not covered; people_failed and people_partial count them.
        /// </summary>
        [JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public ScreeningStatus Status { get; set; } = default!;

        [JsonProperty("depth", Required = Newtonsoft.Json.Required.Always)]
        public ScreeningDepth Depth { get; set; } = default!;

        [JsonProperty("platforms", Required = Newtonsoft.Json.Required.Always)]
        public List<HandlePlatform> Platforms { get; set; } = default!;

        [JsonProperty("requested_by", Required = Newtonsoft.Json.Required.Always)]
        public string RequestedBy { get; set; } = default!;

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;

        [JsonProperty("started_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? StartedAt { get; set; }

        [JsonProperty("finished_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? FinishedAt { get; set; }

        /// <summary>
        /// A screening still running then fails with error_code expired.
        /// </summary>
        [JsonProperty("expires_at", Required = Newtonsoft.Json.Required.Always)]
        public string ExpiresAt { get; set; } = default!;

        /// <summary>
        /// People selected when the screening started.
        /// </summary>
        [JsonProperty("people_total", Required = Newtonsoft.Json.Required.Always)]
        public long PeopleTotal { get; set; } = default!;

        /// <summary>
        /// People screened so far, failed ones included.
        /// </summary>
        [JsonProperty("people_done", Required = Newtonsoft.Json.Required.Always)]
        public long PeopleDone { get; set; } = default!;

        [JsonProperty("people_flagged", Required = Newtonsoft.Json.Required.Always)]
        public long PeopleFlagged { get; set; } = default!;

        [JsonProperty("people_failed", Required = Newtonsoft.Json.Required.Always)]
        public long PeopleFailed { get; set; } = default!;

        /// <summary>
        /// People screened with some requested platform not covered; their coverage names which.
        /// </summary>
        [JsonProperty("people_partial", Required = Newtonsoft.Json.Required.Always)]
        public long PeoplePartial { get; set; } = default!;

        /// <summary>
        /// What the screening was estimated to cost when it started, in minor units of USD (cents): its people at its depth and platforms. Until the screening finishes, what it has not yet spent counts against the environment's screening_daily_cap.
        /// </summary>
        [JsonProperty("estimated_spend", Required = Newtonsoft.Json.Required.Always)]
        public long EstimatedSpend { get; set; } = default!;

        /// <summary>
        /// What the people it has screened recorded, in minor units of USD. Each counts toward the UTC day it was recorded on; a failed person records nothing.
        /// </summary>
        [JsonProperty("spent", Required = Newtonsoft.Json.Required.Always)]
        public long Spent { get; set; } = default!;

        /// <summary>
        /// What screening cost, in US cents, in total and per implementation.
        /// </summary>
        [JsonProperty("spend", Required = Newtonsoft.Json.Required.Always)]
        public ScreeningSpend Spend { get; set; } = new();

        /// <summary>
        /// Why a failed or cancelled screening stopped.
        /// </summary>
        [JsonProperty("error_code", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ErrorCode { get; set; }
    }
}