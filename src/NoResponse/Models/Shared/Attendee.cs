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
        /// The attendee's role at the event: guest (a ticket holder), crew, vendor or press. Leave it out when you do not know it: an attendee without one has no role, not guest. A later row naming the same attendee replaces it, and a change of role alone keeps the attendee's screening results.
        /// </summary>
        [JsonProperty("role", Required = Newtonsoft.Json.Required.DisallowNull)]
        public AttendeeRole? Role { get; set; }

        /// <summary>
        /// How sure the latest screening is that it found the attendee's accounts. unscreened until a screening reaches the attendee, and after one that had no profile to check (no_profile_to_check).
        /// </summary>
        [JsonProperty("match_state", Required = Newtonsoft.Json.Required.Always)]
        public MatchState MatchState { get; set; } = default!;

        /// <summary>
        /// true when the latest screening reached the attendee but the list gave nothing to look up on any requested platform, such as no handle for a platform that reads supplied handles only: nobody looked for a profile. match_state stays unscreened, every platform is in coverage.not_searched, and the attendee is neither none_found nor failed and does not make the screening partial. Its lookups are on its report. Left out otherwise.
        /// </summary>
        [JsonProperty("no_profile_to_check", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? NoProfileToCheck { get; set; }

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
        /// The denominator of a screening: accounts found, how many could be read, items read and the period they span, so a clean result is distinct from a person nobody could see.
        /// </summary>
        [JsonProperty("coverage", Required = Newtonsoft.Json.Required.Always)]
        public ScreeningCoverage Coverage { get; set; } = new();

        [JsonProperty("last_screening_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? LastScreeningId { get; set; }

        [JsonProperty("screened_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ScreenedAt { get; set; }

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;

        [JsonProperty("updated_at", Required = Newtonsoft.Json.Required.Always)]
        public string UpdatedAt { get; set; } = default!;

        /// <summary>
        /// What screening cost, in US cents, in total and per implementation.
        /// </summary>
        [JsonProperty("spend", Required = Newtonsoft.Json.Required.Always)]
        public ScreeningSpend Spend { get; set; } = new();
    }
}