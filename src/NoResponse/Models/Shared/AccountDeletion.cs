#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    /// <summary>
    /// Body of the 200 answer to DELETE /v1/accounts/{account_id}: the calling workspace's account was erased in the environment of the calling key. Every assessment of the account went, with its revisions, evidence, outcomes and the identity-graph marks its outcomes set, and an assessment that was still running when the call arrived is stopped. Afterwards GET /v1/accounts/{account_id} and GET /v1/assessments/{assessment_id} of each erased assessment answer 404, and the history page is empty. The call is idempotent: a repeat, or an account with nothing to erase, answers 200 with assessments_deleted 0. The account_id can be assessed again later, as a new account.
    /// </summary>
    public class AccountDeletion
    {
        /// <summary>
        /// Version of the body shape.
        /// </summary>
        [JsonProperty("schema_version", Required = Newtonsoft.Json.Required.Always)]
        public string SchemaVersion { get; } = "1";

        /// <summary>
        /// The account_id that was erased.
        /// </summary>
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        [JsonProperty("deleted", Required = Newtonsoft.Json.Required.Always)]
        public bool Deleted { get; } = true;

        /// <summary>
        /// How many assessments this call deleted. 0 when the account had none left, so a repeat answers 0.
        /// </summary>
        [JsonProperty("assessments_deleted", Required = Newtonsoft.Json.Required.Always)]
        public long AssessmentsDeleted { get; set; } = default!;

        /// <summary>
        /// When the erasure took effect. An assessment of the account begun before this time is refused or stopped.
        /// </summary>
        [JsonProperty("deleted_at", Required = Newtonsoft.Json.Required.Always)]
        public string DeletedAt { get; set; } = default!;
    }
}