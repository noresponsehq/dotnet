#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Utils;

    public class RevokeKeyRequest
    {
        /// <summary>
        /// Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.
        /// </summary>
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=workspace_id")]
        public string WorkspaceId { get; set; } = default!;

        /// <summary>
        /// The key's id. A key of another workspace answers 404, as does a key of the other environment to an API key.
        /// </summary>
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=key_id")]
        public string KeyId { get; set; } = default!;
    }
}