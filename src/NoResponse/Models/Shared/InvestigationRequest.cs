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
    /// Runs a new investigation against one retained workspace person.
    /// </summary>
    public class InvestigationRequest
    {
        [JsonProperty("depth", Required = Newtonsoft.Json.Required.Always)]
        public ScreeningDepth Depth { get; set; } = default!;

        [JsonProperty("person_id", Required = Newtonsoft.Json.Required.Always)]
        public string PersonId { get; set; } = default!;

        /// <summary>
        /// A change to enrichment. A field left out keeps the value in force; max_spend or deadline null removes the bound.
        /// </summary>
        [JsonProperty("enrichment", Required = Newtonsoft.Json.Required.DisallowNull)]
        public EnrichmentChange? Enrichment { get; set; }
    }
}