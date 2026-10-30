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
    /// One satisfied unique term in an identifier expression. term_index is the zero-based depth-first raw leaf position of its first occurrence. Duplicate normalized terms produce one witness. All satisfied unique terms are returned, ordered by term_index, including satisfied terms in an OR branch that was not needed. value preserves the original submitted value in assessment_id. In assessment scope all witnesses belong to the same matching assessment; account search chooses the newest such assessment. In account scope each term uses its newest retained matching assessment, so witnesses may have different assessment IDs. This evidence does not assert ownership or equality between accounts.
    /// </summary>
    public class IdentifierTermMatch
    {
        [JsonProperty("kind", Required = Newtonsoft.Json.Required.Always)]
        public IdentifierSearchKind Kind { get; set; } = default!;

        [JsonProperty("value", Required = Newtonsoft.Json.Required.Always)]
        public string Value { get; set; } = default!;

        /// <summary>
        /// exact compares the original submitted value; canonical uses identifier-specific normalisation; mailbox explicitly selects the email alias rules used by lists.
        /// </summary>
        [JsonProperty("match_basis", Required = Newtonsoft.Json.Required.Always)]
        public Match MatchBasis { get; set; } = default!;

        /// <summary>
        /// Customer-submitted input. It does not mean the customer or NoResponse verified ownership.
        /// </summary>
        [JsonProperty("provenance", Required = Newtonsoft.Json.Required.Always)]
        public Provenance Provenance { get; set; } = default!;

        /// <summary>
        /// Required for username: the username submitted by this customer in this workspace/environment. It does not name or join a source platform. Omit for email and phone.
        /// </summary>
        [JsonProperty("namespace", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Namespace? Namespace { get; set; }

        /// <summary>
        /// Opaque, server-assigned assessment id: 'asm_' and a 26-character Crockford base32 ULID. Unique within the workspace; another workspace's assessment id answers 404.
        /// </summary>
        [JsonProperty("assessment_id", Required = Newtonsoft.Json.Required.Always)]
        public string AssessmentId { get; set; } = default!;

        /// <summary>
        /// Zero-based depth-first position of the first occurrence of this normalized term in the submitted expression.
        /// </summary>
        [JsonProperty("term_index", Required = Newtonsoft.Json.Required.Always)]
        public long TermIndex { get; set; } = default!;
    }
}