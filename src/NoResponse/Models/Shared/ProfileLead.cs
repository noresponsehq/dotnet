#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    public class ProfileLead
    {
        /// <summary>
        /// A value copied exactly from a linked record that the person was not searched by.
        /// </summary>
        [JsonProperty("value", Required = Newtonsoft.Json.Required.Always)]
        public string Value { get; set; } = default!;

        [JsonProperty("kind", Required = Newtonsoft.Json.Required.Always)]
        public string Kind { get; set; } = default!;

        [JsonProperty("why", Required = Newtonsoft.Json.Required.Always)]
        public string Why { get; set; } = default!;

        [JsonProperty("evidence", Required = Newtonsoft.Json.Required.Always)]
        public List<ProfileCitation> Evidence { get; set; } = default!;
    }
}