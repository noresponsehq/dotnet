#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>
    /// POST /v1/assessments/query body. account_id travels in the body rather than the URL path so a provider value that turns out to hold personal data is not written to access logs, proxies or browser history. Read-only: no Idempotency-Key. An account with no unexpired assessments answers 200 with empty items.
    /// </summary>
    public class AssessmentQuery
    {
        /// <summary>
        /// Your own opaque identifier for the account being checked, scoped to your tenant. Use an id you mint, with no direct identifier (email, phone, name or national id) in it. '|' is accepted so an Auth0 user id such as 'auth0|64abc' passes unchanged; '@', '+', '$' and whitespace are refused, as is a JWT; a hex digest such as a SHA-256 is allowed. It travels only in request bodies.
        /// </summary>
        [JsonProperty("account_id", Required = Newtonsoft.Json.Required.Always)]
        public string AccountId { get; set; } = default!;

        [JsonProperty("limit", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Limit { get; set; } = 20;

        /// <summary>
        /// next_cursor from the previous page, unchanged. Opaque, MAC'd and bound to the tenant and account_id.
        /// </summary>
        [JsonProperty("cursor", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Cursor { get; set; }

        /// <summary>
        /// standard returns the consolidated risk response; detailed adds merged provider-neutral facts when the key has detailed scope. There is no raw mode.
        /// </summary>
        [JsonProperty("response_mode", Required = Newtonsoft.Json.Required.DisallowNull)]
        public ResponseMode? ResponseMode { get; set; }
    }
}