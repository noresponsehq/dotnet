#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A change to one flow. A field left out keeps the value in force. Thresholds set on login or purchase start from the values in force, account_creation's while it inherits them. After the change, 0 &lt; step_up &lt; review &lt;= 100 and block is off or review &lt;= block &lt;= 100.
    /// </summary>
    public class FlowChange
    {
        [JsonProperty("step_up", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? StepUp { get; set; }

        [JsonProperty("review", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Review { get; set; }

        /// <summary>
        /// The score from which the decision is block; null turns blocking off.
        /// </summary>
        [JsonProperty("block", Required = Newtonsoft.Json.Required.Default, NullValueHandling = NullValueHandling.Include)]
        public OptionalNullable<long?> Block { get; set; }
        public bool ShouldSerializeBlock() => Block.IsSet;

        /// <summary>
        /// The depth a request for this flow that names none is served at. Defaults: full for account_creation and purchase, quick for login.
        /// </summary>
        [JsonProperty("default_depth", Required = Newtonsoft.Json.Required.DisallowNull)]
        public DefaultDepth? DefaultDepth { get; set; }

        /// <summary>
        /// Login or purchase only: drop the flow's own thresholds so it uses account_creation's again. Not combined with step_up, review or block. Send true; false is refused.
        /// </summary>
        [JsonProperty("inherits_account_creation", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? InheritsAccountCreation { get; set; }

        /// <summary>
        /// Alias of inherits_account_creation, still accepted. Send true; false is refused.
        /// </summary>
        [Obsolete("This field will be removed in a future release, please migrate away from it as soon as possible")]
        [JsonProperty("inherits_signup", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? InheritsSignup { get; set; }

        /// <summary>
        /// immediate (the default): an assessment of this flow is decided from the evidence its answer has, and later evidence may change the decision. held: while the account's current assessment of this flow has not completed, the account's decision for the flow is held, and the final decision arrives as account.risk_changed when the investigation completes or hold_seconds pass, whichever is first. Each flow has its own admission.
        /// </summary>
        [JsonProperty("admission", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Admission? Admission { get; set; }

        /// <summary>
        /// How long a held flow's assessment is held after it was created, 30 to 86400 seconds (default 300). Kept while the flow is immediate.
        /// </summary>
        [JsonProperty("hold_seconds", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? HoldSeconds { get; set; }

        /// <summary>
        /// The decision a held assessment takes when hold_seconds pass before its investigation completes, unless the evidence so far gives a stricter one (default review). It is announced as account.risk_changed like any other change, so an expired hold is always announced. A hosted provider hook, which cannot wait, answers a held account by it at once: deny for block, allow otherwise. Kept while the flow is immediate.
        /// </summary>
        [JsonProperty("on_expiry", Required = Newtonsoft.Json.Required.DisallowNull)]
        public OnExpiry? OnExpiry { get; set; }
    }
}