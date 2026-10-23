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
    /// The decision for each flow. Every flow uses the tenant's signup thresholds today, so all three equal risk.decision.
    /// </summary>
    public class AccountDecisionsByFlow
    {
        /// <summary>
        /// block when the score reached your block threshold (risk.block of the assessment), otherwise the assessment's recommendation. Your application applies it.
        /// </summary>
        [JsonProperty("signup", Required = Newtonsoft.Json.Required.Always)]
        public AccountDecision Signup { get; set; } = default!;

        /// <summary>
        /// block when the score reached your block threshold (risk.block of the assessment), otherwise the assessment's recommendation. Your application applies it.
        /// </summary>
        [JsonProperty("login", Required = Newtonsoft.Json.Required.Always)]
        public AccountDecision Login { get; set; } = default!;

        /// <summary>
        /// block when the score reached your block threshold (risk.block of the assessment), otherwise the assessment's recommendation. Your application applies it.
        /// </summary>
        [JsonProperty("purchase", Required = Newtonsoft.Json.Required.Always)]
        public AccountDecision Purchase { get; set; } = default!;
    }
}