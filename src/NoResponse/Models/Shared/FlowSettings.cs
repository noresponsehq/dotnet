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
    /// One flow's policy in force. A score below step_up is allow, below review step_up, and from review review; from block, when present, the decision is block. admission says when the flow's decision is final.
    /// </summary>
    public class FlowSettings
    {
        [JsonProperty("step_up", Required = Newtonsoft.Json.Required.Always)]
        public long StepUp { get; set; } = default!;

        [JsonProperty("review", Required = Newtonsoft.Json.Required.Always)]
        public long Review { get; set; } = default!;

        /// <summary>
        /// The score from which the decision is block. Absent while blocking is off, the default.
        /// </summary>
        [JsonProperty("block", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Block { get; set; }

        /// <summary>
        /// assessments only.
        /// </summary>
        [JsonProperty("default_depth", Required = Newtonsoft.Json.Required.Always)]
        public UsageCountDepth DefaultDepth { get; set; } = default!;

        /// <summary>
        /// true while this flow has no thresholds of its own and uses account_creation's, the default for login and purchase. Always false for signup.
        /// </summary>
        [JsonProperty("inherits_account_creation", Required = Newtonsoft.Json.Required.Always)]
        public bool InheritsAccountCreation { get; set; } = default!;

        /// <summary>
        /// immediate (the default): an assessment of this flow is decided from the evidence its answer has, and later evidence may change the decision. held: while the account's current assessment of this flow has not completed, the account's decision for the flow is held, and the final decision arrives as account.risk_changed when the investigation completes or hold_seconds pass, whichever is first. Each flow has its own admission.
        /// </summary>
        [JsonProperty("admission", Required = Newtonsoft.Json.Required.Always)]
        public Admission Admission { get; set; } = default!;

        /// <summary>
        /// How long a held flow's assessment is held after it was created, 30 to 86400 seconds (default 300). Kept while the flow is immediate.
        /// </summary>
        [JsonProperty("hold_seconds", Required = Newtonsoft.Json.Required.Always)]
        public long HoldSeconds { get; set; } = default!;

        /// <summary>
        /// The decision a held assessment takes when hold_seconds pass before its investigation completes, unless the evidence so far gives a stricter one (default review). It is announced as account.risk_changed like any other change, so an expired hold is always announced. A hosted provider hook, which cannot wait, answers a held account by it at once: deny for block, allow otherwise. Kept while the flow is immediate.
        /// </summary>
        [JsonProperty("on_expiry", Required = Newtonsoft.Json.Required.Always)]
        public OnExpiry OnExpiry { get; set; } = default!;
    }
}