#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    public class UserActor
    {
        [JsonProperty("type", Required = Newtonsoft.Json.Required.Always)]
        public string Type { get; } = "user";

        /// <summary>
        /// The user's id at the identity provider the console signs people in with: 1 to 256 printable ASCII characters with no space, compared exactly.
        /// </summary>
        [JsonProperty("external_user_id", Required = Newtonsoft.Json.Required.Always)]
        public string ExternalUserId { get; set; } = default!;

        /// <summary>
        /// The public id of the console credential the user acted through.
        /// </summary>
        [JsonProperty("console_credential_id", Required = Newtonsoft.Json.Required.Always)]
        public string ConsoleCredentialId { get; set; } = default!;

        /// <summary>
        /// Stable workspace principal id, retained across identity-provider subject migration. Absent for older events.
        /// </summary>
        [JsonProperty("principal_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? PrincipalId { get; set; }
    }
}