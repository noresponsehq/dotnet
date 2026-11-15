#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class ReviewPersonRequest
    {
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=group_id")]
        public string GroupId { get; set; } = default!;

        [NoResponseMetadata("pathParam:style=simple,explode=false,name=person_id")]
        public string PersonId { get; set; } = default!;

        [NoResponseMetadata("request:mediaType=application/json")]
        public PersonReviewRequest Body { get; set; } = new();
    }
}