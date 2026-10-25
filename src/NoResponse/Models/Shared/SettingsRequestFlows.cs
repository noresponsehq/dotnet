#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>
    /// Use account_creation or its deprecated alias signup, not both.
    /// </summary>
    public class SettingsRequestFlows
    {
        /// <summary>
        /// A change to one flow. A field left out keeps the value in force. Thresholds set on login or purchase start from the values in force, account_creation's while it inherits them. After the change, 0 &lt; step_up &lt; review &lt;= 100 and block is off or review &lt;= block &lt;= 100.
        /// </summary>
        [JsonProperty("account_creation", Required = Newtonsoft.Json.Required.DisallowNull)]
        public FlowChange? AccountCreation { get; set; }

        /// <summary>
        /// A change to one flow. A field left out keeps the value in force. Thresholds set on login or purchase start from the values in force, account_creation's while it inherits them. After the change, 0 &lt; step_up &lt; review &lt;= 100 and block is off or review &lt;= block &lt;= 100.
        /// </summary>
        [JsonProperty("login", Required = Newtonsoft.Json.Required.DisallowNull)]
        public FlowChange? Login { get; set; }

        /// <summary>
        /// A change to one flow. A field left out keeps the value in force. Thresholds set on login or purchase start from the values in force, account_creation's while it inherits them. After the change, 0 &lt; step_up &lt; review &lt;= 100 and block is off or review &lt;= block &lt;= 100.
        /// </summary>
        [JsonProperty("purchase", Required = Newtonsoft.Json.Required.DisallowNull)]
        public FlowChange? Purchase { get; set; }

        /// <summary>
        /// A change to one flow. A field left out keeps the value in force. Thresholds set on login or purchase start from the values in force, account_creation's while it inherits them. After the change, 0 &lt; step_up &lt; review &lt;= 100 and block is off or review &lt;= block &lt;= 100.
        /// </summary>
        [JsonProperty("signup", Required = Newtonsoft.Json.Required.DisallowNull)]
        public FlowChange? Signup { get; set; }
    }
}