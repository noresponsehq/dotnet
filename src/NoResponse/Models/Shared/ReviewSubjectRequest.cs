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
    /// An assessment link also requires assessment_id and assessment_version; an issued_credential link also requires credential_id. Every reference must belong to account_id in this environment. Other kinds reject those fields.
    /// </summary>
    public class ReviewSubjectRequest
    {
        [JsonProperty("kind", Required = Newtonsoft.Json.Required.Always)]
        public ReviewSubjectKind Kind { get; set; } = default!;

        [JsonProperty("account_id", Required = Newtonsoft.Json.Required.Always)]
        public string AccountId { get; set; } = default!;

        [JsonProperty("assessment_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? AssessmentId { get; set; }

        [JsonProperty("assessment_version", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? AssessmentVersion { get; set; }

        [JsonProperty("credential_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? CredentialId { get; set; }

        [JsonProperty("reason", Required = Newtonsoft.Json.Required.Always)]
        public string Reason { get; set; } = default!;

        [JsonProperty("expected_version", Required = Newtonsoft.Json.Required.Always)]
        public long ExpectedVersion { get; set; } = default!;
    }
}