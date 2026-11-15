#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class CreateInvestigationRequest
    {
        /// <summary>
        /// Opaque request key, 16 to 128 characters. Repeating the key and request returns the same resource in its current state while it is retained. A different request with the same key answers 409. Use a new key for a deliberate new operation. Keys are scoped to the environment and operation; person searches also scope them to the group and person. Optional: the SDK generates a UUID when you omit it and sends the same key on every retry of the call.
        /// </summary>
        [NoResponseMetadata("header:style=simple,explode=false,name=Idempotency-Key")]
        public string? IdempotencyKey { get; set; }

        [NoResponseMetadata("request:mediaType=application/json")]
        public InvestigationRequest Body { get; set; } = new();
    }
}