#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Utils;

    public class DeleteGroupRequest
    {
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=group_id")]
        public string GroupId { get; set; } = default!;
    }
}