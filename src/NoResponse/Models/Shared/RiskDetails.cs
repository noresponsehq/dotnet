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
        /// The matching breach tables, newest first, at most 100.
        /// </summary>
        [JsonProperty("breaches", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<Breach>? Breaches { get; set; }

        /// <summary>
        /// Names found next to the identifiers in breach records, merged, with the number of distinct tables each was seen in and the sites of those tables.
        /// </summary>
        [JsonProperty("breach_names", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<RiskDetailsBreachName>? BreachNames { get; set; }

        /// <summary>
        /// The identity's dated footprint events, newest first, at most 300: the year and month, what happened (created, last_active, breached), the site, its category, the kind of evidence and the identifier it belongs to. No breach value: dates are coarsened to the month.
        /// </summary>
        [JsonProperty("events", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<RiskDetailsEvent>? Events { get; set; }

        /// <summary>
        /// Every online-account search of the email or username: the platform, what the search came to and, for an account found, what the platform returned beside it. Found first, then not found, then searches that failed or are still running, each by platform name; at most 64. A failed or running search keeps its own outcome and is not reported as not_found.
        /// </summary>
        [JsonProperty("accounts", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<OnlineAccount>? Accounts { get; set; }

        /// <summary>
        /// What an identity_attribute_conflict reason weighed: each attribute the records disagree on and the sites reporting it. Absent when the reason did not fire, and on an answer stored before conflicts were recorded. No value is served.
        /// </summary>
        [JsonProperty("attribute_conflicts", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<RiskDetailsAttributeConflict>? AttributeConflicts { get; set; }
    }
}