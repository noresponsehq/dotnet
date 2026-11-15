#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class CancelScreeningRequest
    {
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=group_id")]
        public string GroupId { get; set; } = default!;

        [NoResponseMetadata("pathParam:style=simple,explode=false,name=screening_id")]
        public string ScreeningId { get; set; } = default!;

        [NoResponseMetadata("request:mediaType=application/json")]
        public ScreeningCancelRequest Body { get; set; } = new();
    }
}