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
    /// A tenant-local investigation link. risk_version is the account risk version captured when the link was added. It does not assert a relationship or change feedback or enforcement.
    /// </summary>
    public class ReviewSubject
    {
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

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

        [JsonProperty("risk_version", Required = Newtonsoft.Json.Required.Always)]
        public long RiskVersion { get; set; } = default!;

        [JsonProperty("reason", Required = Newtonsoft.Json.Required.Always)]
        public string Reason { get; set; } = default!;

        [JsonProperty("created_by", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedBy { get; set; } = default!;

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;
    }
}