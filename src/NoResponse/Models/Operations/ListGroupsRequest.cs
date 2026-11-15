#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Utils;

    public class ListGroupsRequest
    {
        /// <summary>
        /// At most this many items.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=limit")]
        public long? Limit { get; set; } = 20;

        /// <summary>
        /// Read groups older than this one.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=before")]
        public string? Before { get; set; }
    }
}