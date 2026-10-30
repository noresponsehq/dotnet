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
    /// One stored assessment revision, exactly as GET /v1/assessments/{assessment_id}/versions/{version} returns it, with the decision of the assessment's first answer. It is not re-scored and carries no current account risk.
    /// </summary>
    public class AssessmentSearchItem
    {
        /// <summary>
        /// Version of the body shape.
        /// </summary>
        [JsonProperty("schema_version", Required = Newtonsoft.Json.Required.Always)]
        public string SchemaVersion { get; } = "1";

        /// <summary>
        /// This assessment's id, equal to detail.assessment_id.
        /// </summary>
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        /// <summary>
        /// Processing state, separate from evidence coverage. complete: final; nothing is still running, but unsupported, failed or unanswered lanes remain in incomplete_sources. partial: a recommendation exists and evidence is incomplete; assessment_url identifies continuing work when present. pending: no recommendation yet; poll or wait for the webhook. Finished processing does not mean all sources answered.
        /// </summary>
        [JsonProperty("status", Required = Newtonsoft.Json.Required.Always)]
        public Status Status { get; set; } = default!;

        /// <summary>
        /// The event an assessment is made for: account_creation (a user creates an account), login (a user returns to an existing account) or purchase (a payment or a credit grant). The account's risk is reported with the decision for this flow.
        /// </summary>
        [JsonProperty("flow", Required = Newtonsoft.Json.Required.Always)]
        public Flow Flow { get; set; } = default!;

        /// <summary>
        /// How long the synchronous answer waits for sources. quick waits only for the fast sources, those that answer within 500 ms of ingress (or deadline_ms when lower); full waits for the whole budget, up to 3000 ms. Sources still running at the cut continue asynchronously and update the assessment either way.
        /// </summary>
        [JsonProperty("depth", Required = Newtonsoft.Json.Required.Always)]
        public Depth Depth { get; set; } = default!;

        /// <summary>
        /// One assessment's own facts: its score and recommendation, the signals behind them, the lanes still running and its timing. risk here is this assessment's own score; the account's current risk is the account member of the assessment. risk and recommendation are present for complete and partial; assessment_url is present for pending.
        /// </summary>
        [JsonProperty("detail", Required = Newtonsoft.Json.Required.Always)]
        public AssessmentDetail Detail { get; set; } = new();

        /// <summary>
        /// An assessment's own decision: block when its stored risk.block is true, otherwise its recommendation. It is not the account's decision or held-admission state.
        /// </summary>
        [JsonProperty("first_decision", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Decision? FirstDecision { get; set; }

        /// <summary>
        /// Why this result matched a typed identifier search. value is the original value submitted in assessment_id. Provenance records a customer submission, not a source discovery or verified ownership. Equality is deterministic and has no inferred confidence score. An account result names its newest retained matching assessment, which can differ from latest.
        /// </summary>
        [JsonProperty("matched_identifier", Required = Newtonsoft.Json.Required.DisallowNull)]
        public IdentifierMatch? MatchedIdentifier { get; set; }

        /// <summary>
        /// Present only for identifier_expression. All satisfied unique terms, ordered by term_index, with original submitted values and retained assessment witnesses. A duplicate normalized term appears once at its first raw leaf index. The legacy single identifier filter instead returns matched_identifier.
        /// </summary>
        [JsonProperty("matched_identifiers", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<IdentifierTermMatch>? MatchedIdentifiers { get; set; }
    }
}