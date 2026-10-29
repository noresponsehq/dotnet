#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Distinct observed accounts with retained assessments. Typed identifier searches use stable opaque account order, independent of newly submitted assessments; ordinary and legacy email searches use newest-seen order. One repeatable-read database snapshot supplies each page's rows and totals. Pages do not share a snapshot: new matching accounts may appear on a later page or before the cursor and be missed until a fresh search; retention, erasure and changed membership can remove results. Current account decisions are read separately and may advance during a page. Cursors bind the workspace/environment, operation and filters, expire, and cannot cross scopes. Erasure and retention always take precedence over pagination.
    /// </summary>
    public class AccountDirectoryPage
    {
        /// <summary>
        /// Version of the body shape.
        /// </summary>
        [JsonProperty("schema_version", Required = Newtonsoft.Json.Required.Always)]
        public string SchemaVersion { get; } = "1";

        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<AccountDirectoryPageItem> Items { get; set; } = default!;

        /// <summary>
        /// Distinct accounts matching the filters, exact below 10000 and capped at 10000.
        /// </summary>
        [JsonProperty("total", Required = Newtonsoft.Json.Required.Always)]
        public long Total { get; set; } = default!;

        /// <summary>
        /// True when at least 10000 accounts match.
        /// </summary>
        [JsonProperty("total_capped", Required = Newtonsoft.Json.Required.Always)]
        public bool TotalCapped { get; set; } = default!;

        /// <summary>
        /// Opaque, expiring cursor bound to this environment, operation and every filter. Keep the filters unchanged.
        /// </summary>
        [JsonProperty("next_cursor", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? NextCursor { get; set; }
    }
}