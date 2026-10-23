#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Body of the 200 answer to POST /v1/assessments/query: one page of the calling tenant's unexpired assessments for one account, newest first, each at its latest assessment_version, as its detail; the account's current risk is GET /v1/accounts/{account_id}. Every item's account_id equals the page's. An assessment past its expires_at is not listed. next_cursor is present only when another page exists.
    /// </summary>
    public class AssessmentList
    {
        /// <summary>
        /// Version of the body shape.
        /// </summary>
        [JsonProperty("schema_version", Required = Newtonsoft.Json.Required.Always)]
        public string SchemaVersion { get; } = "1";

        /// <summary>
        /// Your own opaque identifier for the account being checked, scoped to your tenant. Use an id you mint, with no direct identifier (email, phone, name or national id) in it. '|' is accepted so an Auth0 user id such as 'auth0|64abc' passes unchanged; '@', '+', '$' and whitespace are refused by the pattern, as is an exact-shape JWT; a hex digest such as a SHA-256 is allowed, because hashing an internal id is a common pseudonymous scheme. A value such as '07700900123' or 'jane.doe.gmail.com' cannot be told apart from an opaque id. It travels in bodies, and in a URL only as the path segment of GET /v1/accounts/{account_id} and its /assessments. An assessment requested by a provider hook at pre_registration, which has no account yet, carries the server-issued account_id pre: followed by its assessment_id.
        /// </summary>
        [JsonProperty("account_id", Required = Newtonsoft.Json.Required.Always)]
        public string AccountId { get; set; } = default!;

        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<AssessmentDetail> Items { get; set; } = default!;

        /// <summary>
        /// Opaque cursor for the next page, bound to your tenant and the account_id it was issued for. Pass it back unchanged in the next query; a tampered, foreign or expired cursor answers 400 invalid_cursor.
        /// </summary>
        [JsonProperty("next_cursor", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? NextCursor { get; set; }

        /// <summary>
        /// Your current retention period, so a consumer knows how far back history reaches. Every item expires no later than its created_at plus this many days.
        /// </summary>
        [JsonProperty("retention_days", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? RetentionDays { get; set; } = 365;
    }
}