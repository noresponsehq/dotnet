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
    /// One answer from one source: where it came from, what found it and every field it holds. A person's lookup records also say how likely each is the person.
    /// </summary>
    public class Record
    {
        /// <summary>
        /// Made from the source and the fields' names and original values, so the same answer found twice is one record with the same id.
        /// </summary>
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        /// <summary>
        /// Where a record came from.
        /// </summary>
        [JsonProperty("source", Required = Newtonsoft.Json.Required.Always)]
        public RecordSource Source { get; set; } = new();

        /// <summary>
        /// Each kind of searched value that found the record.
        /// </summary>
        [JsonProperty("matched", Required = Newtonsoft.Json.Required.Always)]
        public List<RecordMatch> Matched { get; set; } = default!;

        /// <summary>
        /// Every field the source returned, in the order the source's connector lists them; a breach row's columns in name order. Nothing is withheld: a breach row's passwords, hashes and salts are fields like any other.
        /// </summary>
        [JsonProperty("fields", Required = Newtonsoft.Json.Required.Always)]
        public List<RecordField> Fields { get; set; } = default!;

        /// <summary>
        /// When the source last gave this answer.
        /// </summary>
        [JsonProperty("observed_at", Required = Newtonsoft.Json.Required.Always)]
        public string ObservedAt { get; set; } = default!;

        /// <summary>
        /// How likely the record is the person it was found for, from 0.01 to 0.99, to two places. A record found by the person's own email or phone starts as theirs; any other starts lower the more people its search found on its site. Each value it shares with what is known of the person raises it, a rare value more than a common one, and a different birth date or full name lowers it. What is known is what the person's row gives and the values of their own records: those found by their email or phone, and those a reviewer joined. Present on a person's lookup records, with agreed, clashed and joined.
        /// </summary>
        [JsonProperty("likelihood", Required = Newtonsoft.Json.Required.DisallowNull)]
        public double? Likelihood { get; set; }

        /// <summary>
        /// The fields of this record whose values agreed with what is known of the person, by their field names, in the record's order. The value that found the record is not among them.
        /// </summary>
        [JsonProperty("agreed", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<string>? Agreed { get; set; }

        /// <summary>
        /// The fields of this record that clashed with what is known of the person: a birth date or a full name that differs from the person's.
        /// </summary>
        [JsonProperty("clashed", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<string>? Clashed { get; set; }

        /// <summary>
        /// Whether the record joins the person: its likelihood reached 0.9 or a reviewer joined it, and no reviewer rejected it. A record that does not join stays a lead.
        /// </summary>
        [JsonProperty("joined", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? Joined { get; set; }

        /// <summary>
        /// A reviewer's overrule (POST .../people/{person_id}/records), which decides joined whatever the likelihood and sticks for as long as a lookup finds the same record. Absent when nobody overruled it.
        /// </summary>
        [JsonProperty("verdict", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Verdict? Verdict { get; set; }
    }
}