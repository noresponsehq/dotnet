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

    public class RiskDetailsAttributeConflict
    {
        /// <summary>
        /// name_gender is the records' gender against the submitted first name's usual one.
        /// </summary>
        [JsonProperty("attribute", Required = Newtonsoft.Json.Required.Always)]
        public Attribute Attribute { get; set; } = default!;

        /// <summary>
        /// The domains of the breached sites whose tables report the attribute, newest breach first, at most 32. Absent when none of the tables has a site.
        /// </summary>
        [JsonProperty("sites", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<string>? Sites { get; set; }

        /// <summary>
        /// How many of the tables reporting the attribute have no site, such as a credential list or a stealer log. Absent when none.
        /// </summary>
        [JsonProperty("unsited", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Unsited { get; set; }
    }
}