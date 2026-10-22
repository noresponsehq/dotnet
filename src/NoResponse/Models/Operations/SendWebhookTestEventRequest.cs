#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class SendWebhookTestEventRequest
    {
        /// <summary>
        /// The endpoint's whe_ id. An id that is unknown, malformed or another tenant's answers 404.
        /// </summary>
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=endpoint_id")]
        public string EndpointId { get; set; } = default!;

        [NoResponseMetadata("request:mediaType=application/json")]
        public WebhookTestRequest Body { get; set; } = new();
    }
}