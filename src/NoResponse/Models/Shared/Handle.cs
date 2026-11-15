#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// An account the person is known to hold.
    /// </summary>
    public class Handle
    {
        /// <summary>
        /// A platform a screening can cover, such as instagram, x, bluesky, youtube, tiktok or github. The screening registry owns the set: a request that names a platform it does not cover answers 422. A new platform adds an id without changing this contract, so read a platform you do not know in a response as one you have not seen before, not as an error. synthetic is the deterministic connector's own platform, for exercising the product end to end with no network.
        /// </summary>
        [JsonProperty("platform", Required = Newtonsoft.Json.Required.Always)]
        public string Platform { get; set; } = default!;

        /// <summary>
        /// The account's handle, without a leading @.
        /// </summary>
        [JsonProperty("handle", Required = Newtonsoft.Json.Required.Always)]
        public string HandleValue { get; set; } = default!;
    }
}