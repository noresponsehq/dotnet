#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    public class PersonMatch
    {
        [JsonProperty("person_id", Required = Newtonsoft.Json.Required.Always)]
        public string PersonId { get; set; } = default!;

        [JsonProperty("matched_on", Required = Newtonsoft.Json.Required.Always)]
        public List<PersonMatchMatchedOn> MatchedOn { get; set; } = default!;
    }
}