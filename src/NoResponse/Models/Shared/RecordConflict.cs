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
    /// Two identifiers you gave that lead to records of different people.
    /// </summary>
    public class RecordConflict
    {
        /// <summary>
        /// anchors_disagree: records found by two of the identifiers you gave clash with each other, so those identifiers may belong to different people.
        /// </summary>
        [JsonProperty("signal", Required = Newtonsoft.Json.Required.Always)]
        public RecordConflictSignal Signal { get; set; } = default!;

        /// <summary>
        /// The kind of value that clashes: person_name or born.
        /// </summary>
        [JsonProperty("kind", Required = Newtonsoft.Json.Required.Always)]
        public string Kind { get; set; } = default!;

        /// <summary>
        /// The two identifiers you gave whose records disagree.
        /// </summary>
        [JsonProperty("between", Required = Newtonsoft.Json.Required.Always)]
        public List<Between> Between { get; set; } = default!;

        /// <summary>
        /// The ids of the two records that clash, in the order of between.
        /// </summary>
        [JsonProperty("records", Required = Newtonsoft.Json.Required.Always)]
        public List<string> Records { get; set; } = default!;
    }
}