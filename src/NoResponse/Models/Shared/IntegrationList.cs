#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// GET /v1/integrations: every integration of the tenant, oldest first, at most 10. No secret is ever listed.
    /// </summary>
    public class IntegrationList
    {
        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<Integration> Items { get; set; } = default!;
    }
}