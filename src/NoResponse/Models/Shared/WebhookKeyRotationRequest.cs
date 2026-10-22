#nullable enable
namespace NoResponse.Models.Shared
{
    using NoResponse.Utils;

    /// <summary>
    /// POST /v1/webhooks/{endpoint_id}/rotate body: an empty object. The old key keeps verifying for 7 days.
    /// </summary>
    public class WebhookKeyRotationRequest
    {
    }
}