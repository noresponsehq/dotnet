#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Utils;

    public class GetPersonRequest
    {
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=group_id")]
        public string GroupId { get; set; } = default!;

        [NoResponseMetadata("pathParam:style=simple,explode=false,name=person_id")]
        public string PersonId { get; set; } = default!;
    }
}