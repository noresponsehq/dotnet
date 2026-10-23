#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>
    /// A few typed facts per identifier. Every fact is optional: absent means the evidence did not answer and makes no claim, while false or 0 is a definite answer. Lanes that do not exist yet leave their members absent.
    /// </summary>
    public class RiskSummary
    {
        [JsonProperty("phone", Required = Newtonsoft.Json.Required.DisallowNull)]
        public RiskSummaryPhone? Phone { get; set; }

        [JsonProperty("email", Required = Newtonsoft.Json.Required.DisallowNull)]
        public RiskSummaryEmail? Email { get; set; }

        [JsonProperty("username", Required = Newtonsoft.Json.Required.DisallowNull)]
        public RiskSummaryUsername? Username { get; set; }

        /// <summary>
        /// Facts about the submitted IP from the offline database and this workspace's own recent assessments. Every member is optional; absent makes no claim.
        /// </summary>
        [JsonProperty("ip", Required = Newtonsoft.Json.Required.DisallowNull)]
        public IpSummary? Ip { get; set; }

        /// <summary>
        /// Cross-identifier agreement, computed from facts only. Every member is optional; absent makes no claim.
        /// </summary>
        [JsonProperty("consistency", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Consistency? Consistency { get; set; }
    }
}