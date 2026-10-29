#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class UpdateMemberRequest
    {
        /// <summary>
        /// Your workspace id, as each key lists it in workspace_id. Another workspace, a malformed id and a test environment's id answer 404.
        /// </summary>
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=workspace_id")]
        public string WorkspaceId { get; set; } = default!;

        /// <summary>
        /// The member's mem_ id. A member of another workspace, or a removed one, answers 404.
        /// </summary>
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=member_id")]
        public string MemberId { get; set; } = default!;

        [NoResponseMetadata("request:mediaType=application/json")]
        public MemberUpdateRequest Body { get; set; } = new();
    }
}