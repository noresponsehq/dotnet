#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;

    public class IdentifierSubmission
    {
        /// <summary>
        /// The original value submitted in assessment_id. Under canonical or mailbox matching the two accounts' values can differ.
        /// </summary>
        [JsonProperty("value", Required = Newtonsoft.Json.Required.Always)]
        public string Value { get; set; } = default!;

        /// <summary>
        /// Opaque, server-assigned assessment id: 'asm_' and a 26-character Crockford base32 ULID. Unique within the workspace; another workspace's assessment id answers 404.
        /// </summary>
        [JsonProperty("assessment_id", Required = Newtonsoft.Json.Required.Always)]
        public string AssessmentId { get; set; } = default!;

        /// <summary>
        /// When assessment_id was created.
        /// </summary>
        [JsonProperty("submitted_at", Required = Newtonsoft.Json.Required.Always)]
        public string SubmittedAt { get; set; } = default!;
    }
}