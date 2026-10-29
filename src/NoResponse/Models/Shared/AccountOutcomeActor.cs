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

    public class AccountOutcomeActor
    {
        [JsonProperty("type", Required = Newtonsoft.Json.Required.Always)]
        public AccountOutcomeActorType Type { get; set; } = default!;

        [JsonProperty("key_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? KeyId { get; set; }

        /// <summary>
        /// The user's id at the identity provider the console signs people in with: 1 to 256 printable ASCII characters with no space, compared exactly.
        /// </summary>
        [JsonProperty("external_user_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ExternalUserId { get; set; }

        [JsonProperty("console_credential_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ConsoleCredentialId { get; set; }

        /// <summary>
        /// Opaque, server-assigned member id: mem_ and a 26-character ULID. It stays the same when a removed member is invited back.
        /// </summary>
        [JsonProperty("principal_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? PrincipalId { get; set; }
    }
}