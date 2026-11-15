#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Utils;

    public class GetInvestigationRequest
    {
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=investigation_id")]
        public string InvestigationId { get; set; } = default!;
    }
}