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

    public class ProfileStatement
    {
        [JsonProperty("topic", Required = Newtonsoft.Json.Required.Always)]
        public Topic Topic { get; set; } = default!;

        [JsonProperty("text", Required = Newtonsoft.Json.Required.Always)]
        public string Text { get; set; } = default!;

        /// <summary>
        /// confirmed: a linked record or the searched identifier states it directly. likely: a strong inference. possible: a weak inference.
        /// </summary>
        [JsonProperty("certainty", Required = Newtonsoft.Json.Required.Always)]
        public Certainty Certainty { get; set; } = default!;

        /// <summary>
        /// stated: read from a record. inferred: reasoned from several facts. absence: every source answered and none found it.
        /// </summary>
        [JsonProperty("basis", Required = Newtonsoft.Json.Required.Always)]
        public Basis Basis { get; set; } = default!;

        /// <summary>
        /// Other explanations to keep open for an inference.
        /// </summary>
        [JsonProperty("alternatives", Required = Newtonsoft.Json.Required.Always)]
        public List<string> Alternatives { get; set; } = default!;

        /// <summary>
        /// Every record, post or check the statement rests on.
        /// </summary>
        [JsonProperty("evidence", Required = Newtonsoft.Json.Required.Always)]
        public List<ProfileCitation> Evidence { get; set; } = default!;
    }
}