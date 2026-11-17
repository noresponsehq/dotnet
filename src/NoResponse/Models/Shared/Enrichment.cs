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
    /// How enrichment searches the values a standalone investigation of a workspace person finds (POST /v1/investigations); group screening only suggests. Defaults: mode automatic, min_likelihood 0.5, kinds username, max_spend null and deadline null. An investigation request may override each field for that investigation. A change applies to the next search enrichment starts, investigations already run included. Enrichment also keeps to limits of its own: each value once per person (once more if that search failed), at most 5 searches from one search's records, 20 per person and 6 searches deep.
    /// </summary>
    public class Enrichment
    {
        /// <summary>
        /// off: enrichment starts no search. suggest: enrichment starts no search by itself. automatic: after a workspace person's own investigation, and each time a search on that person finishes, enrichment starts searches by itself.
        /// </summary>
        [JsonProperty("mode", Required = Newtonsoft.Json.Required.Always)]
        public EnrichmentMode Mode { get; set; } = default!;

        /// <summary>
        /// Enrichment searches values only from records more likely than this to be the person's, 0 to 0.99.
        /// </summary>
        [JsonProperty("min_likelihood", Required = Newtonsoft.Json.Required.Always)]
        public double MinLikelihood { get; set; } = default!;

        /// <summary>
        /// The kinds of value enrichment searches: distinctive usernames, and email addresses and phone numbers, which belong to one person each.
        /// </summary>
        [JsonProperty("kinds", Required = Newtonsoft.Json.Required.Always)]
        public List<EnrichmentKind> Kinds { get; set; } = default!;

        /// <summary>
        /// The most enrichment may spend on a person from one investigation, in minor units of USD (cents): each search it started after that investigation started counts its estimate or what it spent, whichever is more. At the bound enrichment starts nothing more for that investigation. null: only the environment's screening_daily_cap bounds it.
        /// </summary>
        [JsonProperty("max_spend", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public long? MaxSpend { get; set; }

        /// <summary>
        /// How long enrichment may start searches after an investigation starts, 60 to 2592000 seconds. null: no deadline.
        /// </summary>
        [JsonProperty("deadline", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public long? Deadline { get; set; }
    }
}