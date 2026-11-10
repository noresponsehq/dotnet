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
    /// One identifier that ties a profile to the attendee.
    /// </summary>
    public class ScreeningMatchEvidence
    {
        /// <summary>
        /// What matched. email, phone, supplied_handle and source_fact are hard identifiers; company, city and school are soft. supplied_handle is a handle on the attendee row; source_fact is an account a profile found by the attendee's lookups lists.
        /// </summary>
        [JsonProperty("kind", Required = Newtonsoft.Json.Required.Always)]
        public ScreeningMatchEvidenceKind Kind { get; set; } = default!;

        /// <summary>
        /// Where the match was seen, such as the profile field that carried it.
        /// </summary>
        [JsonProperty("detail", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Detail { get; set; }

        /// <summary>
        /// The resolver, collector or analyser that produced a record, by its catalogue name.
        /// </summary>
        [JsonProperty("resolver", Required = Newtonsoft.Json.Required.Always)]
        public string Resolver { get; set; } = default!;
    }
}