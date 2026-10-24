#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class RecordEnforcementRequest
    {
        /// <summary>
        /// The account_id the workspace sends with its assessments, as one path segment (percent-encode '|' and ':' if the client requires it). A value that does not match the account_id shape answers 400 invalid_request.
        /// </summary>
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=account_id")]
        public string AccountId { get; set; } = default!;

        [NoResponseMetadata("request:mediaType=application/json")]
        public EnforcementRequest Body { get; set; } = new();
    }
}