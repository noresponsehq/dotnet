#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>
    /// Retrieve the account decision at one recorded risk version. The answer uses the original assessment and account-specific risk inputs. Earlier versions that were not captured return 404. The assessment version, policy version and account calculation version identify the decision.
    /// </summary>
    public class AccountDecisionSnapshot
    {
        /// <summary>
        /// Body of the 200 answer to GET /v1/accounts/{account_id}: the one current risk of one of the calling workspace's accounts. It is the risk of the account's latest assessment that has a decision (status complete or partial), by created_at and then assessment_id, scored exactly as GET /v1/assessments/{assessment_id} scores that assessment, plus the account's own reason shared_api_key when a key issued to the account was used from many IP addresses or countries (POST /v1/key_usage/batches). Its assessments are the account's history (GET /v1/accounts/{account_id}/assessments).
        /// </summary>
        [JsonProperty("account", Required = Newtonsoft.Json.Required.Always)]
        public Account Account { get; set; } = new();

        /// <summary>
        /// Monotonic revision of this assessment. 1 on the synchronous answer; each material change while the assessment is served (an async continuation settling a source, a change of recommendation, confidence or status) increments it. Expiry does not increment it: an expired assessment is gone, not revised, and the expiry webhook carries the last served version.
        /// </summary>
        [JsonProperty("assessment_version", Required = Newtonsoft.Json.Required.Always)]
        public long AssessmentVersion { get; set; } = default!;

        /// <summary>
        /// Version of the hand-authored heuristic policy that produced the recommendation, for example 'risk-2026-10.7'. A workspace threshold override adds a +t&lt;N&gt; suffix, e.g. 'risk-2026-10.7+t3'.
        /// </summary>
        [JsonProperty("policy_version", Required = Newtonsoft.Json.Required.Always)]
        public string PolicyVersion { get; set; } = default!;

        [JsonProperty("overlay_version", Required = Newtonsoft.Json.Required.Always)]
        public long OverlayVersion { get; } = 1;

        [JsonProperty("changed_at", Required = Newtonsoft.Json.Required.Always)]
        public string ChangedAt { get; set; } = default!;

        /// <summary>
        /// Opaque, server-assigned id of one recorded outcome: 'out_' and a 26-character Crockford base32 ULID. Unique within the workspace.
        /// </summary>
        [JsonProperty("feedback_outcome_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? FeedbackOutcomeId { get; set; }
    }
}