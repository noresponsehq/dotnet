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
    /// Search retained assessments created in [from,to), at most 366 days. identifier matches the submitted value of each returned assessment directly. The legacy email filter keeps its account-wide mailbox semantics and cannot be combined with identifier. Other filters combine using AND and describe assessment decisions, separate from current account decisions. Results use newest-created order with an assessment-ID tie break and are live, not a snapshot across pages. Requires history or investigate authority; identifier, identifier_expression and email filtering also require identifier-read permission. Detailed response mode additionally requires detailed permission. No Idempotency-Key. identifier_expression requires scope assessment and matches the complete expression within each returned submission; account scope returns 400 invalid_request. At most 8 raw terms and 3 group levels. Mutually exclusive with identifier and email.
    /// </summary>
    public class AssessmentSearchRequest
    {
        [JsonProperty("from", Required = Newtonsoft.Json.Required.Always)]
        public string From { get; set; } = default!;

        [JsonProperty("to", Required = Newtonsoft.Json.Required.Always)]
        public string To { get; set; } = default!;

        /// <summary>
        /// Your own opaque identifier for the account being checked, scoped to your workspace. Use an id you mint, with no direct identifier (email, phone, name or national id) in it. '|' is accepted so an Auth0 user id such as 'auth0|64abc' passes unchanged; '@', '+', '$' and whitespace are refused by the pattern, as is an exact-shape JWT; a hex digest such as a SHA-256 is allowed, because hashing an internal id is a common pseudonymous scheme. A value such as '07700900123' or 'jane.doe.gmail.com' cannot be told apart from an opaque id. It travels in bodies, and in a URL only as the path segment of GET /v1/accounts/{account_id} and its /assessments. An assessment requested by a provider hook at pre_registration, which has no account yet, carries the server-issued account_id pre: followed by its assessment_id.
        /// </summary>
        [JsonProperty("account_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? AccountId { get; set; }

        /// <summary>
        /// Assessments of every account that one of its retained admitted assessments sent this mailbox as identifiers.email, matched as lists match an email entry: case-insensitive, a +tag removed and, at gmail.com and googlemail.com, the dots of the local part ignored. Only assessments stored since email list matching began carry the mailbox. An address that is not a valid mailbox is refused with 400.
        /// </summary>
        [JsonProperty("email", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Email { get; set; }

        /// <summary>
        /// The event an assessment is made for: account_creation (a user creates an account), login (a user returns to an existing account) or purchase (a payment or a credit grant). The account's risk is reported with the decision for this flow.
        /// </summary>
        [JsonProperty("flow", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Flow? Flow { get; set; }

        /// <summary>
        /// Processing state, separate from evidence coverage. complete: final; nothing is still running, but unsupported, failed or unanswered lanes remain in incomplete_sources. partial: a recommendation exists and evidence is incomplete; assessment_url identifies continuing work when present. pending: no recommendation yet; poll or wait for the webhook. Finished processing does not mean all sources answered.
        /// </summary>
        [JsonProperty("status", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Status? Status { get; set; }

        /// <summary>
        /// The recommended action: allow, step_up or review. Your application applies it.
        /// </summary>
        [JsonProperty("recommendation", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Recommendation? Recommendation { get; set; }

        [JsonProperty("limit", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Limit { get; set; } = 20;

        /// <summary>
        /// Opaque, expiring cursor bound to workspace, environment, operation and all filters, including expression scope, ordered structure and original term values. Keep grouping, operand order, matching modes and values unchanged; an omitted match is equivalent to exact. The page limit may change.
        /// </summary>
        [JsonProperty("cursor", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Cursor { get; set; }

        /// <summary>
        /// standard returns the consolidated risk response; detailed adds merged provider-neutral facts when the key has detailed scope. There is no raw mode.
        /// </summary>
        [JsonProperty("response_mode", Required = Newtonsoft.Json.Required.DisallowNull)]
        public ResponseMode? ResponseMode { get; set; }

        /// <summary>
        /// An assessment's own decision: block when its stored risk.block is true, otherwise its recommendation. It is not the account's decision or held-admission state.
        /// </summary>
        [JsonProperty("decision", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Decision? Decision { get; set; }

        /// <summary>
        /// Which answer of each assessment the filters describe: latest, the latest stored revision, or first, the first answer, as served when the assessment was first answered (POST /v1/assessments/aggregate counts first answers). A cell of an aggregate drills down to POST /v1/assessments/search with the same filters, revision first and the cell's bucket as from and to.
        /// </summary>
        [JsonProperty("revision", Required = Newtonsoft.Json.Required.DisallowNull)]
        public AnswerRevision? Revision { get; set; }

        [JsonProperty("band", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Band? Band { get; set; }

        /// <summary>
        /// A stable reason code. New codes may appear; ignore any you do not know.
        /// </summary>
        [JsonProperty("reason", Required = Newtonsoft.Json.Required.DisallowNull)]
        public ReasonCode? Reason { get; set; }

        /// <summary>
        /// The selected reason's served impact band. Requires reason.
        /// </summary>
        [JsonProperty("impact", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Impact? Impact { get; set; }

        /// <summary>
        /// An evidence lane: accounts (account presence for email and username), phone, breach, domain (the email domain's registration) and ip.
        /// </summary>
        [JsonProperty("lane", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Lane? Lane { get; set; }

        [JsonProperty("country", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Country { get; set; }

        [JsonProperty("asn", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Asn { get; set; }

        /// <summary>
        /// Inclusive lower score bound. Pending answers without a score do not match.
        /// </summary>
        [JsonProperty("score_min", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? ScoreMin { get; set; }

        /// <summary>
        /// Inclusive upper score bound, at least score_min when both are supplied.
        /// </summary>
        [JsonProperty("score_max", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? ScoreMax { get; set; }

        /// <summary>
        /// The list action captured at the first answer. Requires revision first. Older answers without captured list state do not match, including none.
        /// </summary>
        [JsonProperty("list_match", Required = Newtonsoft.Json.Required.DisallowNull)]
        public ListMatch? ListMatch { get; set; }

        /// <summary>
        /// true returns only assessments whose latest stored revision's decision differs from their first answer's decision (see decision); false only the others. An assessment whose first answer or latest revision has no decision yet is not changed. Cannot be combined with revision first.
        /// </summary>
        [JsonProperty("changed", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? Changed { get; set; }

        /// <summary>
        /// One customer-submitted identifier. Defaults to exact comparison of the original submitted value. canonical uses the supported identifier normalisation without an ownership claim: email preserves local-part case, NFC-normalises it and lowercases the domain without removing dots or tags; phone uses international E.164 without extensions or an inferred region; username uses NFC and Unicode case folding within the customer namespace. mailbox is email-only and explicitly selects existing list rules: case-insensitive, remove +tag, and ignore local-part dots at gmail.com and googlemail.com; googlemail.com maps to gmail.com. Username requires namespace customer. Email and phone forbid namespace. Source-discovered identifiers and platform handles are not searched. Unsupported kinds, namespaces, modes and invalid values return 400. Search equality does not establish verified ownership or merge accounts.
        /// </summary>
        [JsonProperty("identifier", Required = Newtonsoft.Json.Required.DisallowNull)]
        public IdentifierSearch? Identifier { get; set; }

        /// <summary>
        /// A bounded AND/OR expression over submitted identifiers. Required scope assessment means the entire expression must match one retained assessment. Scope account permits each term to match a different retained assessment of the same account and is accepted only by account search. Exactly one of term, and or or is allowed at every node. Groups contain 2 to 8 operands, with at most 3 nested group levels and 8 raw term occurrences over the entire expression. A root term is allowed. Duplicate terms with the same kind, match rule, namespace and normalized comparison are idempotent, but count toward the raw limit. Match rules default to exact and preserve the existing identifier contract. Only retained submitted identifiers can satisfy a term. Unknown fields, unsupported scopes and malformed or oversized expressions return 400 invalid_request.
        /// </summary>
        [JsonProperty("identifier_expression", Required = Newtonsoft.Json.Required.DisallowNull)]
        public IdentifierExpression? IdentifierExpression { get; set; }
    }
}