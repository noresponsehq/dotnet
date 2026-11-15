#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Create or reuse a workspace person.
    /// </summary>
    public class WorkspacePersonResult
    {
        [JsonProperty("person", Required = Newtonsoft.Json.Required.Always)]
        public WorkspacePerson Person { get; set; } = new();

        [JsonProperty("suggested_matches", Required = Newtonsoft.Json.Required.Always)]
        public List<PersonMatch> SuggestedMatches { get; set; } = default!;

        [JsonProperty("created", Required = Newtonsoft.Json.Required.Always)]
        public bool Created { get; set; } = default!;
    }
}