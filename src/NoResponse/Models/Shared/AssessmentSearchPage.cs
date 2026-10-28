#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// A bounded page of latest assessment revisions, newest creation first. Each revision identifies the exact result found; no current account risk is attached.
    /// </summary>
    public class AssessmentSearchPage
    {
        /// <summary>
        /// Version of the body shape.
        /// </summary>
        [JsonProperty("schema_version", Required = Newtonsoft.Json.Required.Always)]
        public string SchemaVersion { get; } = "1";

        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<AssessmentRevision> Items { get; set; } = default!;

        /// <summary>
        /// Opaque, expiring cursor bound to this environment, operation and every filter. Keep the filters unchanged.
        /// </summary>
        [JsonProperty("next_cursor", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? NextCursor { get; set; }

        /// <summary>
        /// The calling workspace's configured retention period in days. Default 365 (12 months), which is also the maximum: a workspace may only shorten it, to at least 1 day. Each record expires from its own created_at; see assessment expires_at.
        /// </summary>
        [JsonProperty("retention_days", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? RetentionDays { get; set; } = 365;
    }
}