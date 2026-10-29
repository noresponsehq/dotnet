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

    public class ReviewResolveRequest
    {
        [JsonProperty("expected_version", Required = Newtonsoft.Json.Required.Always)]
        public long ExpectedVersion { get; set; } = default!;

        [JsonProperty("expected_risk_version", Required = Newtonsoft.Json.Required.Always)]
        public long ExpectedRiskVersion { get; set; } = default!;

        [JsonProperty("conclusion", Required = Newtonsoft.Json.Required.Always)]
        public Conclusion Conclusion { get; set; } = default!;

        [JsonProperty("note", Required = Newtonsoft.Json.Required.Always)]
        public string Note { get; set; } = default!;
    }
}