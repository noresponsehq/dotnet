#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ListPolicyEvaluationsResponse
    {
        public Func<Task<ListPolicyEvaluationsResponse?>>? Next { get; set; }
        public Dictionary<string, List<string>> Headers { get; set; } = new Dictionary<string, List<string>>();

        [NoResponseMetadata("")]
        public PolicyEvaluationList Result { get; set; } = new();
    }
}