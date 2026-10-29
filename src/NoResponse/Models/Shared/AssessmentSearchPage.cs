#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// A bounded page of latest assessment revisions, newest creation first, with how many assessments match the filters. Each item is the revision found and its first answer's decision; no current account risk is attached.
    /// </summary>
    public class AssessmentSearchPage
    {
        /// <summary>
        /// Version of the body shape.
        /// </summary>
        [JsonProperty("schema_version", Required = Newtonsoft.Json.Required.Always)]
        public string SchemaVersion { get; } = "1";

        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<AssessmentSearchItem> Items { get; set; } = default!;

        /// <summary>
        /// How many assessments match the filters over every page, counted when this page was read: exact up to 10000. At 10000 or more it is 10000 and total_capped is true. The cursor does not change it; a later page can count differently, as results are live.
        /// </summary>
        [JsonProperty("total", Required = Newtonsoft.Json.Required.Always)]
        public long Total { get; set; } = default!;

        /// <summary>
        /// True when at least 10000 assessments match, so total is 10000 and not the exact count.
        /// </summary>
        [JsonProperty("total_capped", Required = Newtonsoft.Json.Required.Always)]
        public bool TotalCapped { get; set; } = default!;

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