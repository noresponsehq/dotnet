#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ListAccountOutcomesResponse
    {
        public Func<Task<ListAccountOutcomesResponse?>>? Next { get; set; }
        public Dictionary<string, List<string>> Headers { get; set; } = new Dictionary<string, List<string>>();

        [NoResponseMetadata("")]
        public AccountOutcomePage Result { get; set; } = new();
    }
}