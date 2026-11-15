#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Adds 1 to 500 workspace people to the group. Reuses an explicit person_id or external_id. Matching email or phone values only suggest people; automatic merging is disabled.
    /// </summary>
    public class PersonBatchRequest
    {
        [JsonProperty("people", Required = Newtonsoft.Json.Required.Always)]
        public List<PersonIdentity> People { get; set; } = default!;
    }
}