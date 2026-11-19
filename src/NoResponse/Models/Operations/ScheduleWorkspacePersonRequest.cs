#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class ScheduleWorkspacePersonRequest
    {
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=person_id")]
        public string PersonId { get; set; } = default!;

        [NoResponseMetadata("request:mediaType=application/json")]
        public PersonScheduleRequest Body { get; set; } = new();
    }
}