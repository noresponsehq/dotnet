#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class RiskSummaryUsername
    {
        /// <summary>
        /// Accounts found for the username.
        /// </summary>
        [JsonProperty("accounts_found", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? AccountsFound { get; set; }

        /// <summary>
        /// Breach facts for one selector. found is the one required member: false is a definite 'in no breach table'. Every other member is optional, and absent makes no claim. No member carries a breach value, password or hash, and unknown members are refused. truncated means the value matched more tables or records than the lookup keeps, so absence-based claims (a missing name match, for example) are withheld; a truncated result is always found.
        /// </summary>
        [JsonProperty("breach", Required = Newtonsoft.Json.Required.DisallowNull)]
        public BreachSummary? Breach { get; set; }
    }
}