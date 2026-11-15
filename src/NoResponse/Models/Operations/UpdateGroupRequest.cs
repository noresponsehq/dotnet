#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class UpdateGroupRequest
    {
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=group_id")]
        public string GroupId { get; set; } = default!;

        [NoResponseMetadata("request:mediaType=application/json")]
        public GroupUpdate Body { get; set; } = new();
    }
}