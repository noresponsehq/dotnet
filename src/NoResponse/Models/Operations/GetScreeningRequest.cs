#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Utils;

    public class GetScreeningRequest
    {
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=group_id")]
        public string GroupId { get; set; } = default!;

        [NoResponseMetadata("pathParam:style=simple,explode=false,name=screening_id")]
        public string ScreeningId { get; set; } = default!;
    }
}