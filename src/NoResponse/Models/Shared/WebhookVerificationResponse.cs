#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// The body an endpoint returns, with status 200 and Content-Type application/json within 5 seconds, to an endpoint.verification event. A 2xx without the echoed challenge does not verify the endpoint.
    /// </summary>
    public class WebhookVerificationResponse
    {
        /// <summary>
        /// Endpoint verification nonce: 32 random bytes, unpadded base64url (43 characters). Fresh per verification.
        /// </summary>
        [JsonProperty("challenge", Required = Newtonsoft.Json.Required.Always)]
        public string Challenge { get; set; } = default!;
    }
}