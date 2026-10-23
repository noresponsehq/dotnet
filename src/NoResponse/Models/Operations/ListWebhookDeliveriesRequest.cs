#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Utils;

    public class ListWebhookDeliveriesRequest
    {
        /// <summary>
        /// The endpoint's whe_ id. An id that is unknown, malformed or another tenant's answers 404.
        /// </summary>
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=endpoint_id")]
        public string EndpointId { get; set; } = default!;

        /// <summary>
        /// Attempts per page.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=limit")]
        public long? Limit { get; set; } = 20;

        /// <summary>
        /// next_cursor from the previous page, unchanged.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=cursor")]
        public string? Cursor { get; set; }
    }
}