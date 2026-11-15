#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class CancelInvestigationRequest
    {
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=investigation_id")]
        public string InvestigationId { get; set; } = default!;

        [NoResponseMetadata("request:mediaType=application/json")]
        public ScreeningCancelRequest Body { get; set; } = new();
    }
}