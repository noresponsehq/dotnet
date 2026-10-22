#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// A newly issued signing key with its secret, shown only in this answer. A receiver verifies NR-Webhook-Signature with the secret's UTF-8 bytes as the HMAC-SHA256 key.
    /// </summary>
    public class SigningSecret
    {
        /// <summary>
        /// The signing key id, sent as NR-Webhook-Key-Id on every delivery it signs.
        /// </summary>
        [JsonProperty("key_id", Required = Newtonsoft.Json.Required.Always)]
        public string KeyId { get; set; } = default!;

        /// <summary>
        /// The signing secret: 32 random bytes as 43 characters of unpadded base64url. It is not stored in readable form or returned again.
        /// </summary>
        [JsonProperty("secret", Required = Newtonsoft.Json.Required.Always)]
        public string Secret { get; set; } = default!;

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;
    }
}