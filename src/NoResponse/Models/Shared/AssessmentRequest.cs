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
    /// POST /v1/assessments body. At least one of email, username or phone is required; name and ip only refine an assessment. No field carries a password, hash, token or breach value, and unknown members are refused. The email local part is kept as received and only the domain is lower-cased; the username is compared case-insensitively; the phone is strict E.164 and is assessed asynchronously. The whole body is capped at 32768 bytes. client_context holds bounded hints from the client of the user; auth_context holds normalised claims from a session your backend has verified. Raw tokens are refused.
    /// </summary>
    public class AssessmentRequest
    {
        /// <summary>
        /// Your own opaque identifier for the account being checked, scoped to your workspace. Use an id you mint, with no direct identifier (email, phone, name or national id) in it. '|' is accepted so an Auth0 user id such as 'auth0|64abc' passes unchanged; '@', '+', '$' and whitespace are refused by the pattern, as is an exact-shape JWT; a hex digest such as a SHA-256 is allowed, because hashing an internal id is a common pseudonymous scheme. A value such as '07700900123' or 'jane.doe.gmail.com' cannot be told apart from an opaque id. It travels in bodies, and in a URL only as the path segment of GET /v1/accounts/{account_id} and its /assessments. An assessment requested by a provider hook at pre_registration, which has no account yet, carries the server-issued account_id pre: followed by its assessment_id.
        /// </summary>
        [JsonProperty("account_id", Required = Newtonsoft.Json.Required.Always)]
        public string AccountId { get; set; } = default!;

        /// <summary>
        /// The event an assessment is made for: account_creation (a user creates an account), login (a user returns to an existing account) or purchase (a payment or a credit grant). The account's risk is reported with the decision for this flow.
        /// </summary>
        [JsonProperty("flow", Required = Newtonsoft.Json.Required.Always)]
        public Flow Flow { get; set; } = default!;

        /// <summary>
        /// How long the synchronous answer waits for sources. quick waits only for the fast sources, those that answer within 500 ms of ingress (or deadline_ms when lower); full waits for the whole budget, up to 3000 ms. Sources still running at the cut continue asynchronously and update the assessment either way.
        /// </summary>
        [JsonProperty("depth", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Depth? Depth { get; set; }

        /// <summary>
        /// At least one of email, username or phone. name and ip refine the match and travel beside a lookup identifier; a body with only name or ip is refused with 400.
        /// </summary>
        [JsonProperty("identifiers", Required = Newtonsoft.Json.Required.Always)]
        public AssessmentRequestIdentifiers Identifiers { get; set; } = new();

        /// <summary>
        /// Optional policy inputs: region.
        /// </summary>
        [JsonProperty("policy_context", Required = Newtonsoft.Json.Required.DisallowNull)]
        public AssessmentRequestPolicyContext? PolicyContext { get; set; }

        /// <summary>
        /// An optional provider correlation id, echoed back. It is not an idempotency key. Refuses an exact-shape JWT or a bare 40, 64 or 128 character hex digest; a UUID is accepted.
        /// </summary>
        [JsonProperty("request_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? RequestId { get; set; }

        /// <summary>
        /// Optional tighter budget, 50 to 3000 ms, measured from the moment the request reaches NoResponse. Absent means the 3000 ms ceiling; a value above it is refused with 400 rather than clamped.
        /// </summary>
        [JsonProperty("deadline_ms", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? DeadlineMs { get; set; }

        /// <summary>
        /// standard returns the consolidated risk response; detailed adds merged provider-neutral facts when the key has detailed scope. There is no raw mode.
        /// </summary>
        [JsonProperty("response_mode", Required = Newtonsoft.Json.Required.DisallowNull)]
        public ResponseMode? ResponseMode { get; set; }

        /// <summary>
        /// Optional context observed by the client of the user. The server parses these hints into coarse features, leaves raw user_agent and opaque ids out of every answer, and weighs a mismatch with the other evidence.
        /// </summary>
        [JsonProperty("client_context", Required = Newtonsoft.Json.Required.DisallowNull)]
        public AssessmentRequestClientContext? ClientContext { get; set; }

        /// <summary>
        /// Normalised claims from a session your backend has already verified with WorkOS, Clerk, Auth0 or any OIDC provider. Pass the output of the SDK's auth adapters, not a raw token.
        /// </summary>
        [JsonProperty("auth_context", Required = Newtonsoft.Json.Required.DisallowNull)]
        public AuthContext? AuthContext { get; set; }

        /// <summary>
        /// Optional. When your platform issues an API key to the new account, send SHA-256 of that key, so later usage of the key, or a leaked copy, ties back to this assessment. Each SDK has a helper that computes it. Send only the hash: customer keys are high-entropy, so the hash reveals nothing about them. The assessment stores it, and no response, webhook or log carries it.
        /// </summary>
        [JsonProperty("issued_credential", Required = Newtonsoft.Json.Required.DisallowNull)]
        public AssessmentRequestIssuedCredential? IssuedCredential { get; set; }
    }
}