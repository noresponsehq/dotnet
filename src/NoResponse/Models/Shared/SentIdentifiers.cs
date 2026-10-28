#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// The identifiers an assessment was sent: each member of the request's identifiers object exactly as received, before the server normalised it (the email domain keeps its case, the name its Unicode form). A member the request did not send is absent. NoResponse keeps them encrypted under the workspace's own key, does not index or log them, and deletes them with the assessment: on DELETE of the assessment or its account, at its expires_at, and with the workspace.
    /// </summary>
    public class SentIdentifiers
    {
        /// <summary>
        /// identifiers.email as the request sent it.
        /// </summary>
        [JsonProperty("email", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Email { get; set; }

        /// <summary>
        /// identifiers.username as the request sent it.
        /// </summary>
        [JsonProperty("username", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Username { get; set; }

        /// <summary>
        /// identifiers.phone as the request sent it.
        /// </summary>
        [JsonProperty("phone", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Phone { get; set; }

        /// <summary>
        /// identifiers.name as the request sent it, before NFC normalisation.
        /// </summary>
        [JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Name { get; set; }

        /// <summary>
        /// identifiers.ip as the request sent it.
        /// </summary>
        [JsonProperty("ip", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Ip { get; set; }
    }
}