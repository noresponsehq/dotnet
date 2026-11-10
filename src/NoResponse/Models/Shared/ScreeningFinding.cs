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
    /// Something the analyser flagged. band is capped by the strongest match among the profiles of its evidence: possible at most moderate, probable at most high. capped_from is the band the analyser gave when the cap lowered it.
    /// </summary>
    public class ScreeningFinding
    {
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        /// <summary>
        /// What a finding is about: a direct threat to the principal or the event, intent to harm without a named target, fixation on the principal, a grievance against the host, research into the venue, route or security, weapons tied to any of these, disruption or protest planning toward this event, violent extremist content or identification, prior harassment or stalking of staff or principals, or posting the principal's addresses or movements.
        /// </summary>
        [JsonProperty("category", Required = Newtonsoft.Json.Required.Always)]
        public FindingCategory Category { get; set; } = default!;

        /// <summary>
        /// How severe a finding is, most severe first.
        /// </summary>
        [JsonProperty("band", Required = Newtonsoft.Json.Required.Always)]
        public FindingBand Band { get; set; } = default!;

        /// <summary>
        /// How severe a finding is, most severe first.
        /// </summary>
        [JsonProperty("capped_from", Required = Newtonsoft.Json.Required.DisallowNull)]
        public FindingBand? CappedFrom { get; set; }

        [JsonProperty("confidence", Required = Newtonsoft.Json.Required.Always)]
        public double Confidence { get; set; } = default!;

        [JsonProperty("rationale", Required = Newtonsoft.Json.Required.Always)]
        public string Rationale { get; set; } = default!;

        /// <summary>
        /// The exact words of the first evidence item that the finding rests on, as the analyser quoted them.
        /// </summary>
        [JsonProperty("quote", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Quote { get; set; }

        /// <summary>
        /// The posts and media that support the finding.
        /// </summary>
        [JsonProperty("evidence", Required = Newtonsoft.Json.Required.Always)]
        public List<string> Evidence { get; set; } = default!;

        /// <summary>
        /// The resolver, collector or analyser that produced a record, by its catalogue name.
        /// </summary>
        [JsonProperty("analyser", Required = Newtonsoft.Json.Required.Always)]
        public string Analyser { get; set; } = default!;

        [JsonProperty("model", Required = Newtonsoft.Json.Required.Always)]
        public string Model { get; set; } = default!;

        /// <summary>
        /// What the analysis cost, in US cents.
        /// </summary>
        [JsonProperty("spend_minor", Required = Newtonsoft.Json.Required.Always)]
        public long SpendMinor { get; set; } = default!;

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;
    }
}