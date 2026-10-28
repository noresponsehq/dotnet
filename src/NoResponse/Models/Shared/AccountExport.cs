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
    /// Retrieve one consistent snapshot of retained customer-accessible records, including assessment revisions, account decisions, feedback, key associations, enforcements and request diagnostics. Secrets, restricted source information, other customers' data and legacy free-text notes are excluded. The same standard/detailed permissions apply. An export exceeding 10000 records or 8 MiB returns export_too_large without a partial result. Historical decisions that were not captured are identified as an exclusion.
    /// </summary>
    public class AccountExport
    {
        /// <summary>
        /// Version of the body shape.
        /// </summary>
        [JsonProperty("schema_version", Required = Newtonsoft.Json.Required.Always)]
        public string SchemaVersion { get; } = "1";

        /// <summary>
        /// Your own opaque identifier for the account being checked, scoped to your workspace. Use an id you mint, with no direct identifier (email, phone, name or national id) in it. '|' is accepted so an Auth0 user id such as 'auth0|64abc' passes unchanged; '@', '+', '$' and whitespace are refused by the pattern, as is an exact-shape JWT; a hex digest such as a SHA-256 is allowed, because hashing an internal id is a common pseudonymous scheme. A value such as '07700900123' or 'jane.doe.gmail.com' cannot be told apart from an opaque id. It travels in bodies, and in a URL only as the path segment of GET /v1/accounts/{account_id} and its /assessments. An assessment requested by a provider hook at pre_registration, which has no account yet, carries the server-issued account_id pre: followed by its assessment_id.
        /// </summary>
        [JsonProperty("account_id", Required = Newtonsoft.Json.Required.Always)]
        public string AccountId { get; set; } = default!;

        [JsonProperty("exported_at", Required = Newtonsoft.Json.Required.Always)]
        public string ExportedAt { get; set; } = default!;

        [JsonProperty("complete", Required = Newtonsoft.Json.Required.Always)]
        public bool Complete { get; } = true;

        [JsonProperty("current_risk_version", Required = Newtonsoft.Json.Required.Always)]
        public long CurrentRiskVersion { get; set; } = default!;

        [JsonProperty("assessments", Required = Newtonsoft.Json.Required.Always)]
        public List<AssessmentRevision> Assessments { get; set; } = default!;

        [JsonProperty("decisions", Required = Newtonsoft.Json.Required.Always)]
        public List<AccountDecisionSnapshot> Decisions { get; set; } = default!;

        [JsonProperty("outcomes", Required = Newtonsoft.Json.Required.Always)]
        public List<AccountOutcome> Outcomes { get; set; } = default!;

        [JsonProperty("assessment_outcomes", Required = Newtonsoft.Json.Required.Always)]
        public List<ExportAssessmentOutcome> AssessmentOutcomes { get; set; } = default!;

        [JsonProperty("credentials", Required = Newtonsoft.Json.Required.Always)]
        public List<CredentialAssociation> Credentials { get; set; } = default!;

        [JsonProperty("bindings", Required = Newtonsoft.Json.Required.Always)]
        public List<AccountBinding> Bindings { get; set; } = default!;

        [JsonProperty("enforcements", Required = Newtonsoft.Json.Required.Always)]
        public List<Enforcement> Enforcements { get; set; } = default!;

        [JsonProperty("requests", Required = Newtonsoft.Json.Required.Always)]
        public List<RequestDiagnostic> Requests { get; set; } = default!;

        [JsonProperty("exclusions", Required = Newtonsoft.Json.Required.Always)]
        public List<Exclusion> Exclusions { get; set; } = default!;
    }
}