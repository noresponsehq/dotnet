#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Utils;

    public class DeleteWorkspacePersonRequest
    {
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=person_id")]
        public string PersonId { get; set; } = default!;
    }
}