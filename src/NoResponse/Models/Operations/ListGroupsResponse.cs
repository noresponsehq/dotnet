#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    public class ListGroupsResponse
    {
        public Dictionary<string, List<string>> Headers { get; set; } = new Dictionary<string, List<string>>();

        [NoResponseMetadata("")]
        public GroupList Result { get; set; } = new();
    }
}