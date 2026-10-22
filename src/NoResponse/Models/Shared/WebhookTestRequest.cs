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
    /// POST /v1/webhooks/{endpoint_id}/test body: which event to send.
    /// </summary>
    public class WebhookTestRequest
    {
        /// <summary>
        /// endpoint.verification sends a fresh challenge now, in this call, and the endpoint is verified if it echoes it. assessment.updated queues an event whose data is a fixed example assessment, with test true, delivered and retried like any other; the endpoint must be verified.
        /// </summary>
        [JsonProperty("event", Required = Newtonsoft.Json.Required.Always)]
        public WebhookTestRequestEvent Event { get; set; } = default!;
    }
}