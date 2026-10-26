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

    public class RiskDetailsEvent
    {
        [JsonProperty("year", Required = Newtonsoft.Json.Required.Always)]
        public long Year { get; set; } = default!;

        [JsonProperty("month", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Month { get; set; }

        [JsonProperty("kind", Required = Newtonsoft.Json.Required.Always)]
        public RiskDetailsEventKind Kind { get; set; } = default!;

        [JsonProperty("site", Required = Newtonsoft.Json.Required.Always)]
        public string Site { get; set; } = default!;

        [JsonProperty("category", Required = Newtonsoft.Json.Required.Always)]
        public string Category { get; set; } = default!;

        /// <summary>
        /// The source-neutral kind of evidence: account_presence (an account found on a site), auth_provider (the account at your auth provider), breach (breach and credential tables), breach_abuse (abuse and fraud communities' tables) and code_hosting (a code hosting profile).
        /// </summary>
        [JsonProperty("family", Required = Newtonsoft.Json.Required.Always)]
        public FootprintFamily Family { get; set; } = default!;

        [JsonProperty("identifier", Required = Newtonsoft.Json.Required.Always)]
        public Identifier Identifier { get; set; } = default!;
    }
}