#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class ReviewWorkspacePersonChangeRequest
    {
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=person_id")]
        public string PersonId { get; set; } = default!;

        [NoResponseMetadata("pathParam:style=simple,explode=false,name=change_id")]
        public string ChangeId { get; set; } = default!;

        [NoResponseMetadata("request:mediaType=application/json")]
        public ScreeningCancelRequest Body { get; set; } = new();
    }
}