#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Utils;

    public class ListAccountsRequest
    {
        /// <summary>
        /// next_cursor from an earlier page, unchanged. Absent, the list starts from the oldest change.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=changed_since")]
        public string? ChangedSince { get; set; }

        /// <summary>
        /// Most accounts per page.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=limit")]
        public long? Limit { get; set; } = 50;

        /// <summary>
        /// true lists only accounts with a decision other than allow for some flow and no applied enforcement at their current risk_version.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=unenforced")]
        public bool? Unenforced { get; set; } = false;
    }
}