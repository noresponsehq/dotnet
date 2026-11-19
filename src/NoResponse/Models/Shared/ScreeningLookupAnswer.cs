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

    public class ScreeningLookupAnswer
    {
        /// <summary>
        /// The id of the connector asked: breach for the breach search.
        /// </summary>
        [JsonProperty("connector", Required = Newtonsoft.Json.Required.Always)]
        public string Connector { get; set; } = default!;

        [JsonProperty("identifier", Required = Newtonsoft.Json.Required.Always)]
        public Identifier Identifier { get; set; } = default!;

        /// <summary>
        /// Per-signal outcome. A source that timed out, was blocked or errored reports blocked, unavailable or inconclusive with a reason and is listed in incomplete_sources; not_found is a definite absence.
        /// </summary>
        [JsonProperty("outcome", Required = Newtonsoft.Json.Required.Always)]
        public SignalOutcome Outcome { get; set; } = default!;

        [JsonProperty("answered_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? AnsweredAt { get; set; }
    }
}