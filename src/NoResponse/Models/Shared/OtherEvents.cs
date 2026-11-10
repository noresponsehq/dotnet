#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Where else in the environment the attendee appears, read live from those events: deleting one removes it here.
    /// </summary>
    public class OtherEvents
    {
        /// <summary>
        /// How many attendees of other events match, of which items lists the 20 most recent.
        /// </summary>
        [JsonProperty("total", Required = Newtonsoft.Json.Required.Always)]
        public long Total { get; set; } = default!;

        /// <summary>
        /// The 20 attendees of the most recent other events, newest event first.
        /// </summary>
        [JsonProperty("items", Required = Newtonsoft.Json.Required.Always)]
        public List<OtherEvent> Items { get; set; } = default!;
    }
}