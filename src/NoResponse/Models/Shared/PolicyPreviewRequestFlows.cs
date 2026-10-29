#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class PolicyPreviewRequestFlows
    {
        [JsonProperty("account_creation", Required = Newtonsoft.Json.Required.DisallowNull)]
        public PolicyPreviewRequestFlowsAccountCreation? AccountCreation { get; set; }

        [JsonProperty("login", Required = Newtonsoft.Json.Required.DisallowNull)]
        public PolicyPreviewRequestFlowsLogin? Login { get; set; }

        [JsonProperty("purchase", Required = Newtonsoft.Json.Required.DisallowNull)]
        public PolicyPreviewRequestFlowsPurchase? Purchase { get; set; }
    }
}