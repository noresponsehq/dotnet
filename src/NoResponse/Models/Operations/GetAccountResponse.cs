#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    public class GetAccountResponse
    {
        public Dictionary<string, List<string>> Headers { get; set; } = new Dictionary<string, List<string>>();

        [NoResponseMetadata("")]
        public Account Result { get; set; } = new();
    }
}