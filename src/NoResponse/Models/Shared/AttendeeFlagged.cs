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
    /// A screening recorded a finding on an attendee. It says which attendee and how severe, not what they posted, who they are or why: GET the attendee report for the findings. Sent once per screening that flags the attendee, so a re-screen of someone on watch announces them again.
    /// </summary>
    public class AttendeeFlagged
    {
        /// <summary>
        /// The attendee's event.
        /// </summary>
        [JsonProperty("event_id", Required = Newtonsoft.Json.Required.Always)]
        public string EventId { get; set; } = default!;

        /// <summary>
        /// The flagged attendee.
        /// </summary>
        [JsonProperty("attendee_id", Required = Newtonsoft.Json.Required.Always)]
        public string AttendeeId { get; set; } = default!;

        /// <summary>
        /// The screening that flagged them.
        /// </summary>
        [JsonProperty("screening_id", Required = Newtonsoft.Json.Required.Always)]
        public string ScreeningId { get; set; } = default!;

        [JsonProperty("highest_band", Required = Newtonsoft.Json.Required.Always)]
        public HighestBand HighestBand { get; set; } = default!;

        /// <summary>
        /// How sure the screening is that the accounts it read are the attendee's. A finding on a possible match is at most moderate, on a probable match at most high.
        /// </summary>
        [JsonProperty("match_state", Required = Newtonsoft.Json.Required.Always)]
        public AttendeeFlaggedMatchState MatchState { get; set; } = default!;

        /// <summary>
        /// When the screening recorded the finding; equal to the event's created_at.
        /// </summary>
        [JsonProperty("flagged_at", Required = Newtonsoft.Json.Required.Always)]
        public string FlaggedAt { get; set; } = default!;
    }
}