#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// Immutable publication provenance remains in policy history after evaluation evidence expires. SHA-256 values identify the frozen manifest, evaluator semantic input, cohort, and canonical result separately.
    /// </summary>
    public class Publication
    {
        [JsonProperty("manifest_sha256", Required = Newtonsoft.Json.Required.Always)]
        public string ManifestSha256 { get; set; } = default!;

        [JsonProperty("input_sha256", Required = Newtonsoft.Json.Required.Always)]
        public string InputSha256 { get; set; } = default!;

        [JsonProperty("cohort_sha256", Required = Newtonsoft.Json.Required.Always)]
        public string CohortSha256 { get; set; } = default!;

        [JsonProperty("result_sha256", Required = Newtonsoft.Json.Required.Always)]
        public string ResultSha256 { get; set; } = default!;

        [JsonProperty("evaluator_sha256", Required = Newtonsoft.Json.Required.Always)]
        public string EvaluatorSha256 { get; set; } = default!;

        [JsonProperty("evaluation_id", Required = Newtonsoft.Json.Required.Always)]
        public string EvaluationId { get; set; } = default!;

        [JsonProperty("evaluator_version", Required = Newtonsoft.Json.Required.Always)]
        public string EvaluatorVersion { get; } = "identity-threshold-evaluator-v2";

        [JsonProperty("build_version", Required = Newtonsoft.Json.Required.Always)]
        public string BuildVersion { get; set; } = default!;

        [JsonProperty("captured_at", Required = Newtonsoft.Json.Required.Always)]
        public string CapturedAt { get; set; } = default!;

        [JsonProperty("published_at", Required = Newtonsoft.Json.Required.Always)]
        public string PublishedAt { get; set; } = default!;
    }
}