#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// Current customer-reported labels, not independently established ground truth. Missing keys mean zero. Unknown is an explicit report; absent or expired feedback is unreported.
    /// </summary>
    public class OutcomesByOutcome
    {
        [JsonProperty("confirmed_abuse", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? ConfirmedAbuse { get; set; }

        [JsonProperty("account_takeover", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? AccountTakeover { get; set; }

        [JsonProperty("chargeback", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Chargeback { get; set; }

        [JsonProperty("false_positive", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? FalsePositive { get; set; }

        [JsonProperty("legitimate", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Legitimate { get; set; }

        [JsonProperty("unknown", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Unknown { get; set; }
    }
}