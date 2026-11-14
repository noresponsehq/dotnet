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
    /// A recorded verdict on one record. A later verdict on the same record replaces it.
    /// </summary>
    public class RecordVerdict
    {
        [JsonProperty("record_id", Required = Newtonsoft.Json.Required.Always)]
        public string RecordId { get; set; } = default!;

        /// <summary>
        /// A reviewer's overrule (POST .../people/{person_id}/records), which decides joined whatever the likelihood and sticks for as long as a lookup finds the same record. Absent when nobody overruled it.
        /// </summary>
        [JsonProperty("verdict", Required = Newtonsoft.Json.Required.Always)]
        public Verdict Verdict { get; set; } = default!;

        [JsonProperty("decided_by", Required = Newtonsoft.Json.Required.Always)]
        public string DecidedBy { get; set; } = default!;

        [JsonProperty("decided_at", Required = Newtonsoft.Json.Required.Always)]
        public string DecidedAt { get; set; } = default!;
    }
}