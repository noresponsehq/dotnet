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
    /// A person of another group in the same environment who shares an email or phone with this one, as that group recorded them.
    /// </summary>
    public class OtherGroup
    {
        [JsonProperty("group_id", Required = Newtonsoft.Json.Required.Always)]
        public string GroupId { get; set; } = default!;

        [JsonProperty("group_name", Required = Newtonsoft.Json.Required.Always)]
        public string GroupName { get; set; } = default!;

        /// <summary>
        /// The other group's optional event start; null for an ongoing group.
        /// </summary>
        [JsonProperty("group_starts_at", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public string? GroupStartsAt { get; set; }

        /// <summary>
        /// The person on that group's list.
        /// </summary>
        [JsonProperty("person_id", Required = Newtonsoft.Json.Required.Always)]
        public string PersonId { get; set; } = default!;

        /// <summary>
        /// The same workspace person explicitly belongs to this group. Membership requires an explicit person or customer reference.
        /// </summary>
        [JsonProperty("matched_on", Required = Newtonsoft.Json.Required.Always)]
        public List<OtherGroupMatchedOn> MatchedOn { get; set; } = default!;

        /// <summary>
        /// How sure the latest screening is that it found the person's accounts. unscreened until a screening reaches the person, and after one that had no profile to check (no_profile_to_check).
        /// </summary>
        [JsonProperty("match_state", Required = Newtonsoft.Json.Required.Always)]
        public MatchState MatchState { get; set; } = default!;

        /// <summary>
        /// true when that person's latest screening had no profile to check (see the person's no_profile_to_check). Left out otherwise.
        /// </summary>
        [JsonProperty("no_profile_to_check", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? NoProfileToCheck { get; set; }

        /// <summary>
        /// That person's most severe band in that group, or null.
        /// </summary>
        [JsonProperty("highest_band", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public HighestBand? HighestBand { get; set; }

        /// <summary>
        /// The latest reviewer decision in that group, or null before any.
        /// </summary>
        [JsonProperty("decision", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public PersonDecision? Decision { get; set; }

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