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
    /// One assessment's own facts: its score and recommendation, the signals behind them, the lanes still running and its timing. risk here is this assessment's own score; the account's current risk is the account member of the assessment. risk and recommendation are present for complete and partial; assessment_url is present for pending.
    /// </summary>
    public class AssessmentDetail
    {
        /// <summary>
        /// Version of the body shape.
        /// </summary>
        [JsonProperty("schema_version", Required = Newtonsoft.Json.Required.Always)]
        public string SchemaVersion { get; } = "1";

        /// <summary>
        /// Opaque, server-assigned assessment id: 'asm_' and a 26-character Crockford base32 ULID. Unique within the tenant; another tenant's assessment id answers 404.
        /// </summary>
        [JsonProperty("assessment_id", Required = Newtonsoft.Json.Required.Always)]
        public string AssessmentId { get; set; } = default!;

        /// <summary>
        /// Your own opaque identifier for the account being checked, scoped to your tenant. Use an id you mint, with no direct identifier (email, phone, name or national id) in it. '|' is accepted so an Auth0 user id such as 'auth0|64abc' passes unchanged; '@', '+', '$' and whitespace are refused by the pattern, as is an exact-shape JWT; a hex digest such as a SHA-256 is allowed, because hashing an internal id is a common pseudonymous scheme. A value such as '07700900123' or 'jane.doe.gmail.com' cannot be told apart from an opaque id. It travels in bodies, and in a URL only as the path segment of GET /v1/accounts/{account_id} and its /assessments. An assessment requested by a provider hook at pre_registration, which has no account yet, carries the server-issued account_id pre: followed by its assessment_id.
        /// </summary>
        [JsonProperty("account_id", Required = Newtonsoft.Json.Required.Always)]
        public string AccountId { get; set; } = default!;

        /// <summary>
        /// An optional provider correlation id, echoed back. It is not an idempotency key. Refuses an exact-shape JWT or a bare 40, 64 or 128 character hex digest; a UUID is accepted.
        /// </summary>
        [JsonProperty("request_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? RequestId { get; set; }

        /// <summary>
        /// complete: final; nothing is still running. A lane that failed or missed its 10 s deadline stays listed in incomplete_sources, and confidence is lower. partial: a recommendation exists and at least one lane is still running or did not settle (see incomplete_sources). pending: nothing usable before the deadline; no recommendation yet, poll or wait for the webhook.
        /// </summary>
        [JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public Status Status { get; set; } = default!;

        /// <summary>
        /// The recommended action: allow, step_up or review. Your application applies it.
        /// </summary>
        [JsonProperty("recommendation", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Recommendation? Recommendation { get; set; }

        /// <summary>
        /// Ordinal confidence in the recommendation under the current policy: low, medium, high or unavailable.
        /// </summary>
        [JsonProperty("confidence", Required = Newtonsoft.Json.Required.Always)]
        public Confidence Confidence { get; set; } = default!;

        /// <summary>
        /// The one risk decision for this event. score is 0 (safe) to 100 (risky): the sum of the weights of the reasons that fired, clamped to 0 to 100, from hand-set versioned weights. Every answer with a recommendation carries it, and the top-level recommendation equals risk.recommendation. It names no source or provider.
        /// </summary>
        [JsonProperty("risk", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Risk? Risk { get; set; }

        /// <summary>
        /// Detailed view only (response_mode=detailed, which needs the detailed scope): a standard answer omits signals[], and a detailed one always carries it, possibly empty. The body does not name its mode, so that rule is the serving gate's; the schema states the half it can: an answer with risk.details is a detailed view and carries signals[]. At most one signal per (type, source_id, identifier).
        /// </summary>
        [JsonProperty("signals", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<Signal>? Signals { get; set; }

        /// <summary>
        /// The lanes that did not settle, each listed once with a reason; lanes, not sources. In a detailed view a lane is listed exactly when one of its signals did not settle; a standard view carries no signals to match.
        /// </summary>
        [JsonProperty("incomplete_sources", Required = Newtonsoft.Json.Required.Always)]
        public List<IncompleteSource> IncompleteSources { get; set; } = default!;

        /// <summary>
        /// Version of the hand-authored heuristic weights that produced the recommendation, for example 'risk-2026-10.7'. A tenant threshold override adds a +t&lt;N&gt; suffix, e.g. 'risk-2026-10.7+t3'.
        /// </summary>
        [JsonProperty("policy_version", Required = Newtonsoft.Json.Required.Always)]
        public string PolicyVersion { get; set; } = default!;

        /// <summary>
        /// Labels the v0 policy as hand-authored and not statistically validated.
        /// </summary>
        [JsonProperty("policy_basis", Required = Newtonsoft.Json.Required.Always)]
        public string PolicyBasis { get; } = "heuristic_unvalidated";

        /// <summary>
        /// Monotonic revision of this assessment. 1 on the synchronous answer; each material change while the assessment is served (an async continuation settling a source, a change of recommendation, confidence or status) increments it. Expiry does not increment it: an expired assessment is gone, not revised, and the expiry webhook carries the last served version.
        /// </summary>
        [JsonProperty("assessment_version", Required = Newtonsoft.Json.Required.Always)]
        public long AssessmentVersion { get; set; } = default!;

        /// <summary>
        /// When the assessment was created. Fixed for the life of the assessment: every revision carries the same created_at.
        /// </summary>
        [JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public string CreatedAt { get; set; } = default!;

        [JsonProperty("updated_at", Required = Newtonsoft.Json.Required.Always)]
        public string UpdatedAt { get; set; } = default!;

        /// <summary>
        /// After this instant the assessment is no longer served: GET /v1/assessments/{assessment_id} answers 404 and it leaves the account history. It is created_at plus your retention period at creation (default and maximum 365 days) and is fixed per record: a later revision keeps it, and a new assessment for the same account starts its own clock. If you later shorten retention, the effective expiry becomes created_at plus the shorter of the two periods.
        /// </summary>
        [JsonProperty("expires_at", Required = Newtonsoft.Json.Required.Always)]
        public string ExpiresAt { get; set; } = default!;

        /// <summary>
        /// Server-relative path of the authoritative assessment, GET /v1/assessments/{assessment_id}.
        /// </summary>
        [JsonProperty("assessment_url", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? AssessmentUrl { get; set; }

        [JsonProperty("timing", Required = Newtonsoft.Json.Required.Always)]
        public AssessmentDetailTiming Timing { get; set; } = new();

        /// <summary>
        /// The identity fields of the auth context the assessment was made with, echoed for the tenant's own correlation: the request's auth_context, or the one a hosted provider hook (POST /hooks/{integration_id}) built, with transport provider_hook and its hook_point. Present exactly when the request carried an auth_context, and the same on every revision of the assessment. Values are as the request or the hook sent them; tokens, timestamps, roles and scopes are not echoed, and the echo does not change the score.
        /// </summary>
        [JsonProperty("auth_context", Required = Newtonsoft.Json.Required.DisallowNull)]
        public AssessmentDetailAuthContext? AuthContext { get; set; }
    }
}