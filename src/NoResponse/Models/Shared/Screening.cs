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

        [JsonProperty("event_id", Required = Newtonsoft.Json.Required.Always)]
        public string EventId { get; set; } = default!;

        [JsonProperty("environment", Required = Newtonsoft.Json.Required.Always)]
        public AuditSettingsEnvironment Environment { get; set; } = default!;

        /// <summary>
        /// partial finished with some attendees not covered; attendees_failed counts them.
        /// </summary>
        [JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public ScreeningStatus Status { get; set; } = default!;

        [JsonProperty("depth", Required = Newtonsoft.Json.Required.Always)]
        public ScreeningDepth Depth { get; set; } = default!;

        [JsonProperty("platforms", Required = Newtonsoft.Json.Required.Always)]
        public List<ScreeningPlatform> Platforms { get; set; } = default!;

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
        /// Attendees selected when the screening started.
        /// </summary>
        [JsonProperty("attendees_total", Required = Newtonsoft.Json.Required.Always)]
        public long AttendeesTotal { get; set; } = default!;

        /// <summary>
        /// Attendees screened so far, failed ones included.
        /// </summary>
        [JsonProperty("attendees_done", Required = Newtonsoft.Json.Required.Always)]
        public long AttendeesDone { get; set; } = default!;

        [JsonProperty("attendees_flagged", Required = Newtonsoft.Json.Required.Always)]
        public long AttendeesFlagged { get; set; } = default!;

        [JsonProperty("attendees_failed", Required = Newtonsoft.Json.Required.Always)]
        public long AttendeesFailed { get; set; } = default!;

        /// <summary>
        /// Why a failed or cancelled screening stopped.
        /// </summary>
        [JsonProperty("error_code", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ErrorCode { get; set; }
    }
}