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
        /// The earlier of the explicit lifetime and any rotation overlap. Rotation preserves the original lifetime.
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

        /// <summary>
        /// The optional display name supplied for this API key. Preserved on rotation.
        /// </summary>
        [JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Name { get; set; }

        /// <summary>
        /// Optional operational purpose, at most 200 printable characters without leading or trailing whitespace. Do not put secrets or personal data here. Preserved on rotation and included in the creation request fingerprint; omitted for older credentials.
        /// </summary>
        [JsonProperty("purpose", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Purpose { get; set; }

        [JsonProperty("kind", Required = Newtonsoft.Json.Required.DisallowNull)]
        public KeyKind? Kind { get; set; }

        /// <summary>
        /// The issuing member's external identity for a personal key. Personal ownership is fixed and the key is revoked when that member loses authority. Service keys belong to the workspace and omit this field.
        /// </summary>
        [JsonProperty("owner_user_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? OwnerUserId { get; set; }

        /// <summary>
        /// Replacement public key identifier, or legacy when the successor predates recorded lineage. This field excludes the secret.
        /// </summary>
        [JsonProperty("rotated_to", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? RotatedTo { get; set; }

        /// <summary>
        /// Latest recorded authenticated request observation, updated at most once per minute. Capture is best effort after the response and includes authenticated requests refused by authorization or rate limits. Absence means unknown and does not prove the key was unused. It is independent of diagnostic retention and resets on the replacement key after rotation. This field retains no raw IP or user-agent.
        /// </summary>
        [JsonProperty("last_used_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? LastUsedAt { get; set; }
    }
}