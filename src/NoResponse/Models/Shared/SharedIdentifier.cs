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

    public class SharedIdentifier
    {
        [JsonProperty("kind", Required = Newtonsoft.Json.Required.Always)]
        public IdentifierSearchKind Kind { get; set; } = default!;

        /// <summary>
        /// exact compares the original submitted value; canonical uses identifier-specific normalisation; mailbox explicitly selects the email alias rules used by lists.
        /// </summary>
        [JsonProperty("match_basis", Required = Newtonsoft.Json.Required.Always)]
        public Match MatchBasis { get; set; } = default!;

        /// <summary>
        /// Customer-submitted input. It does not mean the customer or NoResponse verified ownership.
        /// </summary>
        [JsonProperty("provenance", Required = Newtonsoft.Json.Required.Always)]
        public Provenance Provenance { get; set; } = default!;

        /// <summary>
        /// Required for username: the username submitted by this customer in this workspace/environment. It does not name or join a source platform. Omit for email and phone.
        /// </summary>
        [JsonProperty("namespace", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Namespace? Namespace { get; set; }

        [JsonProperty("account", Required = Newtonsoft.Json.Required.Always)]
        public IdentifierSubmission Account { get; set; } = new();

        [JsonProperty("linked_account", Required = Newtonsoft.Json.Required.Always)]
        public IdentifierSubmission LinkedAccount { get; set; } = new();
    }
}