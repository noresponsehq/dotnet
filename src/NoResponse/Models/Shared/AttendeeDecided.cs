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
    /// A reviewer recorded a decision on an attendee. The decision's note stays in the API. Sent once per recorded review; an attendee's latest review is its decision, so order by decided_at.
    /// </summary>
    public class AttendeeDecided
    {
        /// <summary>
        /// The attendee's event.
        /// </summary>
        [JsonProperty("event_id", Required = Newtonsoft.Json.Required.Always)]
        public string EventId { get; set; } = default!;

        /// <summary>
        /// The attendee decided on.
        /// </summary>
        [JsonProperty("attendee_id", Required = Newtonsoft.Json.Required.Always)]
        public string AttendeeId { get; set; } = default!;

        /// <summary>
        /// The recorded review.
        /// </summary>
        [JsonProperty("review_id", Required = Newtonsoft.Json.Required.Always)]
        public string ReviewId { get; set; } = default!;

        [JsonProperty("decision", Required = Newtonsoft.Json.Required.Always)]
        public AttendeeDecision Decision { get; set; } = default!;

        /// <summary>
        /// Who decided: the member's external user id, or the API key's label, as the attendee's decided_by.
        /// </summary>
        [JsonProperty("decided_by", Required = Newtonsoft.Json.Required.Always)]
        public string DecidedBy { get; set; } = default!;

        /// <summary>
        /// When the decision was recorded; equal to the event's created_at.
        /// </summary>
        [JsonProperty("decided_at", Required = Newtonsoft.Json.Required.Always)]
        public string DecidedAt { get; set; } = default!;
    }
}