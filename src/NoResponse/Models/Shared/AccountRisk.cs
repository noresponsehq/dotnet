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
        /// block when the score reached your block threshold (risk.block of the assessment), otherwise the assessment's recommendation. Your application applies it.
        /// </summary>
        [JsonProperty("decision", Required = Newtonsoft.Json.Required.Always)]
        public AccountDecision Decision { get; set; } = default!;

        /// <summary>
        /// Every reason that fired, highest weight first. The weights sum to score before clamping.
        /// </summary>
        [JsonProperty("reasons", Required = Newtonsoft.Json.Required.Always)]
        public List<AccountRiskReason> Reasons { get; set; } = default!;
    }
}