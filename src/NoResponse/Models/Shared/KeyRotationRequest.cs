#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// POST /v1/keys/rotate body. The workspace and the key being rotated are the API key that signs the request, so neither is a body member. An empty object takes the default overlap.
    /// </summary>
    public class KeyRotationRequest
    {
        /// <summary>
        /// How long the presented key keeps working beside the new one, from 60 seconds to 7 days. Default 86400 (24 hours). At the end of the overlap the presented key stops authenticating.
        /// </summary>
        [JsonProperty("overlap_seconds", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? OverlapSeconds { get; set; }
    }
}