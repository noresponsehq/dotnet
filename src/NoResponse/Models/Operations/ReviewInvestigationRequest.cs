#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class ReviewInvestigationRequest
    {
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=investigation_id")]
        public string InvestigationId { get; set; } = default!;

        [NoResponseMetadata("request:mediaType=application/json")]
        public PersonReviewRequest Body { get; set; } = new();
    }
}