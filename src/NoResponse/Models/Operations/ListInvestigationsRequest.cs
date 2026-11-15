#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Utils;

    public class ListInvestigationsRequest
    {
        [NoResponseMetadata("queryParam:style=form,explode=true,name=person_id")]
        public string? PersonId { get; set; }

        [NoResponseMetadata("queryParam:style=form,explode=true,name=before")]
        public string? Before { get; set; }

        [NoResponseMetadata("queryParam:style=form,explode=true,name=limit")]
        public long? Limit { get; set; } = 100;
    }
}