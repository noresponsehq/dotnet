#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>
    /// An association between an account and an issued key. The response excludes the key and its hash. Retirement removes this association from risk calculations; revoke the actual key through your own service.
    /// </summary>
    public class CredentialAssociation
    {
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        /// <summary>
        /// Your own opaque identifier for the account being checked, scoped to your workspace. Use an id you mint, with no direct identifier (email, phone, name or national id) in it. '|' is accepted so an Auth0 user id such as 'auth0|64abc' passes unchanged; '@', '+', '$' and whitespace are refused by the pattern, as is an exact-shape JWT; a hex digest such as a SHA-256 is allowed, because hashing an internal id is a common pseudonymous scheme. A value such as '07700900123' or 'jane.doe.gmail.com' cannot be told apart from an opaque id. It travels in bodies, and in a URL only as the path segment of GET /v1/accounts/{account_id}, its /assessments and its /linked_accounts. An assessment requested by a provider hook at pre_registration, which has no account yet, carries the server-issued account_id pre: followed by its assessment_id.
        /// </summary>
        [JsonProperty("account_id", Required = Newtonsoft.Json.Required.Always)]
        public string AccountId { get; set; } = default!;

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;

        [JsonProperty("retired_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? RetiredAt { get; set; }

        [JsonProperty("actor", Required = Newtonsoft.Json.Required.Always)]
        public CredentialAssociationActor Actor { get; set; } = new();

        [JsonProperty("retired_by", Required = Newtonsoft.Json.Required.DisallowNull)]
        public CredentialAssociationRetiredBy? RetiredBy { get; set; }
    }
}