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
    /// One breach table that matched, detailed view only. name is the catalogue's display name for the table's site, not the upstream provider. No member carries a record, value, password or hash.
    /// </summary>
    public class Breach
    {
        [JsonProperty("name", Required = Newtonsoft.Json.Required.Always)]
        public string Name { get; set; } = default!;

        /// <summary>
        /// The breached site's domain, lower case. Absent when the table has none (a credential list, for example).
        /// </summary>
        [JsonProperty("domain", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Domain { get; set; }

        /// <summary>
        /// The table's category from its name, lower case (social, gaming, combolist, ...).
        /// </summary>
        [JsonProperty("category", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Category { get; set; }

        /// <summary>
        /// Who is in the table: site_breach, a site's own users; credential_list, a combolist or compilation; stealer_log, malware or botnet victims; scrape; hacking_forum, a criminal hacking, cracking or carding community; abuse_service, the customers of DDoS-for-hire, account generators, dump or card shops, darknet markets, stalkerware or breach-lookup services; darkweb_site, an onion site of unknown purpose.
        /// </summary>
        [JsonProperty("kind", Required = Newtonsoft.Json.Required.Always)]
        public BreachKind Kind { get; set; } = default!;

        /// <summary>
        /// A breach table's date year.
        /// </summary>
        [JsonProperty("year", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Year { get; set; }

        /// <summary>
        /// Absent when only the year is known.
        /// </summary>
        [JsonProperty("month", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Month { get; set; }

        /// <summary>
        /// The submitted email or username whose own search matched the table. A submitted full name is searched as well, and what it finds feeds the identity's reasons rather than this list.
        /// </summary>
        [JsonProperty("matched_on", Required = Newtonsoft.Json.Required.Always)]
        public BreachSelector MatchedOn { get; set; } = default!;

        [JsonProperty("exposed", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<BreachExposed>? Exposed { get; set; }
    }
}