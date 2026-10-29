#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Read audit stream subscriptions.
    /// </summary>
    public class AuditStreamList
    {
        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<AuditStream> Items { get; set; } = default!;
    }
}