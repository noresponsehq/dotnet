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
    /// A reviewer's verdict on whether one of a person's lookup records is theirs.
    /// </summary>
    public class RecordVerdictRequest
    {
        /// <summary>
        /// One of the person's lookup records.
        /// </summary>
        [JsonProperty("record_id", Required = Newtonsoft.Json.Required.Always)]
        public string RecordId { get; set; } = default!;

        /// <summary>
        /// A reviewer's overrule (POST .../people/{person_id}/records), which decides joined whatever the likelihood and sticks for as long as a lookup finds the same record. Absent when nobody overruled it.
        /// </summary>
        [JsonProperty("verdict", Required = Newtonsoft.Json.Required.Always)]
        public Verdict Verdict { get; set; } = default!;
    }
}