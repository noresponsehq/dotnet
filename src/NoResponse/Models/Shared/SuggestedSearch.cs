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
    /// A search enrichment would start: a distinctive or unique value of a likely record, not yet searched for the person. A person starts it with POST .../searches, naming the record and the field.
    /// </summary>
    public class SuggestedSearch
    {
        /// <summary>
        /// The lookup record whose value enrichment would search. It is among the report's lookup records.
        /// </summary>
        [JsonProperty("record_id", Required = Newtonsoft.Json.Required.Always)]
        public string RecordId { get; set; } = default!;

        /// <summary>
        /// The field of that record that holds the value. Send record_id and field to POST .../searches to start the search.
        /// </summary>
        [JsonProperty("field", Required = Newtonsoft.Json.Required.Always)]
        public string Field { get; set; } = default!;

        /// <summary>
        /// The kind of the value: one of the kinds the environment's enrichment setting names.
        /// </summary>
        [JsonProperty("kind", Required = Newtonsoft.Json.Required.Always)]
        public SuggestedSearchKind Kind { get; set; } = default!;

        /// <summary>
        /// The likelihood of the record that holds the value, from 0.01 to 0.99. Enrichment suggests a value only from a record more likely than the setting's min_likelihood, the likeliest first.
        /// </summary>
        [JsonProperty("likelihood", Required = Newtonsoft.Json.Required.Always)]
        public double Likelihood { get; set; } = default!;
    }
}