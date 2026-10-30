#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Other accounts in this workspace and environment whose retained assessments submitted an email, phone or customer-namespace username that one of account_id's retained assessments also submitted. Matching uses the identifier search rules: exact, canonical and, for email, mailbox. Only customer-submitted identifiers link accounts; source-discovered identifiers do not. Expired, retention-shortened, deleted and erased assessments link nothing. A shared identifier is not verified ownership. Accounts use stable opaque order, the page and total share one database snapshot, and later pages are live. Cursors bind the workspace, environment, operation and account_id, and expire.
    /// </summary>
    public class LinkedAccountPage
    {
        /// <summary>
        /// Version of the body shape.
        /// </summary>
        [JsonProperty("schema_version", Required = Newtonsoft.Json.Required.Always)]
        public string SchemaVersion { get; } = "1";

        /// <summary>
        /// The account whose submitted identifiers were compared. It is excluded from items.
        /// </summary>
        [JsonProperty("account_id", Required = Newtonsoft.Json.Required.Always)]
        public string AccountId { get; set; } = default!;

        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<LinkedAccount> Items { get; set; } = default!;

        /// <summary>
        /// Distinct linked accounts, exact below 10000 and capped at 10000.
        /// </summary>
        [JsonProperty("total", Required = Newtonsoft.Json.Required.Always)]
        public long Total { get; set; } = default!;

        /// <summary>
        /// True when at least 10000 accounts are linked.
        /// </summary>
        [JsonProperty("total_capped", Required = Newtonsoft.Json.Required.Always)]
        public bool TotalCapped { get; set; } = default!;

        /// <summary>
        /// Opaque, expiring cursor bound to this environment, operation and account_id. Absent on the last page.
        /// </summary>
        [JsonProperty("next_cursor", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? NextCursor { get; set; }
    }
}