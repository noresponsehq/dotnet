#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Accounts observed in retained assessments, newest seen first, with each account once per page and across pages. Counts and keyset pagination are computed over distinct accounts in one database snapshot per page. Live data can change between pages.
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