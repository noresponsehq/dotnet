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
    /// The decision for each flow: the account's score under that flow's thresholds. A flow the workspace has not set uses its account_creation thresholds, so the flows differ only where it set them.
    /// </summary>
    public class AccountDecisionsByFlow
    {
        /// <summary>
        /// block when the score reached your block threshold (risk.block of the assessment), otherwise the assessment's recommendation. Your application applies it.
        /// </summary>
        [JsonProperty("account_creation", Required = Newtonsoft.Json.Required.Always)]
        public AccountDecision AccountCreation { get; set; } = default!;

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