#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Utils;

    public class DownloadCustomerExportChunkRequest
    {
        /// <summary>
        /// Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.
        /// </summary>
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=workspace_id")]
        public string WorkspaceId { get; set; } = default!;

        [NoResponseMetadata("pathParam:style=simple,explode=false,name=export_id")]
        public string ExportId { get; set; } = default!;

        [NoResponseMetadata("pathParam:style=simple,explode=false,name=chunk_index")]
        public long ChunkIndex { get; set; } = default!;
    }
}