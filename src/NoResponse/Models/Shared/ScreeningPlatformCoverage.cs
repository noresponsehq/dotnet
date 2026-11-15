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
    /// What a screening read on one platform.
    /// </summary>
    public class ScreeningPlatformCoverage
    {
        /// <summary>
        /// A platform a screening can cover, such as instagram, x, bluesky, youtube, tiktok or github. The screening registry owns the set: a request that names a platform it does not cover answers 422. A new platform adds an id without changing this contract, so read a platform you do not know in a response as one you have not seen before, not as an error. synthetic is the deterministic connector's own platform, for exercising the product end to end with no network.
        /// </summary>
        [JsonProperty("platform", Required = Newtonsoft.Json.Required.Always)]
        public string Platform { get; set; } = default!;

        /// <summary>
        /// covered read at least one profile; none_found looked and found no account; inaccessible found accounts that are private or locked; failed could not finish looking; unavailable has no resolver or collector enabled.
        /// </summary>
        [JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public ScreeningPlatformCoverageStatus Status { get; set; } = default!;

        [JsonProperty("accounts_found", Required = Newtonsoft.Json.Required.Always)]
        public long AccountsFound { get; set; } = default!;

        [JsonProperty("items_read", Required = Newtonsoft.Json.Required.Always)]
        public long ItemsRead { get; set; } = default!;
    }
}