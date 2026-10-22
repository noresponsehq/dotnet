#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>
    /// The 201 answer of POST /v1/webhooks/{endpoint_id}/rotate: the new signing secret, shown once, which signs every delivery from now on, and when the key it replaced stops verifying. Until then a receiver accepts both.
    /// </summary>
    public class WebhookKeyRotation
    {
        /// <summary>
        /// Opaque id of the endpoint registration.
        /// </summary>
        [JsonProperty("endpoint_id", Required = Newtonsoft.Json.Required.Always)]
        public string EndpointId { get; set; } = default!;

        /// <summary>
        /// A newly issued signing key with its secret, shown only in this answer. A receiver verifies NR-Webhook-Signature with the secret's UTF-8 bytes as the HMAC-SHA256 key.
        /// </summary>
        [JsonProperty("signing_secret", Required = Newtonsoft.Json.Required.Always)]
        public SigningSecret SigningSecret { get; set; } = new();

        /// <summary>
        /// The key that signed until this rotation.
        /// </summary>
        [JsonProperty("previous_key_id", Required = Newtonsoft.Json.Required.Always)]
        public string PreviousKeyId { get; set; } = default!;

        /// <summary>
        /// When the previous key stops verifying: 7 days after the rotation.
        /// </summary>
        [JsonProperty("previous_key_expires_at", Required = Newtonsoft.Json.Required.Always)]
        public string PreviousKeyExpiresAt { get; set; } = default!;
    }
}