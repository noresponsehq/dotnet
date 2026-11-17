#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class JudgeWorkspacePersonRecordRequest
    {
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=person_id")]
        public string PersonId { get; set; } = default!;

        [NoResponseMetadata("request:mediaType=application/json")]
        public RecordVerdictRequest Body { get; set; } = new();
    }
}