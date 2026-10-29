#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Utils;

    public class GetErasureReceiptRequest
    {
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=erasure_id")]
        public string ErasureId { get; set; } = default!;
    }
}