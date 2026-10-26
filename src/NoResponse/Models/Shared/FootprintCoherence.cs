#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class FootprintCoherence
    {
        /// <summary>
        /// How far independent sources agree on one attribute: values is the number of distinct values reported, support the tables and profiles reporting any, and agreement match (one value), partial (the most reported value holds more than half the support) or mismatch. The values themselves are not served.
        /// </summary>
        [JsonProperty("birth_date", Required = Newtonsoft.Json.Required.DisallowNull)]
        public AttributeAgreement? BirthDate { get; set; }

        /// <summary>
        /// How far independent sources agree on one attribute: values is the number of distinct values reported, support the tables and profiles reporting any, and agreement match (one value), partial (the most reported value holds more than half the support) or mismatch. The values themselves are not served.
        /// </summary>
        [JsonProperty("gender", Required = Newtonsoft.Json.Required.DisallowNull)]
        public AttributeAgreement? Gender { get; set; }

        /// <summary>
        /// How far independent sources agree on one attribute: values is the number of distinct values reported, support the tables and profiles reporting any, and agreement match (one value), partial (the most reported value holds more than half the support) or mismatch. The values themselves are not served.
        /// </summary>
        [JsonProperty("nationality", Required = Newtonsoft.Json.Required.DisallowNull)]
        public AttributeAgreement? Nationality { get; set; }

        /// <summary>
        /// How far independent sources agree on one attribute: values is the number of distinct values reported, support the tables and profiles reporting any, and agreement match (one value), partial (the most reported value holds more than half the support) or mismatch. The values themselves are not served.
        /// </summary>
        [JsonProperty("country", Required = Newtonsoft.Json.Required.DisallowNull)]
        public AttributeAgreement? Country { get; set; }

        /// <summary>
        /// How far independent sources agree on one attribute: values is the number of distinct values reported, support the tables and profiles reporting any, and agreement match (one value), partial (the most reported value holds more than half the support) or mismatch. The values themselves are not served.
        /// </summary>
        [JsonProperty("city", Required = Newtonsoft.Json.Required.DisallowNull)]
        public AttributeAgreement? City { get; set; }

        /// <summary>
        /// How far independent sources agree on one attribute: values is the number of distinct values reported, support the tables and profiles reporting any, and agreement match (one value), partial (the most reported value holds more than half the support) or mismatch. The values themselves are not served.
        /// </summary>
        [JsonProperty("zip", Required = Newtonsoft.Json.Required.DisallowNull)]
        public AttributeAgreement? Zip { get; set; }

        /// <summary>
        /// How far independent sources agree on one attribute: values is the number of distinct values reported, support the tables and profiles reporting any, and agreement match (one value), partial (the most reported value holds more than half the support) or mismatch. The values themselves are not served.
        /// </summary>
        [JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull)]
        public AttributeAgreement? Name { get; set; }

        /// <summary>
        /// How far independent sources agree on one attribute: values is the number of distinct values reported, support the tables and profiles reporting any, and agreement match (one value), partial (the most reported value holds more than half the support) or mismatch. The values themselves are not served.
        /// </summary>
        [JsonProperty("company", Required = Newtonsoft.Json.Required.DisallowNull)]
        public AttributeAgreement? Company { get; set; }

        /// <summary>
        /// How far independent sources agree on one attribute: values is the number of distinct values reported, support the tables and profiles reporting any, and agreement match (one value), partial (the most reported value holds more than half the support) or mismatch. The values themselves are not served.
        /// </summary>
        [JsonProperty("website", Required = Newtonsoft.Json.Required.DisallowNull)]
        public AttributeAgreement? Website { get; set; }

        /// <summary>
        /// How far independent sources agree on one attribute: values is the number of distinct values reported, support the tables and profiles reporting any, and agreement match (one value), partial (the most reported value holds more than half the support) or mismatch. The values themselves are not served.
        /// </summary>
        [JsonProperty("social", Required = Newtonsoft.Json.Required.DisallowNull)]
        public AttributeAgreement? Social { get; set; }
    }
}