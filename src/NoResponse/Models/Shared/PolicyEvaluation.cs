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

    public class PolicyEvaluation
    {
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        [JsonProperty("environment", Required = Newtonsoft.Json.Required.Always)]
        public AuditSettingsEnvironment Environment { get; set; } = default!;

        [JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public PolicyEvaluationStatus Status { get; set; } = default!;

        [JsonProperty("policy_version", Required = Newtonsoft.Json.Required.Always)]
        public long PolicyVersion { get; set; } = default!;

        [JsonProperty("scoring_policy", Required = Newtonsoft.Json.Required.Always)]
        public string ScoringPolicy { get; set; } = default!;

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;

        [JsonProperty("expires_at", Required = Newtonsoft.Json.Required.Always)]
        public string ExpiresAt { get; set; } = default!;

        [JsonProperty("captured_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? CapturedAt { get; set; }

        [JsonProperty("finished_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? FinishedAt { get; set; }

        [JsonProperty("accounts", Required = Newtonsoft.Json.Required.Always)]
        public long Accounts { get; set; } = default!;

        [JsonProperty("requests", Required = Newtonsoft.Json.Required.Always)]
        public long Requests { get; set; } = default!;

        [JsonProperty("evaluator_version", Required = Newtonsoft.Json.Required.Always)]
        public string EvaluatorVersion { get; } = "identity-threshold-evaluator-v2";

        [JsonProperty("evaluator_sha256", Required = Newtonsoft.Json.Required.Always)]
        public string EvaluatorSha256 { get; set; } = default!;

        [JsonProperty("build_version", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? BuildVersion { get; set; }

        [JsonProperty("result_status", Required = Newtonsoft.Json.Required.DisallowNull)]
        public ResultStatus? ResultStatus { get; set; }

        [JsonProperty("error_code", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ErrorCode { get; set; }

        [JsonProperty("published_policy_version", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? PublishedPolicyVersion { get; set; }

        [JsonProperty("manifest_sha256", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ManifestSha256 { get; set; }

        [JsonProperty("input_sha256", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? InputSha256 { get; set; }

        [JsonProperty("cohort_sha256", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? CohortSha256 { get; set; }

        [JsonProperty("result_sha256", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ResultSha256 { get; set; }
    }
}