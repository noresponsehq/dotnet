#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Normalized multi-factor evidence from a verified provider session.
    /// </summary>
    public class AuthContextMfa
    {
        [JsonProperty("present", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? Present { get; set; }

        [JsonProperty("methods", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<string>? Methods { get; set; }

        [JsonProperty("age_seconds", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? AgeSeconds { get; set; }
    }
}