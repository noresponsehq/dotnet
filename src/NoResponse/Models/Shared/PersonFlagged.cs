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
    /// A screening recorded a finding on a person. It says which person and how severe, not what they posted, who they are or why: GET the person report for the findings. Sent once per screening that flags the person, so a re-screen of someone on watch announces them again.
    /// </summary>
    public class PersonFlagged
    {
        /// <summary>
        /// The person's group.
        /// </summary>
        [JsonProperty("group_id", Required = Newtonsoft.Json.Required.Always)]
        public string GroupId { get; set; } = default!;

        /// <summary>
        /// The flagged person.
        /// </summary>
        [JsonProperty("person_id", Required = Newtonsoft.Json.Required.Always)]
        public string PersonId { get; set; } = default!;

        /// <summary>
        /// The screening that flagged them.
        /// </summary>
        [JsonProperty("screening_id", Required = Newtonsoft.Json.Required.Always)]
        public string ScreeningId { get; set; } = default!;

        [JsonProperty("highest_band", Required = Newtonsoft.Json.Required.Always)]
        public HighestBand HighestBand { get; set; } = default!;

        /// <summary>
        /// How sure the screening is that the accounts it read are the person's. A finding on a possible match is at most moderate, on a probable match at most high.
        /// </summary>
        [JsonProperty("match_state", Required = Newtonsoft.Json.Required.Always)]
        public PersonFlaggedMatchState MatchState { get; set; } = default!;

        /// <summary>
        /// When the screening recorded the finding; equal to the event's created_at.
        /// </summary>
        [JsonProperty("flagged_at", Required = Newtonsoft.Json.Required.Always)]
        public string FlaggedAt { get; set; } = default!;
    }
}