#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Utils;

    public class GetUsageRequest
    {
        /// <summary>
        /// Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.
        /// </summary>
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=workspace_id")]
        public string WorkspaceId { get; set; } = default!;

        /// <summary>
        /// The first UTC day, as YYYY-MM-DD.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=from")]
        public string? From { get; set; }

        /// <summary>
        /// The last UTC day, included, as YYYY-MM-DD; no later than today.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=to")]
        public string? To { get; set; }
    }
}