#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class ReplayWebhookDeliveryRequest
    {
        /// <summary>
        /// The endpoint's whe_ id. An id that is unknown, malformed or another tenant's answers 404.
        /// </summary>
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=endpoint_id")]
        public string EndpointId { get; set; } = default!;

        /// <summary>
        /// The evt_ id of a delivery the endpoint's log holds, as GET /v1/webhooks/{endpoint_id}/deliveries lists it. An id that is malformed, not sent to this endpoint, past the log's retention or another tenant's answers 404.
        /// </summary>
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=event_id")]
        public string EventId { get; set; } = default!;

        [NoResponseMetadata("request:mediaType=application/json")]
        public WebhookReplayRequest Body { get; set; } = new();
    }
}