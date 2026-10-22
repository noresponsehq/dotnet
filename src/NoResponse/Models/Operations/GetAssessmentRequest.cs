#nullable enable
namespace NoResponse.Models.Operations
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    public class GetAssessmentRequest
    {
        /// <summary>
        /// Path segment, matched case-sensitively. A value that does not match the assessment_id shape answers 404, not 400.
        /// </summary>
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=assessment_id")]
        public string AssessmentId { get; set; } = default!;

        /// <summary>
        /// Optional standard or separately authorised detailed customer view; raw is rejected.
        /// </summary>
        [NoResponseMetadata("queryParam:style=form,explode=true,name=response_mode")]
        public ResponseMode? ResponseMode { get; set; }
    }
}