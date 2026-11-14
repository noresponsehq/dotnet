#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    public class UpdateGroupResponse
    {
        public Dictionary<string, List<string>> Headers { get; set; } = new Dictionary<string, List<string>>();

        [NoResponseMetadata("")]
        public Group Result { get; set; } = new();
    }
}