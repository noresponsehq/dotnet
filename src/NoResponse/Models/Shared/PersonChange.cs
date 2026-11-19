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

    public class PersonChange
    {
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        /// <summary>
        /// account_added and account_removed: an account or profile appeared or disappeared. breach_added and breach_removed: a breach record appeared or disappeared. identifier_added and identifier_removed: an email, phone, username, name or account id on an account the person already held. field_changed: another field on such an account. finding_added: a new finding. posts_added: new posts, counted in count.
        /// </summary>
        [JsonProperty("kind", Required = Newtonsoft.Json.Required.Always)]
        public PersonChangeKind Kind { get; set; } = default!;

        /// <summary>
        /// One rule ranks every change: identity changes (accounts, breaches, identifiers) are high, profile fields and findings are medium, new posts are low.
        /// </summary>
        [JsonProperty("significance", Required = Newtonsoft.Json.Required.Always)]
        public Significance Significance { get; set; } = default!;

        /// <summary>
        /// The connector, site or platform the change came from.
        /// </summary>
        [JsonProperty("source", Required = Newtonsoft.Json.Required.Always)]
        public string Source { get; set; } = default!;

        [JsonProperty("field", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Field { get; set; }

        [JsonProperty("before", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Before { get; set; }

        [JsonProperty("after", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? After { get; set; }

        [JsonProperty("count", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Count { get; set; }

        /// <summary>
        /// The investigation that found the change.
        /// </summary>
        [JsonProperty("investigation_id", Required = Newtonsoft.Json.Required.Always)]
        public string InvestigationId { get; set; } = default!;

        /// <summary>
        /// The earlier investigation it was compared with.
        /// </summary>
        [JsonProperty("previous_investigation_id", Required = Newtonsoft.Json.Required.Always)]
        public string PreviousInvestigationId { get; set; } = default!;

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;

        [JsonProperty("reviewed_at", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public string? ReviewedAt { get; set; }

        [JsonProperty("reviewed_by", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ReviewedBy { get; set; }
    }
}