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
    /// A hosted provider integration. Point the provider at url: a WorkOS user registration or authentication Action, or the NoResponse Auth0 Action, which sends secret as a bearer token. The answer allows or denies the event: deny only when the assessment's risk.block is true, and allow on every failure, so a fault on the NoResponse side lets the user through. secret is present only in the answers of POST /v1/integrations and POST /v1/integrations/{integration_id}/rotate for an Auth0 integration, and is not returned again.
    /// </summary>
    public class Integration
    {
        /// <summary>
        /// Opaque, server-assigned integration id: int_ and a 26-character Crockford base32 ULID. Another tenant's integration id answers 404.
        /// </summary>
        [JsonProperty("integration_id", Required = Newtonsoft.Json.Required.Always)]
        public string IntegrationId { get; set; } = default!;

        /// <summary>
        /// The authentication provider that calls the endpoint.
        /// </summary>
        [JsonProperty("platform", Required = Newtonsoft.Json.Required.Always)]
        public IntegrationPlatform Platform { get; set; } = default!;

        /// <summary>
        /// Each hook point at most once.
        /// </summary>
        [JsonProperty("hook_points", Required = Newtonsoft.Json.Required.Always)]
        public List<IntegrationHookPoint> HookPoints { get; set; } = default!;

        /// <summary>
        /// The hosted endpoint, https://api.noresponse.org/hooks/{integration_id} in production.
        /// </summary>
        [JsonProperty("url", Required = Newtonsoft.Json.Required.Always)]
        public string Url { get; set; } = default!;

        /// <summary>
        /// Auth0 only, and only when it is created or rotated: the shared secret the Auth0 Action sends as Authorization: Bearer &lt;secret&gt;. nri_ and 43 unpadded base64url characters (32 random bytes). Store it as an Auth0 Action secret.
        /// </summary>
        [JsonProperty("secret", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Secret { get; set; }

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;

        /// <summary>
        /// When the secret was last rotated; absent until the first rotation.
        /// </summary>
        [JsonProperty("rotated_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? RotatedAt { get; set; }

        /// <summary>
        /// After a rotation, when the replaced secret stops being accepted; absent when no replaced secret is still accepted.
        /// </summary>
        [JsonProperty("previous_secret_expires_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? PreviousSecretExpiresAt { get; set; }
    }
}