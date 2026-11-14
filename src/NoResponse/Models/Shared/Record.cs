#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// One answer from one source: where it came from, what found it and every field it holds.
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
    }
}