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

    public class Review
    {
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        /// <summary>
        /// Your own opaque identifier for the account being checked, scoped to your workspace. Use an id you mint, with no direct identifier (email, phone, name or national id) in it. '|' is accepted so an Auth0 user id such as 'auth0|64abc' passes unchanged; '@', '+', '$' and whitespace are refused by the pattern, as is an exact-shape JWT; a hex digest such as a SHA-256 is allowed, because hashing an internal id is a common pseudonymous scheme. A value such as '07700900123' or 'jane.doe.gmail.com' cannot be told apart from an opaque id. It travels in bodies, and in a URL only as the path segment of GET /v1/accounts/{account_id} and its /assessments. An assessment requested by a provider hook at pre_registration, which has no account yet, carries the server-issued account_id pre: followed by its assessment_id.
        /// </summary>
        [JsonProperty("account_id", Required = Newtonsoft.Json.Required.Always)]
        public string AccountId { get; set; } = default!;

        [JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public ReviewStatus Status { get; set; } = default!;

        [JsonProperty("priority", Required = Newtonsoft.Json.Required.Always)]
        public Priority Priority { get; set; } = default!;

        [JsonProperty("assignee", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Assignee { get; set; }

        [JsonProperty("version", Required = Newtonsoft.Json.Required.Always)]
        public long Version { get; set; } = default!;

        [JsonProperty("opened_risk_version", Required = Newtonsoft.Json.Required.Always)]
        public long OpenedRiskVersion { get; set; } = default!;

        [JsonProperty("current_risk_version", Required = Newtonsoft.Json.Required.Always)]
        public long CurrentRiskVersion { get; set; } = default!;

        [JsonProperty("created_by", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedBy { get; set; } = default!;

        [JsonProperty("updated_by", Required = Newtonsoft.Json.Required.Always)]
        public string UpdatedBy { get; set; } = default!;

        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;

        [JsonProperty("updated_at", Required = Newtonsoft.Json.Required.Always)]
        public string UpdatedAt { get; set; } = default!;

        [JsonProperty("expires_at", Required = Newtonsoft.Json.Required.Always)]
        public string ExpiresAt { get; set; } = default!;

        [JsonProperty("resolution", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Resolution? Resolution { get; set; }

        /// <summary>
        /// Immutable workspace member ID. assignee is the historical display identity captured at assignment.
        /// </summary>
        [JsonProperty("assignee_principal_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? AssigneePrincipalId { get; set; }
    }
}