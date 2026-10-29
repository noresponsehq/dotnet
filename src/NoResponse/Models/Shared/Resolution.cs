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

    public class Resolution
    {
        [JsonProperty("review_version", Required = Newtonsoft.Json.Required.Always)]
        public long ReviewVersion { get; set; } = default!;

        [JsonProperty("risk_version", Required = Newtonsoft.Json.Required.Always)]
        public long RiskVersion { get; set; } = default!;

        [JsonProperty("assessment_version", Required = Newtonsoft.Json.Required.Always)]
        public long AssessmentVersion { get; set; } = default!;

        [JsonProperty("assessment_id", Required = Newtonsoft.Json.Required.Always)]
        public string AssessmentId { get; set; } = default!;

        [JsonProperty("conclusion", Required = Newtonsoft.Json.Required.Always)]
        public Conclusion Conclusion { get; set; } = default!;

        [JsonProperty("note", Required = Newtonsoft.Json.Required.Always)]
        public string Note { get; set; } = default!;

        [JsonProperty("created_by", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedBy { get; set; } = default!;

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;
    }
}