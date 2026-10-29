#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System.Collections.Generic;

    public class AuditStreamRequest
    {
        [JsonProperty("endpoint_id", Required = Newtonsoft.Json.Required.Always)]
        public string EndpointId { get; set; } = default!;

        [JsonProperty("actions", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<string>? Actions { get; set; }
    }
}