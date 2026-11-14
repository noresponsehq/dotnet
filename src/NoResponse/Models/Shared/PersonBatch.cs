#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// What the batch did, row by row.
    /// </summary>
    public class PersonBatch
    {
        [JsonProperty("created", Required = Newtonsoft.Json.Required.Always)]
        public long Created { get; set; } = default!;

        [JsonProperty("updated", Required = Newtonsoft.Json.Required.Always)]
        public long Updated { get; set; } = default!;

        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<PersonBatchItem> Items { get; set; } = default!;
    }
}