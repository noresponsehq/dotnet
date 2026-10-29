#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Utils;

    public class ListRequestDiagnosticsRequest
    {
        /// <summary>
        /// Inclusive start of a range of at most seven days.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=from")]
        public string From { get; set; } = default!;

        /// <summary>
        /// Exclusive end of the range.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=to")]
        public string To { get; set; } = default!;

        /// <summary>
        /// Optional filter or paging parameter.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=request_id")]
        public string? RequestId { get; set; }

        /// <summary>
        /// Optional filter or paging parameter.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=route")]
        public string? Route { get; set; }

        /// <summary>
        /// Optional filter or paging parameter.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=status")]
        public long? Status { get; set; }

        /// <summary>
        /// Optional filter or paging parameter.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=limit")]
        public long? Limit { get; set; } = 50;

        /// <summary>
        /// Optional filter or paging parameter.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=cursor")]
        public string? Cursor { get; set; }

        /// <summary>
        /// Filter by the public authenticated API key identifier; older unattributed rows do not match.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=api_key_id")]
        public string? ApiKeyId { get; set; }
    }
}