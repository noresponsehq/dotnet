#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>
    /// Body of the 200 answer to GET /v1/accounts/{account_id}: the one current risk of one of the calling workspace's accounts. It is the risk of the account's latest assessment that has a decision (status complete or partial), by created_at and then assessment_id, scored exactly as GET /v1/assessments/{assessment_id} scores that assessment, plus the account's own reason shared_api_key when a key issued to the account was used from many IP addresses or countries (POST /v1/key_usage/batches). Its assessments are the account's history (GET /v1/accounts/{account_id}/assessments).
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Version of the body shape.
        /// </summary>
        [JsonProperty("schema_version", Required = Newtonsoft.Json.Required.Always)]
        public string SchemaVersion { get; } = "1";

        /// <summary>
        /// The account_id the workspace sends with its assessments.
        /// </summary>
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        [JsonProperty("risk", Required = Newtonsoft.Json.Required.Always)]
        public AccountRisk Risk { get; set; } = new();

        /// <summary>
        /// The decision for each flow: the account's score under that flow's thresholds. A flow the workspace has not set uses its account_creation thresholds, so the flows differ only where it set them.
        /// </summary>
        [JsonProperty("decisions_by_flow", Required = Newtonsoft.Json.Required.Always)]
        public AccountDecisionsByFlow DecisionsByFlow { get; set; } = new();

        /// <summary>
        /// The assessment the current risk comes from.
        /// </summary>
        [JsonProperty("assessment_id", Required = Newtonsoft.Json.Required.Always)]
        public string AssessmentId { get; set; } = default!;

        /// <summary>
        /// updated_at of that assessment's latest revision.
        /// </summary>
        [JsonProperty("updated_at", Required = Newtonsoft.Json.Required.Always)]
        public string UpdatedAt { get; set; } = default!;
    }
}