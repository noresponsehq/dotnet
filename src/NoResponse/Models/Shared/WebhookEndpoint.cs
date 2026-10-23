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
    /// One of the workspace's registered webhook endpoints. Signing secrets are not part of it: a secret is shown once, when it is issued (POST /v1/webhooks and POST /v1/webhooks/{endpoint_id}/rotate).
    /// </summary>
    public class WebhookEndpoint
    {
        /// <summary>
        /// Opaque id of the endpoint registration.
        /// </summary>
        [JsonProperty("endpoint_id", Required = Newtonsoft.Json.Required.Always)]
        public string EndpointId { get; set; } = default!;

        /// <summary>
        /// The destination URL, as registered.
        /// </summary>
        [JsonProperty("url", Required = Newtonsoft.Json.Required.Always)]
        public string Url { get; set; } = default!;

        /// <summary>
        /// unverified until the endpoint echoes an endpoint.verification challenge (send one with POST /v1/webhooks/{endpoint_id}/test); verified receives assessment.updated events; disabled after 72 hours of continuous delivery failure, and verified again by the same challenge.
        /// </summary>
        [JsonProperty("state", Required = Newtonsoft.Json.Required.Always)]
        public WebhookEndpointState State { get; set; } = default!;

        /// <summary>
        /// standard returns the consolidated risk response; detailed adds merged provider-neutral facts when the key has detailed scope. There is no raw mode.
        /// </summary>
        [JsonProperty("response_mode", Required = Newtonsoft.Json.Required.Always)]
        public ResponseMode ResponseMode { get; set; } = default!;

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;

        /// <summary>
        /// When the endpoint last echoed a challenge. Absent until it first does.
        /// </summary>
        [JsonProperty("verified_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? VerifiedAt { get; set; }

        /// <summary>
        /// The keys a receiver may see in NR-Webhook-Key-Id, oldest first. The last one signs every new delivery; an older one keeps verifying until its expires_at.
        /// </summary>
        [JsonProperty("signing_keys", Required = Newtonsoft.Json.Required.Always)]
        public List<SigningKey> SigningKeys { get; set; } = default!;
    }
}