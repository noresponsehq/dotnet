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
    /// Creates a key whose scopes are bounded by the issuer capabilities in this environment. Delete is owner-only. A service key outlives its creator; a personal key belongs to the authenticated member and is revoked on membership or authority changes. request_id deduplicates creation: retry returns 200 metadata without the secret; changed parameters answer 409. Team scopes members and members_manage are explicitly workspace-wide, live-only, and delegated by a current full owner. Personal credentials cannot hold management or members_manage. Legacy management includes policy publication; use settings without policy_publish for configuration preparation and retention-only edits.
    /// </summary>
    public class KeyRequest
    {
        /// <summary>
        /// The environment the key belongs to. A live key assesses with live sources; a test key answers from the deterministic simulator.
        /// </summary>
        [JsonProperty("environment", Required = Newtonsoft.Json.Required.Always)]
        public KeyEnvironment Environment { get; set; } = default!;

        /// <summary>
        /// The key's scopes, each at most once.
        /// </summary>
        [JsonProperty("scopes", Required = Newtonsoft.Json.Required.Always)]
        public List<KeyRequestScope> Scopes { get; set; } = default!;

        /// <summary>
        /// Optional display name for this API key, at most 100 printable characters. Preserved on rotation.
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
        /// Between one minute and 365 days from now.
        /// </summary>
        [JsonProperty("expires_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ExpiresAt { get; set; }

        /// <summary>
        /// Caller-chosen identifier for one creation attempt, scoped to the issuing actor and environment. Use 1 to 256 printable ASCII characters without spaces. A retry with the same parameters returns the original key metadata with status 200 and no secret; changed parameters return 409.
        /// </summary>
        [JsonProperty("request_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? RequestId { get; set; }
    }
}