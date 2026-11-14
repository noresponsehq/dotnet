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
    /// An account the person is known to hold.
    /// </summary>
    public class Handle
    {
        /// <summary>
        /// A platform a screening can cover. synthetic is the deterministic connector's own platform, for exercising the product end to end with no network.
        /// </summary>
        [JsonProperty("platform", Required = Newtonsoft.Json.Required.Always)]
        public HandlePlatform Platform { get; set; } = default!;

        /// <summary>
        /// The account's handle, without a leading @.
        /// </summary>
        [JsonProperty("handle", Required = Newtonsoft.Json.Required.Always)]
        public string HandleValue { get; set; } = default!;
    }
}