#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// At most 100 additions and removals combined. One transaction applies all changes or none. Idempotency-Key prevents duplicate changes for 24 hours; a different body under the same key returns 409. Matching reads see the new list immediately, while bounded durable jobs propagate account risk notifications.
    /// </summary>
    public class ListBulkRequest
    {
        [JsonProperty("add", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<ListEntryRequest>? Add { get; set; }

        [JsonProperty("remove", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<string>? Remove { get; set; }
    }
}