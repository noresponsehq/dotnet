#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class RecordAccountOutcomeRequest
    {
        /// <summary>
        /// The account_id the workspace sends with its assessments, as one path segment (percent-encode '|' and ':' if the client requires it). A value that does not match the account_id shape answers 400 invalid_request.
        /// </summary>
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=account_id")]
        public string AccountId { get; set; } = default!;

        /// <summary>
        /// Opaque client key, 16 to 128 characters of [A-Za-z0-9._:-] (a UUID or ULID fits). Scoped to the workspace principal and bound to the canonical body for 24 hours. Optional: the SDK generates a UUID when you omit it and sends the same key on every retry of the call.
        /// </summary>
        [NoResponseMetadata("header:style=simple,explode=false,name=Idempotency-Key")]
        public string? IdempotencyKey { get; set; }

        [NoResponseMetadata("request:mediaType=application/json")]
        public AccountOutcomeRequest Body { get; set; } = new();
    }
}