#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// POST /v1/key_usage/batches body: facts about requests made with API keys your platform issued to your accounts, one event per request. Each event names the key by the SHA-256 that the assessment's issued_credential.sha256 carried, not the key itself. The SDKs buffer events and send them in the background. A batch holds 1 to 500 events and at most 256 KiB. Idempotency-Key is required and binds the body as on POST /v1/assessments, so a retried batch is counted once.
    /// </summary>
    public class KeyUsageBatchRequest
    {
        [JsonProperty("events", Required = Newtonsoft.Json.Required.Always)]
        public List<KeyUsageEvent> Events { get; set; } = default!;
    }
}