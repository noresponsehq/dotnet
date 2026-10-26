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

    public class AccountRisk
    {
        /// <summary>
        /// 0 (safe) to 100 (risky), as risk.score of the assessment.
        /// </summary>
        [JsonProperty("score", Required = Newtonsoft.Json.Required.Always)]
        public long Score { get; set; } = default!;

        /// <summary>
        /// block when the score reached your block threshold (risk.block of the assessment), otherwise the assessment's recommendation. held only for a flow whose admission is held (settings flows.&lt;flow&gt;.admission), when the account's current assessment was made for that flow and its investigation has not completed (status partial): the final decision arrives as account.risk_changed when the investigation completes, decided by its score, or when the hold's deadline (hold_seconds after the assessment was created) passes first, as the flow's on_expiry action or the decision the evidence so far gives, whichever is stricter. Your application applies it.
        /// </summary>
        [JsonProperty("decision", Required = Newtonsoft.Json.Required.Always)]
        public AccountDecision Decision { get; set; } = default!;

        /// <summary>
        /// Every reason that fired: risk-raising reasons first, then neutral, then risk-lowering, each strongest impact band first and then by code. The policy's numeric weights are kept private, so a reason carries only its direction and impact band.
        /// </summary>
        [JsonProperty("reasons", Required = Newtonsoft.Json.Required.Always)]
        public List<AccountRiskReason> Reasons { get; set; } = default!;
    }
}