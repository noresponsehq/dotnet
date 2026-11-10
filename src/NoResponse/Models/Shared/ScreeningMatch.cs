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
    /// How a profile was matched to the attendee: the state its evidence earns, and the evidence.
    /// </summary>
    public class ScreeningMatch
    {
        /// <summary>
        /// How sure the screening is that a profile is the attendee's, from its evidence: confirmed is two or more hard identifiers (email, phone, a supplied handle, or a fact an existing source returned), probable is one identifier beyond the name, possible is the name alone.
        /// </summary>
        [JsonProperty("state", Required = Newtonsoft.Json.Required.Always)]
        public ProfileMatchState State { get; set; } = default!;

        [JsonProperty("evidence", Required = Newtonsoft.Json.Required.Always)]
        public List<ScreeningMatchEvidence> Evidence { get; set; } = default!;
    }
}