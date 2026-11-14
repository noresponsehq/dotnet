#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// People in upload order.
    /// </summary>
    public class PersonList
    {
        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<Person> Items { get; set; } = default!;

        /// <summary>
        /// Pass as after to read the next page.
        /// </summary>
        [JsonProperty("next_after", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? NextAfter { get; set; }
    }
}