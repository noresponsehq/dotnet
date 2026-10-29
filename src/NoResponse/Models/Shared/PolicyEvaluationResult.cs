#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>
    /// The evaluation report and its provenance. A no_change report cannot authorise publication.
    /// </summary>
    public class PolicyEvaluationResult
    {
        [JsonProperty("evaluation", Required = Newtonsoft.Json.Required.Always)]
        public PolicyEvaluation Evaluation { get; set; } = new();

        [JsonProperty("report", Required = Newtonsoft.Json.Required.Always)]
        public Report Report { get; set; } = new();
    }
}