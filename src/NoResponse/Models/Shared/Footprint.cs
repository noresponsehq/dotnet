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
    /// How long and how widely the signup's email and username have existed, and how far independent sources agree on the person behind them, in years and counts only. families are the kinds of evidence that found either identifier. Omitted when nothing found them: a footprint lookup that failed makes no claim, and absence is no_digital_footprint's to state.
    /// </summary>
    public class Footprint
    {
        /// <summary>
        /// One identifier's dated footprint across every source: the first and last year any event was seen, the years between them, the latest year a source dated activity (omitted when none did), how many distinct sites and which kinds of evidence saw it.
        /// </summary>
        [JsonProperty("email", Required = Newtonsoft.Json.Required.DisallowNull)]
        public FootprintTimeline? Email { get; set; }

        /// <summary>
        /// One identifier's dated footprint across every source: the first and last year any event was seen, the years between them, the latest year a source dated activity (omitted when none did), how many distinct sites and which kinds of evidence saw it.
        /// </summary>
        [JsonProperty("username", Required = Newtonsoft.Json.Required.DisallowNull)]
        public FootprintTimeline? Username { get; set; }

        [JsonProperty("families", Required = Newtonsoft.Json.Required.Always)]
        public List<FootprintFamily> Families { get; set; } = default!;

        [JsonProperty("coherence", Required = Newtonsoft.Json.Required.DisallowNull)]
        public FootprintCoherence? Coherence { get; set; }
    }
}