#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    public class AssessmentDeletionAccount
    {
        /// <summary>
        /// The account_id of the deleted assessment.
        /// </summary>
        [JsonProperty("id", Required = Newtonsoft.Json.Required.Always)]
        public string Id { get; set; } = default!;

        /// <summary>
        /// The assessment the account's current risk now comes from. Absent when the account has no assessment with a decision left, and GET /v1/accounts/{account_id} then answers 404.
        /// </summary>
        [JsonProperty("current_assessment_id", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? CurrentAssessmentId { get; set; }
    }
}