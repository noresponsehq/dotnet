#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Utils;

    public class ListKeysRequest
    {
        /// <summary>
        /// Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.
        /// </summary>
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=workspace_id")]
        public string WorkspaceId { get; set; } = default!;
    }
}