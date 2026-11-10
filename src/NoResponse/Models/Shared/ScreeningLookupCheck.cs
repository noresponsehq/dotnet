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
    /// What came of one lane of the lookups for one identifier.
    /// </summary>
    public class ScreeningLookupCheck
    {
        [JsonProperty("identifier", Required = Newtonsoft.Json.Required.Always)]
        public IdentifierSearchKind Identifier { get; set; } = default!;

        /// <summary>
        /// An evidence lane: accounts (account presence for email and username), phone, breach, domain (the email domain's registration) and ip.
        /// </summary>
        [JsonProperty("lane", Required = Newtonsoft.Json.Required.Always)]
        public Lane Lane { get; set; } = default!;

        /// <summary>
        /// found: a source found the identifier. not_found: every source answered and none found it. partial: the sources that answered found nothing and the others did not answer. not_covered: no source answered. A lookup that failed reads partial or not_covered, not not_found.
        /// </summary>
        [JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public ScreeningLookupCheckStatus Status { get; set; } = default!;

        /// <summary>
        /// Sources asked: none when no source could be, as when the source is stopped or your workspace refuses it.
        /// </summary>
        [JsonProperty("sources", Required = Newtonsoft.Json.Required.Always)]
        public long Sources { get; set; } = default!;

        /// <summary>
        /// Sources that gave a definite answer.
        /// </summary>
        [JsonProperty("answered", Required = Newtonsoft.Json.Required.Always)]
        public long Answered { get; set; } = default!;

        /// <summary>
        /// Sources that found the identifier.
        /// </summary>
        [JsonProperty("found", Required = Newtonsoft.Json.Required.Always)]
        public long Found { get; set; } = default!;

        /// <summary>
        /// Why a signal or source did not produce a definite found/not_found. timed_out means the source missed the request deadline; a continuation may still settle it. deferred means the (source, identifier) pair is async-only and was not attempted on the synchronous path; a continuation settles it.
        /// </summary>
        [JsonProperty("reason", Required = Newtonsoft.Json.Required.DisallowNull)]
        public FailureReason? Reason { get; set; }
    }
}