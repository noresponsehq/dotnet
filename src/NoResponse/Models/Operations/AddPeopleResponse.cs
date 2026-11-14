#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    public class AddPeopleResponse
    {
        public Dictionary<string, List<string>> Headers { get; set; } = new Dictionary<string, List<string>>();

        [NoResponseMetadata("")]
        public PersonBatch Result { get; set; } = new();
    }
}