#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class PolicyEvaluationRequestCosts
    {
        [JsonProperty("legitimate", Required = Newtonsoft.Json.Required.Always)]
        public Cost Legitimate { get; set; } = new();

        [JsonProperty("abuse", Required = Newtonsoft.Json.Required.Always)]
        public Cost Abuse { get; set; } = new();
    }
}