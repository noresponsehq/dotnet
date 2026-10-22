#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// One signing key, by id. The secret is not included.
    /// </summary>
    public class SigningKey
    {
        /// <summary>
        /// The signing key id, sent as NR-Webhook-Key-Id on every delivery it signs.
        /// </summary>
        [JsonProperty("key_id", Required = Newtonsoft.Json.Required.Always)]
        public string KeyId { get; set; } = default!;

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;

        /// <summary>
        /// Present on a key a rotation replaced: it stops verifying at this instant, 7 days after the rotation. Absent on the key that signs.
        /// </summary>
        [JsonProperty("expires_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ExpiresAt { get; set; }
    }
}