#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Detailed view only (response_mode=detailed, which needs the detailed scope); a standard answer leaves it out. Merged observations without provider, source, origin, raw breach record, value or score.
    /// </summary>
    public class RiskDetails
    {
        [JsonProperty("phone_names", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<RiskDetailsPhoneName>? PhoneNames { get; set; }

        /// <summary>
        /// The matching breach tables, newest first, at most 50.
        /// </summary>
        [JsonProperty("breaches", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<Breach>? Breaches { get; set; }

        /// <summary>
        /// Names found next to the identifiers in breach records, merged, with the number of distinct tables each was seen in.
        /// </summary>
        [JsonProperty("breach_names", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<RiskDetailsBreachName>? BreachNames { get; set; }
    }
}