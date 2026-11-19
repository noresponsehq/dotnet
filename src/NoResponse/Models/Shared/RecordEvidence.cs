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
    /// One item of evidence behind a record's likelihood.
    /// </summary>
    public class RecordEvidence
    {
        /// <summary>
        /// found_by: what found the record, which sets where its likelihood starts. agrees: a value the record shares with what the investigator gave or with a joined record. clashes: a birth date, full name, gender or country that differs from theirs.
        /// </summary>
        [JsonProperty("signal", Required = Newtonsoft.Json.Required.Always)]
        public RecordEvidenceSignal Signal { get; set; } = default!;

        /// <summary>
        /// For found_by, what the search looked up (email, phone, username, handle, name). Otherwise the kind of value compared, such as email, username, person_name, born, city or crypto_address.
        /// </summary>
        [JsonProperty("kind", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Kind { get; set; }

        /// <summary>
        /// The record's own field that agreed or clashed, or for found_by the field the search matched.
        /// </summary>
        [JsonProperty("field", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Field { get; set; }

        /// <summary>
        /// given for what the investigator gave, or the id of the person's record it agreed or clashed with, or for found_by the record whose value the search looked up.
        /// </summary>
        [JsonProperty("with", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? With { get; set; }

        /// <summary>
        /// The field on the other side: the field of the given identity or of the record named by with.
        /// </summary>
        [JsonProperty("with_field", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? WithField { get; set; }

        /// <summary>
        /// The item's weight in bits, to two places: log2 of how much likelier the evidence is for the same person than for a stranger. Positive raises the likelihood and negative lowers it.
        /// </summary>
        [JsonProperty("bits", Required = Newtonsoft.Json.Required.Always)]
        public double Bits { get; set; } = default!;
    }
}