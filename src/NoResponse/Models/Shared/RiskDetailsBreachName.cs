#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System.Collections.Generic;

    public class RiskDetailsBreachName
    {
        [JsonProperty("value", Required = Newtonsoft.Json.Required.Always)]
        public string Value { get; set; } = default!;

        [JsonProperty("seen_in", Required = Newtonsoft.Json.Required.Always)]
        public long SeenIn { get; set; } = default!;

        /// <summary>
        /// The domains of the breached sites whose tables hold the name, lower case, each once, newest breach first, at most 32: the domain values of breaches[]. Covers the email's and the username's tables together. Absent when none of the tables has a site.
        /// </summary>
        [JsonProperty("sites", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<string>? Sites { get; set; }

        /// <summary>
        /// How many of the tables that hold the name have no site, such as a credential list or a stealer log. Absent when none.
        /// </summary>
        [JsonProperty("unsited", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Unsited { get; set; }
    }
}