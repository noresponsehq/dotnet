#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Retained jobs, newest first. Follow next_before for another page.
    /// </summary>
    public class PolicyEvaluationList
    {
        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<PolicyEvaluation> Items { get; set; } = default!;

        [JsonProperty("next_before", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? NextBefore { get; set; }
    }
}