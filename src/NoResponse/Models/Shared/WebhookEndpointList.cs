#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// GET /v1/webhooks answer: every endpoint of the calling workspace, in every state, ordered by endpoint_id. A workspace holds at most 5, so there is no page.
    /// </summary>
    public class WebhookEndpointList
    {
        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<WebhookEndpoint> Items { get; set; } = default!;
    }
}