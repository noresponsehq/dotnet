#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class CreateAssessmentRequest
    {
        /// <summary>
        /// Opaque client key, 16 to 128 characters of [A-Za-z0-9._:-] (a UUID or ULID fits). Scoped to the workspace principal and bound to the canonical body for 24 hours. Optional: the SDK generates a UUID when you omit it and sends the same key on every retry of the call.
        /// </summary>
        [NoResponseMetadata("header:style=simple,explode=false,name=Idempotency-Key")]
        public string? IdempotencyKey { get; set; }

        [NoResponseMetadata("request:mediaType=application/json")]
        public AssessmentRequest Body { get; set; } = new();
    }
}