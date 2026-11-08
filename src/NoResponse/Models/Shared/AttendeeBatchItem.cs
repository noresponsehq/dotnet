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

    public class AttendeeBatchItem
    {
        /// <summary>
        /// The row's position in the request.
        /// </summary>
        [JsonProperty("index", Required = Newtonsoft.Json.Required.Always)]
        public long Index { get; set; } = default!;

        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        /// <summary>
        /// unchanged: the row matched an attendee with the same details.
        /// </summary>
        [JsonProperty("outcome", Required = Newtonsoft.Json.Required.Always)]
        public AttendeeBatchItemOutcome Outcome { get; set; } = default!;
    }
}