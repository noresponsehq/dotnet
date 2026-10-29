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

    public class Report
    {
        [JsonProperty("evaluator_version", Required = Newtonsoft.Json.Required.Always)]
        public string EvaluatorVersion { get; } = "identity-threshold-evaluator-v2";

        [JsonProperty("input_sha256", Required = Newtonsoft.Json.Required.Always)]
        public string InputSha256 { get; set; } = default!;

        [JsonProperty("cohort_sha256", Required = Newtonsoft.Json.Required.Always)]
        public string CohortSha256 { get; set; } = default!;

        [JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public ResultStatus Status { get; set; } = default!;

        [JsonProperty("candidate", Required = Newtonsoft.Json.Required.Always)]
        public Thresholds Candidate { get; set; } = new();

        [JsonProperty("scope", Required = Newtonsoft.Json.Required.Always)]
        public ReportScope Scope { get; set; } = new();
    }
}