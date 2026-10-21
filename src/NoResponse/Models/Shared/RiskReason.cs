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

    public class RiskReason
    {
        /// <summary>
        /// A stable reason code. New codes may appear; ignore any you do not know.
        /// </summary>
        [JsonProperty("code", Required = Newtonsoft.Json.Required.Always)]
        public ReasonCode Code { get; set; } = default!;

        [JsonProperty("weight", Required = Newtonsoft.Json.Required.Always)]
        public long Weight { get; set; } = default!;

        /// <summary>
        /// The submitted identifier or client context field a reason concerns; absent when it concerns the whole assessment. Client context is a claimed hint, not a device fingerprint.
        /// </summary>
        [JsonProperty("identifier", Required = Newtonsoft.Json.Required.DisallowNull)]
        public ReasonIdentifier? Identifier { get; set; }
    }
}