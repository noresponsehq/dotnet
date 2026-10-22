#nullable enable
namespace NoResponse.Models.Operations
{
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    public class RotateIntegrationSecretRequest
    {
        /// <summary>
        /// Path segment, matched case-sensitively. A value that does not match the integration_id shape, or another tenant's id, answers 404.
        /// </summary>
        [NoResponseMetadata("pathParam:style=simple,explode=false,name=integration_id")]
        public string IntegrationId { get; set; } = default!;

        [NoResponseMetadata("request:mediaType=application/json")]
        public IntegrationRotationRequest Body { get; set; } = new();
    }
}