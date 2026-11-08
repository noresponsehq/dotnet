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
    /// An attendee and where their screening and review stand.
    /// </summary>
    public class Attendee
    {
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        [JsonProperty("event_id", Required = Newtonsoft.Json.Required.Always)]
        public string EventId { get; set; } = default!;

        /// <summary>
        /// Your own id for the attendee, unique within the event. A later row with the same external id updates this attendee.
        /// </summary>
        [JsonProperty("external_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ExternalId { get; set; }

        /// <summary>
        /// The attendee's full name.
        /// </summary>
        [JsonProperty("name", Required = Newtonsoft.Json.Required.Always)]
        public string Name { get; set; } = default!;

        /// <summary>
        /// The attendee's email.
        /// </summary>
        [JsonProperty("email", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Email { get; set; }

        /// <summary>
        /// The attendee's phone in E.164 form.
        /// </summary>
        [JsonProperty("phone", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Phone { get; set; }

        /// <summary>
        /// Where the attendee works.
        /// </summary>
        [JsonProperty("company", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Company { get; set; }

        /// <summary>
        /// Where the attendee lives.
        /// </summary>
        [JsonProperty("city", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? City { get; set; }

        /// <summary>
        /// Accounts the attendee is known to hold, at most 20.
        /// </summary>
        [JsonProperty("handles", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<Handle>? Handles { get; set; }

        /// <summary>
        /// How sure the latest screening is that it found the attendee's accounts. unscreened until a screening reaches the attendee.
        /// </summary>
        [JsonProperty("match_state", Required = Newtonsoft.Json.Required.Always)]
        public MatchState MatchState { get; set; } = default!;

        /// <summary>
        /// The most severe finding's band, or null when nothing was flagged.
        /// </summary>
        [JsonProperty("highest_band", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public HighestBand? HighestBand { get; set; }

        /// <summary>
        /// The latest reviewer decision, or null before any.
        /// </summary>
        [JsonProperty("decision", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public AttendeeDecision? Decision { get; set; }

        [JsonProperty("decision_note", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? DecisionNote { get; set; }

        [JsonProperty("decided_by", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? DecidedBy { get; set; }

        [JsonProperty("decided_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? DecidedAt { get; set; }

        /// <summary>
        /// What the latest screening read, per platform.
        /// </summary>
        [JsonProperty("coverage", Required = Newtonsoft.Json.Required.Always)]
        public AttendeeCoverage Coverage { get; set; } = new();

        [JsonProperty("last_screening_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? LastScreeningId { get; set; }

        [JsonProperty("screened_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ScreenedAt { get; set; }

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;

        [JsonProperty("updated_at", Required = Newtonsoft.Json.Required.Always)]
        public string UpdatedAt { get; set; } = default!;
    }
}