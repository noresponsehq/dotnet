#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Utils;

    public class DeleteAssessmentRequest
    {
        /// <summary>
        /// Path segment, matched case-sensitively. A value that does not match the assessment_id shape answers 404, not 400.
        /// </summary>
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=assessment_id")]
        public string AssessmentId { get; set; } = default!;
    }
}