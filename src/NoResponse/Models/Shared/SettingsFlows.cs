#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class SettingsFlows
    {
        /// <summary>
        /// One flow's policy in force. A score below step_up is allow, below review step_up, and from review review; from block, when present, the decision is block. admission says when the flow's decision is final.
        /// </summary>
        [JsonProperty("account_creation", Required = Newtonsoft.Json.Required.Always)]
        public FlowSettings AccountCreation { get; set; } = new();

        /// <summary>
        /// One flow's policy in force. A score below step_up is allow, below review step_up, and from review review; from block, when present, the decision is block. admission says when the flow's decision is final.
        /// </summary>
        [JsonProperty("login", Required = Newtonsoft.Json.Required.Always)]
        public FlowSettings Login { get; set; } = new();

        /// <summary>
        /// One flow's policy in force. A score below step_up is allow, below review step_up, and from review review; from block, when present, the decision is block. admission says when the flow's decision is final.
        /// </summary>
        [JsonProperty("purchase", Required = Newtonsoft.Json.Required.Always)]
        public FlowSettings Purchase { get; set; } = new();
    }
}