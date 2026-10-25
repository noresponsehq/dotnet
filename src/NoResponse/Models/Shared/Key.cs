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
    /// One API key of your workspace. A key belongs to exactly one environment. key, the secret credential, is present only in the answer that created the key: NoResponse keeps a peppered digest of it, so store it now. Every other field is safe to show.
    /// </summary>
    public class Key
    {
        /// <summary>
        /// The key's public id, a 26-character ULID.
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
        /// The public start of the credential, nrk_&lt;environment&gt;_&lt;key id&gt;, which identifies the key in logs and secret stores without its secret.
        /// </summary>
        [JsonProperty("prefix", Required = Newtonsoft.Json.Required.Always)]
        public string Prefix { get; set; } = default!;

        /// <summary>
        /// The key's scopes, each at most once.
        /// </summary>
        [JsonProperty("scopes", Required = Newtonsoft.Json.Required.Always)]
        public List<KeyScope> Scopes { get; set; } = default!;

        /// <summary>
        /// active: the key authenticates. expired: a rotation's overlap window has ended (expires_at). revoked: the key was revoked (revoked_at) and answers 401.
        /// </summary>
        [JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public KeyStatus Status { get; set; } = default!;

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;

        /// <summary>
        /// The external user id of the member who created the key through the console, or the prefix (nrk_&lt;environment&gt;_&lt;key id&gt;) of the management key that created it. Absent for a key NoResponse issued or a rotation made.
        /// </summary>
        [JsonProperty("created_by", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? CreatedBy { get; set; }

        /// <summary>
        /// When a rotated key stops authenticating (POST /v1/keys/rotate). Absent for a key with no end.
        /// </summary>
        [JsonProperty("expires_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ExpiresAt { get; set; }

        [JsonProperty("revoked_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? RevokedAt { get; set; }

        /// <summary>
        /// The external user id of the member who revoked the key through the console, or the prefix of the management key that revoked it. Absent when the key is not revoked or NoResponse revoked it.
        /// </summary>
        [JsonProperty("revoked_by", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? RevokedBy { get; set; }

        /// <summary>
        /// The credential, nrk_&lt;environment&gt;_&lt;key id&gt;_&lt;secret&gt;. Present only in the 201 answer that created the key.
        /// </summary>
        [JsonProperty("key", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? KeyValue { get; set; }
    }
}