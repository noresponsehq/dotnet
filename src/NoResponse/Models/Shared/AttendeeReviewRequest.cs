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
    /// Records a decision on an attendee.
    /// </summary>
    public class AttendeeReviewRequest
    {
        [JsonProperty("decision", Required = Newtonsoft.Json.Required.Always)]
        public AttendeeDecision Decision { get; set; } = default!;

        /// <summary>
        /// Why, for the venue team and later reviewers.
        /// </summary>
        [JsonProperty("note", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Note { get; set; }
    }
}