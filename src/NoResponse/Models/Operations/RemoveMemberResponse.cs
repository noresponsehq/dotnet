#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Utils;
    using System.Collections.Generic;

    public class RemoveMemberResponse
    {
        public Dictionary<string, List<string>> Headers { get; set; } = new Dictionary<string, List<string>>();
    }
}