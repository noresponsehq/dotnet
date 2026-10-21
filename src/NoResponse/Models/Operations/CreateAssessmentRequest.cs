#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class CreateAssessmentRequest
    {
        /// <summary>
        /// Opaque client key, 16 to 128 characters of [A-Za-z0-9._:-] (a UUID or ULID fits). Scoped to the tenant principal and bound to the canonical body for 24 hours.
        /// </summary>
        [NoResponseMetadata("header:style=simple,explode=false,name=Idempotency-Key")]
        public string IdempotencyKey { get; set; } = default!;

        [NoResponseMetadata("request:mediaType=application/json")]
        public AssessmentRequest Body { get; set; } = new();
    }
}