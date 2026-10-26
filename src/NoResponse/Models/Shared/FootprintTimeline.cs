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
    /// One identifier's dated footprint across every source: the first and last year any event was seen, the years between them, the latest year a source dated activity (omitted when none did), how many distinct sites and which kinds of evidence saw it.
    /// </summary>
    public class FootprintTimeline
    {
        [JsonProperty("first_seen_year", Required = Newtonsoft.Json.Required.Always)]
        public long FirstSeenYear { get; set; } = default!;

        [JsonProperty("last_seen_year", Required = Newtonsoft.Json.Required.Always)]
        public long LastSeenYear { get; set; } = default!;

        [JsonProperty("span_years", Required = Newtonsoft.Json.Required.Always)]
        public long SpanYears { get; set; } = default!;

        [JsonProperty("last_active_year", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? LastActiveYear { get; set; }

        [JsonProperty("sites", Required = Newtonsoft.Json.Required.Always)]
        public long Sites { get; set; } = default!;

        [JsonProperty("families", Required = Newtonsoft.Json.Required.Always)]
        public List<FootprintFamily> Families { get; set; } = default!;
    }
}