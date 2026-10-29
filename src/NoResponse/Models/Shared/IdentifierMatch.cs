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
    /// Why this result matched a typed identifier search. value is the original value submitted in assessment_id. Provenance records a customer submission, not a source discovery or verified ownership. Equality is deterministic and has no inferred confidence score. An account result names its newest retained matching assessment, which can differ from latest.
    /// </summary>
    public class IdentifierMatch
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
    }
}