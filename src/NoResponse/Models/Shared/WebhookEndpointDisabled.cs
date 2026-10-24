#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// One of the environment's endpoints stopped receiving events: every delivery attempt to it failed from failing_since until disabled_at, 72 hours or more. Its queued deliveries are dropped. Fix the receiver and verify it again with POST /v1/webhooks/{endpoint_id}/test (type endpoint.verification) to resume delivery.
    /// </summary>
    public class WebhookEndpointDisabled
    {
        /// <summary>
        /// The endpoint that was disabled.
        /// </summary>
        [JsonProperty("endpoint_id", Required = Newtonsoft.Json.Required.Always)]
        public string EndpointId { get; set; } = default!;

        /// <summary>
        /// The first failed attempt of the unbroken failure streak.
        /// </summary>
        [JsonProperty("failing_since", Required = Newtonsoft.Json.Required.Always)]
        public string FailingSince { get; set; } = default!;

        /// <summary>
        /// When the endpoint was disabled; equal to the event's created_at.
        /// </summary>
        [JsonProperty("disabled_at", Required = Newtonsoft.Json.Required.Always)]
        public string DisabledAt { get; set; } = default!;
    }
}