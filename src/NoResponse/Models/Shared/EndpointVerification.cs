#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    public class EndpointVerification
    {
        /// <summary>
        /// Opaque id of the endpoint registration being verified.
        /// </summary>
        [JsonProperty("endpoint_id", Required = Newtonsoft.Json.Required.Always)]
        public string EndpointId { get; set; } = default!;

        /// <summary>
        /// Endpoint verification nonce: 32 random bytes, unpadded base64url (43 characters). Fresh per verification.
        /// </summary>
        [JsonProperty("challenge", Required = Newtonsoft.Json.Required.Always)]
        public string Challenge { get; set; } = default!;
    }
}