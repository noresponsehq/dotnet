#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    public class PolicyEvaluationRequestLimits
    {
        [JsonProperty("review_rate", Required = Newtonsoft.Json.Required.Always)]
        public double ReviewRate { get; set; } = default!;

        [JsonProperty("server_error_rate", Required = Newtonsoft.Json.Required.Always)]
        public double ServerErrorRate { get; set; } = default!;

        [JsonProperty("unavailable_result_rate", Required = Newtonsoft.Json.Required.Always)]
        public double UnavailableResultRate { get; set; } = default!;

        [JsonProperty("p95_ms", Required = Newtonsoft.Json.Required.Always)]
        public double P95Ms { get; set; } = default!;
    }
}