#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>
    /// The 201 answer of POST /v1/webhooks: the new endpoint, unverified, and its first signing secret, shown once. Store the secret where the receiver reads it, then send endpoint.verification with POST /v1/webhooks/{endpoint_id}/test.
    /// </summary>
    public class WebhookEndpointCreated
    {
        /// <summary>
        /// One of the workspace's registered webhook endpoints. Signing secrets are not part of it: a secret is shown once, when it is issued (POST /v1/webhooks and POST /v1/webhooks/{endpoint_id}/rotate).
        /// </summary>
        [JsonProperty("endpoint", Required = Newtonsoft.Json.Required.Always)]
        public WebhookEndpoint Endpoint { get; set; } = new();

        /// <summary>
        /// A newly issued signing key with its secret, shown only in this answer. A receiver verifies NR-Webhook-Signature with the secret's UTF-8 bytes as the HMAC-SHA256 key.
        /// </summary>
        [JsonProperty("signing_secret", Required = Newtonsoft.Json.Required.Always)]
        public SigningSecret SigningSecret { get; set; } = new();
    }
}