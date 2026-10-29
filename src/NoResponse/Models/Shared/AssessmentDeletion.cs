#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>
    /// Body of the 200 answer to DELETE /v1/assessments/{assessment_id}: one of the calling workspace's assessments was deleted, with its versions, evidence, outcomes and the identity-graph marks its outcomes set. Afterwards GET /v1/assessments/{assessment_id} answers 404 and the assessment leaves the account's history. The account is scored again from what remains: account.current_assessment_id names the assessment GET /v1/accounts/{account_id} now serves its risk from, and is absent when the account has no assessment with a decision left. Deleting an assessment that is already gone answers 404.
    /// </summary>
    public class AssessmentDeletion
    {
        /// <summary>
        /// Version of the body shape.
        /// </summary>
        [JsonProperty("schema_version", Required = Newtonsoft.Json.Required.Always)]
        public string SchemaVersion { get; } = "1";

        /// <summary>
        /// The assessment that was deleted.
        /// </summary>
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        [JsonProperty("deleted", Required = Newtonsoft.Json.Required.Always)]
        public bool Deleted { get; } = true;

        [JsonProperty("account", Required = Newtonsoft.Json.Required.Always)]
        public AssessmentDeletionAccount Account { get; set; } = new();

        /// <summary>
        /// When the deletion took effect.
        /// </summary>
        [JsonProperty("deleted_at", Required = Newtonsoft.Json.Required.Always)]
        public string DeletedAt { get; set; } = default!;

        /// <summary>
        /// Receipt for GET /v1/erasures/{erasure_id}, when durable storage issued one.
        /// </summary>
        [JsonProperty("erasure_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? ErasureId { get; set; }
    }
}