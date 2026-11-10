#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>
    /// A screening of an event finished. It counts only the attendees this screening set out to reach, not who they are: read the roster with GET /v1/workspaces/{workspace_id}/events/{environment}/{event_id}/attendees, filtered by band, to act on them. attendees_screened + partial_coverage = attendees_total, attendees_flagged is the sum of by_band, no_footprint counts screened attendees with no flag whose accounts were not found, and no_profile_to_check those the screening had nothing to look up for on any requested platform. A cancelled screening sends no event.
    /// </summary>
    public class ScreeningCompleted
    {
        /// <summary>
        /// The screening that finished.
        /// </summary>
        [JsonProperty("screening_id", Required = Newtonsoft.Json.Required.Always)]
        public string ScreeningId { get; set; } = default!;

        /// <summary>
        /// The event it screened.
        /// </summary>
        [JsonProperty("event_id", Required = Newtonsoft.Json.Required.Always)]
        public string EventId { get; set; } = default!;

        /// <summary>
        /// completed reached every attendee; partial left some not fully covered (partial_coverage); failed covered nobody.
        /// </summary>
        [JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public ScreeningCompletedStatus Status { get; set; } = default!;

        /// <summary>
        /// Why a partial or failed screening stopped before reaching everyone, as the screening resource says: expired, worker_attempts_exhausted, attempts_exhausted or screening_unavailable.
        /// </summary>
        [JsonProperty("error_code", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ErrorCode { get; set; }

        /// <summary>
        /// Every attendee the screening set out to reach: those no screening had reached and those on watch or escalated.
        /// </summary>
        [JsonProperty("attendees_total", Required = Newtonsoft.Json.Required.Always)]
        public long AttendeesTotal { get; set; } = default!;

        /// <summary>
        /// Attendees the screening covered, flagged or not.
        /// </summary>
        [JsonProperty("attendees_screened", Required = Newtonsoft.Json.Required.Always)]
        public long AttendeesScreened { get; set; } = default!;

        /// <summary>
        /// Screened attendees with a finding: the sum of by_band.
        /// </summary>
        [JsonProperty("attendees_flagged", Required = Newtonsoft.Json.Required.Always)]
        public long AttendeesFlagged { get; set; } = default!;

        /// <summary>
        /// Screened attendees by the band of their most severe finding.
        /// </summary>
        [JsonProperty("by_band", Required = Newtonsoft.Json.Required.Always)]
        public ScreeningCompletedByBand ByBand { get; set; } = new();

        /// <summary>
        /// Screened attendees with no finding whose accounts were not found (match_state none_found). A clean result for someone with no visible accounts is not the same as a clean result for someone whose accounts were read.
        /// </summary>
        [JsonProperty("no_footprint", Required = Newtonsoft.Json.Required.Always)]
        public long NoFootprint { get; set; } = default!;

        /// <summary>
        /// Screened attendees with no profile to check: the list gave nothing to look up on any requested platform, such as no handle, so nobody looked for a profile (no_profile_to_check on the attendee). Not in no_footprint, and not partial: nothing failed. Their lookups are on their reports.
        /// </summary>
        [JsonProperty("no_profile_to_check", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? NoProfileToCheck { get; set; }

        /// <summary>
        /// Attendees the screening did not fully cover. Read each one's coverage on its attendee.
        /// </summary>
        [JsonProperty("partial_coverage", Required = Newtonsoft.Json.Required.Always)]
        public long PartialCoverage { get; set; } = default!;

        /// <summary>
        /// When the screening finished; equal to the event's created_at.
        /// </summary>
        [JsonProperty("finished_at", Required = Newtonsoft.Json.Required.Always)]
        public string FinishedAt { get; set; } = default!;
    }
}