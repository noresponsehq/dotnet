#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// The 202 answer of POST /v1/key_usage/batches. A replay with the same Idempotency-Key and body answers the same record and counts nothing again.
    /// </summary>
    public class KeyUsageBatch
    {
        /// <summary>
        /// Opaque, server-assigned id of the batch: 'kub_' and a 26-character Crockford base32 ULID.
        /// </summary>
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        /// <summary>
        /// When NoResponse first recorded the batch.
        /// </summary>
        [JsonProperty("received_at", Required = Newtonsoft.Json.Required.Always)]
        public string ReceivedAt { get; set; } = default!;

        /// <summary>
        /// Events counted.
        /// </summary>
        [JsonProperty("accepted", Required = Newtonsoft.Json.Required.Always)]
        public long Accepted { get; set; } = default!;

        /// <summary>
        /// Events that added nothing: a private, reserved or unreadable ip, or an occurred_at more than 24 hours old or more than 5 minutes ahead.
        /// </summary>
        [JsonProperty("ignored", Required = Newtonsoft.Json.Required.Always)]
        public long Ignored { get; set; } = default!;
    }
}