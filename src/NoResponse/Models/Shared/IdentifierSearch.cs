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
    /// One customer-submitted identifier. Defaults to exact comparison of the original submitted value. canonical uses the supported identifier normalisation without an ownership claim: email preserves local-part case, NFC-normalises it and lowercases the domain without removing dots or tags; phone uses international E.164 without extensions or an inferred region; username uses NFC and Unicode case folding within the customer namespace. mailbox is email-only and explicitly selects existing list rules: case-insensitive, remove +tag, and ignore local-part dots at gmail.com and googlemail.com; googlemail.com maps to gmail.com. Username requires namespace customer. Email and phone forbid namespace. Source-discovered identifiers and platform handles are not searched. Unsupported kinds, namespaces, modes and invalid values return 400. Search equality does not establish verified ownership or merge accounts.
    /// </summary>
    public class IdentifierSearch
    {
        [JsonProperty("kind", Required = Newtonsoft.Json.Required.Always)]
        public IdentifierSearchKind Kind { get; set; } = default!;

        /// <summary>
        /// The comparison value. Stored original identifiers remain unchanged. An invalid identifier is refused instead of returning an empty result.
        /// </summary>
        [JsonProperty("value", Required = Newtonsoft.Json.Required.Always)]
        public string Value { get; set; } = default!;

        /// <summary>
        /// exact compares the original submitted value; canonical uses identifier-specific normalisation; mailbox explicitly selects the email alias rules used by lists.
        /// </summary>
        [JsonProperty("match", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Match? Match { get; set; }

        /// <summary>
        /// Required for username: the username submitted by this customer in this workspace/environment. It does not name or join a source platform. Omit for email and phone.
        /// </summary>
        [JsonProperty("namespace", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Namespace? Namespace { get; set; }
    }
}