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
    /// One entry of an environment's list: an exact email or an email domain that decides an account whatever its score. An account matches when one of its admitted assessments sent the email, or an email of the domain. A block entry decides block for every flow and an allow entry decides allow; when both match, block wins. The account's risk carries the list_match reason with the entry's id, and its score stays the one the assessment's facts give. A held flow still waits for its investigation.
    /// </summary>
    public class ListEntry
    {
        /// <summary>
        /// The entry's id: lst_ and a ULID.
        /// </summary>
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        /// <summary>
        /// The workspace id, which is also its live environment's workspace id; the test environment's is the same id with :test appended. A workspace created through the API is tnt_ and a 26-character ULID.
        /// </summary>
        [JsonProperty("workspace_id", Required = Newtonsoft.Json.Required.Always)]
        public string WorkspaceId { get; set; } = default!;

        /// <summary>
        /// The environment the key belongs to. A live key assesses with live sources; a test key answers from the deterministic simulator.
        /// </summary>
        [JsonProperty("environment", Required = Newtonsoft.Json.Required.Always)]
        public KeyEnvironment Environment { get; set; } = default!;

        /// <summary>
        /// email matches one address, whatever the case of its local part. email_domain matches every address of the registrable domain and of its subdomains.
        /// </summary>
        [JsonProperty("kind", Required = Newtonsoft.Json.Required.Always)]
        public ListEntryKind Kind { get; set; } = default!;

        /// <summary>
        /// The value the entry matches, in canonical form: an email lower-cased whole, or a domain lower-cased and taken to its registrable domain (mail.example.co.uk is stored as example.co.uk).
        /// </summary>
        [JsonProperty("value", Required = Newtonsoft.Json.Required.Always)]
        public string Value { get; set; } = default!;

        /// <summary>
        /// block decides block for the accounts the entry matches; allow decides allow. Block wins when both match.
        /// </summary>
        [JsonProperty("action", Required = Newtonsoft.Json.Required.Always)]
        public ListEntryAction Action { get; set; } = default!;

        /// <summary>
        /// Why the entry exists, for the people who manage the list. It is shown with the entry and decides nothing.
        /// </summary>
        [JsonProperty("note", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Note { get; set; }

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;

        /// <summary>
        /// The external user id of the member who added the entry through the console, or the prefix (nrk_&lt;environment&gt;_&lt;key id&gt;) of the management key that added it.
        /// </summary>
        [JsonProperty("created_by", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedBy { get; set; } = default!;

        /// <summary>
        /// Optional expiry. An expired entry stops matching immediately; bounded maintenance removes it and propagates account risk changes.
        /// </summary>
        [JsonProperty("expires_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ExpiresAt { get; set; }
    }
}