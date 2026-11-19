#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The schedule to set.
    /// </summary>
    public class PersonScheduleRequest
    {
        /// <summary>
        /// How often the person is investigated again: daily, weekly or monthly, counted from when the schedule was set; off runs no scheduled investigation.
        /// </summary>
        [JsonProperty("period", Required = Newtonsoft.Json.Required.Always)]
        public SchedulePeriod Period { get; set; } = default!;
    }
}