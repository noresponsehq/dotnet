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
    /// A change to enrichment. A field left out keeps the value in force; max_spend or deadline null removes the bound.
    /// </summary>
    public class EnrichmentChange
    {
        /// <summary>
        /// off: enrichment starts no search. suggest: enrichment starts no search by itself. automatic: after a workspace person's own investigation, and each time a search on that person finishes, enrichment starts searches by itself.
        /// </summary>
        [JsonProperty("mode", Required = Newtonsoft.Json.Required.DisallowNull)]
        public EnrichmentMode? Mode { get; set; }

        /// <summary>
        /// Enrichment searches values only from records more likely than this to be the person's, 0 to 0.99.
        /// </summary>
        [JsonProperty("min_likelihood", Required = Newtonsoft.Json.Required.DisallowNull)]
        public double? MinLikelihood { get; set; }

        /// <summary>
        /// The kinds of value enrichment searches: distinctive usernames, and email addresses and phone numbers, which belong to one person each.
        /// </summary>
        [JsonProperty("kinds", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<EnrichmentChangeKind>? Kinds { get; set; }

        /// <summary>
        /// The most enrichment may spend on a person from one investigation, in minor units of USD (cents): each search it started after that investigation started counts its estimate or what it spent, whichever is more. At the bound enrichment starts nothing more for that investigation. null: only the environment's screening_daily_cap bounds it.
        /// </summary>
        [JsonProperty("max_spend", Required = Newtonsoft.Json.Required.Default, NullValueHandling = NullValueHandling.Include)]
        public OptionalNullable<long?> MaxSpend { get; set; }
        public bool ShouldSerializeMaxSpend() => MaxSpend.IsSet;

        /// <summary>
        /// How long enrichment may start searches after an investigation starts, 60 to 2592000 seconds. null: no deadline.
        /// </summary>
        [JsonProperty("deadline", Required = Newtonsoft.Json.Required.Default, NullValueHandling = NullValueHandling.Include)]
        public OptionalNullable<long?> Deadline { get; set; }
        public bool ShouldSerializeDeadline() => Deadline.IsSet;
    }
}