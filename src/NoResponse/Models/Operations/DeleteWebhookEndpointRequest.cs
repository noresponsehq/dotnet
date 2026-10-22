#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Utils;

    public class DeleteWebhookEndpointRequest
    {
        /// <summary>
        /// The endpoint's whe_ id. An id that is unknown, malformed or another tenant's answers 404.
        /// </summary>
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=endpoint_id")]
        public string EndpointId { get; set; } = default!;
    }
}