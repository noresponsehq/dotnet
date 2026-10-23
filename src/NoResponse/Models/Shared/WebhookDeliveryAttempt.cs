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

    public class WebhookDeliveryAttempt
    {
        /// <summary>
        /// The event id the receiver saw as NR-Webhook-Id and as the body's id. Replay it with POST /v1/webhooks/{endpoint_id}/deliveries/{event_id}/replay.
        /// </summary>
        [JsonProperty("event_id", Required = Newtonsoft.Json.Required.Always)]
        public string EventId { get; set; } = default!;

        [JsonProperty("event_type", Required = Newtonsoft.Json.Required.Always)]
        public EventType EventType { get; set; } = default!;

        /// <summary>
        /// Which attempt of this delivery this was: 1 for the first send, up to 9 with retries. A replay counts its own attempts from 1.
        /// </summary>
        [JsonProperty("attempt", Required = Newtonsoft.Json.Required.Always)]
        public long Attempt { get; set; } = default!;

        /// <summary>
        /// true for an attempt of a replay you asked for.
        /// </summary>
        [JsonProperty("replay", Required = Newtonsoft.Json.Required.Always)]
        public bool Replay { get; set; } = default!;

        /// <summary>
        /// delivered when the endpoint answered 2xx; failed otherwise, with failure naming why.
        /// </summary>
        [JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public WebhookDeliveryAttemptStatus Status { get; set; } = default!;

        /// <summary>
        /// The HTTP status the endpoint answered. Absent when no response was read.
        /// </summary>
        [JsonProperty("response_code", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? ResponseCode { get; set; }

        /// <summary>
        /// Why the attempt failed: the URL is no longer valid, the host did not resolve, it resolved to an address that is not public, no answer within 5 seconds, the connection failed, a 3xx (redirects are not followed) or another non-2xx status. Absent for a delivered attempt.
        /// </summary>
        [JsonProperty("failure", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Failure? Failure { get; set; }

        /// <summary>
        /// Milliseconds from sending the request to the answer or the failure.
        /// </summary>
        [JsonProperty("latency_ms", Required = Newtonsoft.Json.Required.Always)]
        public long LatencyMs { get; set; } = default!;

        /// <summary>
        /// When the request was sent. Its NR-Webhook-Timestamp is this time in Unix seconds.
        /// </summary>
        [JsonProperty("attempted_at", Required = Newtonsoft.Json.Required.Always)]
        public string AttemptedAt { get; set; } = default!;
    }
}