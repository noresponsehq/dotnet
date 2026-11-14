#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Adds or updates 1 to 500 people. A row naming an existing person of the group (the same external_id, else the same email, else the same phone) updates it.
    /// </summary>
    public class PersonBatchRequest
    {
        [JsonProperty("people", Required = Newtonsoft.Json.Required.Always)]
        public List<PersonIdentity> People { get; set; } = default!;
    }
}