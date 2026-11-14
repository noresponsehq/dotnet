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
    /// How distinctive a searchable value is: an email, phone, username, password, hash, person name or IP. Absent for other values, and when its rating could not be worked out, such as when a count failed.
    /// </summary>
    public class RecordDistinctive
    {
        /// <summary>
        /// unique: an email or phone, which belongs to one person by design. distinctive: rare enough that a new search on it likely finds one person. common: shared by many people, so a search on it finds strangers. A new search is suggested for unique and distinctive values only; any value can still be searched.
        /// </summary>
        [JsonProperty("rating", Required = Newtonsoft.Json.Required.Always)]
        public Rating Rating { get; set; } = default!;

        /// <summary>
        /// Why, in plain words, such as "seen 3,861,493 times in breach data", "in 4 breach rows" or "38 bits, about one in 430 billion usernames".
        /// </summary>
        [JsonProperty("reason", Required = Newtonsoft.Json.Required.Always)]
        public string Reason { get; set; } = default!;
    }
}