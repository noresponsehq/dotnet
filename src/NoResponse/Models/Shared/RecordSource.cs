#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// Where a record came from.
    /// </summary>
    public class RecordSource
    {
        /// <summary>
        /// The id of the connector that read the record: breach for a breach row.
        /// </summary>
        [JsonProperty("connector", Required = Newtonsoft.Json.Required.Always)]
        public string Connector { get; set; } = default!;

        /// <summary>
        /// The kind of source: platform for an account read off a live platform; for a breach row, the kind of leak its table is: site_breach, credential_list, stealer_log, scrape, hacking_forum, abuse_service or darkweb_site.
        /// </summary>
        [JsonProperty("kind", Required = Newtonsoft.Json.Required.Always)]
        public string Kind { get; set; } = default!;

        /// <summary>
        /// The host a person knows the source's site by.
        /// </summary>
        [JsonProperty("site", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Site { get; set; }

        /// <summary>
        /// The table a leaked row came from.
        /// </summary>
        [JsonProperty("table", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Table { get; set; }

        /// <summary>
        /// When the source's data dates from, at the precision it is known: YYYY or YYYY-MM for a breach table.
        /// </summary>
        [JsonProperty("date", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Date { get; set; }
    }
}