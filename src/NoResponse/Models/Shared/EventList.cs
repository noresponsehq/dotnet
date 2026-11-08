#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Events, newest first.
    /// </summary>
    public class EventList
    {
        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<Event> Items { get; set; } = default!;

        /// <summary>
        /// Pass as before to read the next, older page.
        /// </summary>
        [JsonProperty("next_before", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? NextBefore { get; set; }
    }
}