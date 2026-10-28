#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class BindProvisionalAccountRequest
    {
        /// <summary>
        /// Path segment, matched case-sensitively. A value that does not match the assessment_id shape answers 404, not 400.
        /// </summary>
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=assessment_id")]
        public string AssessmentId { get; set; } = default!;

        [NoResponseMetadata("request:mediaType=application/json")]
        public AccountBindingRequest Body { get; set; } = new();
    }
}