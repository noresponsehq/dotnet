#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Utils;

    public class ListAccountDecisionsRequest
    {
        /// <summary>
        /// The account_id the workspace sends with its assessments, as one path segment (percent-encode '|' and ':' if the client requires it). A value that does not match the account_id shape answers 400 invalid_request.
        /// </summary>
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=account_id")]
        public string AccountId { get; set; } = default!;

        /// <summary>
        /// Items per page.
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