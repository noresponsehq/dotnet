#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ListAccountCredentialsResponse
    {
        public Func<Task<ListAccountCredentialsResponse?>>? Next { get; set; }
        public Dictionary<string, List<string>> Headers { get; set; } = new Dictionary<string, List<string>>();

        [NoResponseMetadata("")]
        public CredentialPage Result { get; set; } = new();
    }
}