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
    /// One group's count in one bucket. It holds each grouped dimension the counted assessments had a value for: a pending first answer has no decision, recommendation or band; an answer that fired no reason, listed no incomplete lane, matched no list entry or whose IP has no known country or ASN leaves that dimension out.
    /// </summary>
    public class AggregateGroup
    {
        /// <summary>
        /// An assessment's own decision: block when its stored risk.block is true, otherwise its recommendation. It is not the account's decision or held-admission state.
        /// </summary>
        [JsonProperty("decision", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Decision? Decision { get; set; }

        /// <summary>
        /// The event an assessment is made for: account_creation (a user creates an account), login (a user returns to an existing account) or purchase (a payment or a credit grant). The account's risk is reported with the decision for this flow.
        /// </summary>
        [JsonProperty("flow", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Flow? Flow { get; set; }

        /// <summary>
        /// The recommended action: allow, step_up or review. Your application applies it.
        /// </summary>
        [JsonProperty("recommendation", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Recommendation? Recommendation { get; set; }

        /// <summary>
        /// low 0-29, medium 30-59, high 60-84, critical 85-100.
        /// </summary>
        [JsonProperty("band", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Band? Band { get; set; }

        /// <summary>
        /// The action of the list entry that decided the account.
        /// </summary>
        [JsonProperty("list_match", Required = Newtonsoft.Json.Required.DisallowNull)]
        public ListMatch? ListMatch { get; set; }

        /// <summary>
        /// A stable reason code. New codes may appear; ignore any you do not know.
        /// </summary>
        [JsonProperty("reason", Required = Newtonsoft.Json.Required.DisallowNull)]
        public ReasonCode? Reason { get; set; }

        [JsonProperty("impact", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Impact? Impact { get; set; }

        /// <summary>
        /// An evidence lane: accounts (account presence for email and username), phone, breach, domain (the email domain's registration) and ip.
        /// </summary>
        [JsonProperty("lane", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Lane? Lane { get; set; }

        [JsonProperty("country", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Country { get; set; }

        [JsonProperty("asn", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Asn { get; set; }

        [JsonProperty("count", Required = Newtonsoft.Json.Required.Always)]
        public long Count { get; set; } = default!;
    }
}