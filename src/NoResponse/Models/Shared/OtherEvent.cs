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
    /// An attendee of another event in the same environment who shares an email or phone with this one, as that event recorded them.
    /// </summary>
    public class OtherEvent
    {
        [JsonProperty("event_id", Required = Newtonsoft.Json.Required.Always)]
        public string EventId { get; set; } = default!;

        [JsonProperty("event_name", Required = Newtonsoft.Json.Required.Always)]
        public string EventName { get; set; } = default!;

        [JsonProperty("event_starts_at", Required = Newtonsoft.Json.Required.Always)]
        public string EventStartsAt { get; set; } = default!;

        /// <summary>
        /// The attendee on that event's list.
        /// </summary>
        [JsonProperty("attendee_id", Required = Newtonsoft.Json.Required.Always)]
        public string AttendeeId { get; set; } = default!;

        /// <summary>
        /// Which identifiers match, in this order. external_id appears only beside email or phone: the same external id alone is not a match, since ticket numbers may be reused across events.
        /// </summary>
        [JsonProperty("matched_on", Required = Newtonsoft.Json.Required.Always)]
        public List<MatchedOn> MatchedOn { get; set; } = default!;

        /// <summary>
        /// How sure the latest screening is that it found the attendee's accounts. unscreened until a screening reaches the attendee.
        /// </summary>
        [JsonProperty("match_state", Required = Newtonsoft.Json.Required.Always)]
        public MatchState MatchState { get; set; } = default!;

        /// <summary>
        /// That attendee's most severe band at that event, or null.
        /// </summary>
        [JsonProperty("highest_band", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public HighestBand? HighestBand { get; set; }

        /// <summary>
        /// The latest reviewer decision at that event, or null before any.
        /// </summary>
        [JsonProperty("decision", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public AttendeeDecision? Decision { get; set; }

        /// <summary>
        /// Who made that decision.
        /// </summary>
        [JsonProperty("decided_by", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? DecidedBy { get; set; }

        /// <summary>
        /// When that decision was made.
        /// </summary>
        [JsonProperty("decided_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? DecidedAt { get; set; }
    }
}