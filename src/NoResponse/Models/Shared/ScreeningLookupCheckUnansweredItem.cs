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

    public class ScreeningLookupCheckUnansweredItem
    {
        /// <summary>
        /// The id of the connector that did not answer: breach for the breach search.
        /// </summary>
        [JsonProperty("connector", Required = Newtonsoft.Json.Required.Always)]
        public string Connector { get; set; } = default!;

        /// <summary>
        /// Why a signal or source did not produce a definite found/not_found. timed_out means the source missed the request deadline; a continuation may still settle it. deferred means the (source, identifier) pair is async-only and was not attempted on the synchronous path; a continuation settles it.
        /// </summary>
        [JsonProperty("reason", Required = Newtonsoft.Json.Required.Always)]
        public FailureReason Reason { get; set; } = default!;
    }
}