#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Operations;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class UpdateAuditRetentionRequest
    {
        /// <summary>
        /// Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.
        /// </summary>
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=workspace_id")]
        public string WorkspaceId { get; set; } = default!;

        [NoResponseMetadata("pathParam:style=simple,explode=false,name=environment")]
        public Models.Operations.Environment Environment { get; set; } = default!;

        [NoResponseMetadata("request:mediaType=application/json")]
        public AuditSettingsRequest Body { get; set; } = new();
    }
}