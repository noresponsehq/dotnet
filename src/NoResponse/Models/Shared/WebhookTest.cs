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
    /// The 202 answer of POST /v1/webhooks/{endpoint_id}/test: the event that was sent or queued.
    /// </summary>
    public class WebhookTest
    {
        /// <summary>
        /// The event id the receiver sees as NR-Webhook-Id and as the body's id.
        /// </summary>
        [JsonProperty("delivery_id", Required = Newtonsoft.Json.Required.Always)]
        public string DeliveryId { get; set; } = default!;

        /// <summary>
        /// Opaque id of the endpoint registration.
        /// </summary>
        [JsonProperty("endpoint_id", Required = Newtonsoft.Json.Required.Always)]
        public string EndpointId { get; set; } = default!;

        /// <summary>
        /// endpoint.verification sends a fresh challenge now, in this call, and the endpoint is verified if it echoes it. assessment.updated queues an event whose data is a fixed example assessment, with test true, delivered and retried like any other; the endpoint must be verified.
        /// </summary>
        [JsonProperty("event", Required = Newtonsoft.Json.Required.Always)]
        public WebhookTestRequestEvent Event { get; set; } = default!;

        /// <summary>
        /// unverified until the endpoint echoes an endpoint.verification challenge (send one with POST /v1/webhooks/{endpoint_id}/test); verified receives assessment.updated events; disabled after 72 hours of continuous delivery failure, and verified again by the same challenge.
        /// </summary>
        [JsonProperty("state", Required = Newtonsoft.Json.Required.Always)]
        public WebhookEndpointState State { get; set; } = default!;

        /// <summary>
        /// endpoint.verification only: true when the endpoint echoed the challenge within 5 seconds. A false answer leaves the endpoint's state as it was.
        /// </summary>
        [JsonProperty("verified", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? Verified { get; set; }
    }
}