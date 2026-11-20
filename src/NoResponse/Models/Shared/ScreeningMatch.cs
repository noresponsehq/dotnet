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
    /// How a profile was matched to the person: the state its evidence earns, and the evidence.
    /// </summary>
    public class ScreeningMatch
    {
        /// <summary>
        /// How sure the screening is that a profile is the person's, from the likelihood its record scores. Confirmed reaches the join bar of 0.9. Probable reaches 0.85, which takes a shared value beyond the one that found the account. Possible is the rest.
        /// </summary>
        [JsonProperty("state", Required = Newtonsoft.Json.Required.Always)]
        public ProfileMatchState State { get; set; } = default!;

        [JsonProperty("evidence", Required = Newtonsoft.Json.Required.Always)]
        public List<ScreeningMatchEvidence> Evidence { get; set; } = default!;
    }
}