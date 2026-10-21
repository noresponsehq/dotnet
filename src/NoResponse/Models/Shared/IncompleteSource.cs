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
    /// One evidence lane that did not settle, and why. It names a lane, not a source. A lane that missed its deadline is timed_out rather than not_found.
    /// </summary>
    public class IncompleteSource
    {
        /// <summary>
        /// An evidence lane: accounts (account presence for email and username), phone, breach and ip.
        /// </summary>
        [JsonProperty("lane", Required = Newtonsoft.Json.Required.Always)]
        public Lane Lane { get; set; } = default!;

        /// <summary>
        /// Why a lane did not settle. deferred means the lane is asynchronous and still running; a continuation settles it. timed_out means the lane missed its deadline. insufficient_evidence is a settled inconclusive signal rather than an incomplete lane.
        /// </summary>
        [JsonProperty("reason", Required = Newtonsoft.Json.Required.Always)]
        public IncompleteReason Reason { get; set; } = default!;
    }
}