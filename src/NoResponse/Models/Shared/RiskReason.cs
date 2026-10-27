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
    /// One reason that fired. direction is whether it raises or lowers the score, or is neutral (informational, it moves nothing). impact is a coarse band of how much it moves the score: none for a neutral reason, otherwise low, medium or high.
    /// </summary>
    public class RiskReason
    {
        /// <summary>
        /// A stable reason code. New codes may appear; ignore any you do not know.
        /// </summary>
        [JsonProperty("code", Required = Newtonsoft.Json.Required.Always)]
        public ReasonCode Code { get; set; } = default!;

        [JsonProperty("direction", Required = Newtonsoft.Json.Required.Always)]
        public Direction Direction { get; set; } = default!;

        [JsonProperty("impact", Required = Newtonsoft.Json.Required.Always)]
        public Impact Impact { get; set; } = default!;

        /// <summary>
        /// The submitted identifier or client context field a reason concerns; absent when it concerns the whole assessment. Client context is a claimed hint, not a device fingerprint.
        /// </summary>
        [JsonProperty("identifier", Required = Newtonsoft.Json.Required.DisallowNull)]
        public ReasonIdentifier? Identifier { get; set; }

        /// <summary>
        /// The list entry a list_match reason applied. No other reason carries it.
        /// </summary>
        [JsonProperty("list_entry_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ListEntryId { get; set; }
    }
}