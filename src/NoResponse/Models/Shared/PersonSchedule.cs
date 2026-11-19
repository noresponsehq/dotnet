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
    /// The person's rerun schedule. Each due run starts a full investigation of the person, as POST /v1/investigations does, and its changes are stored as changes. The schedule lives in the database and the worker runs it, so it survives restarts and releases; each due run starts exactly one investigation.
    /// </summary>
    public class PersonSchedule
    {
        /// <summary>
        /// How often the person is investigated again: daily, weekly or monthly, counted from when the schedule was set; off runs no scheduled investigation.
        /// </summary>
        [JsonProperty("period", Required = Newtonsoft.Json.Required.Always)]
        public SchedulePeriod Period { get; set; } = default!;

        /// <summary>
        /// When the next scheduled investigation starts, within a minute. null when the schedule is off.
        /// </summary>
        [JsonProperty("next_run_at", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public string? NextRunAt { get; set; }

        /// <summary>
        /// The investigation the schedule last started. null before its first run.
        /// </summary>
        [JsonProperty("last_investigation_id", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public string? LastInvestigationId { get; set; }

        /// <summary>
        /// Why the last due run started no investigation, as the problem code its start would have answered. null when the last due run started one, or none has come due. A run that started none is not retried: the schedule moves to its next period.
        /// </summary>
        [JsonProperty("last_error", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public LastError? LastError { get; set; }

        [JsonProperty("updated_at", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public string? UpdatedAt { get; set; }

        /// <summary>
        /// Who last set the schedule.
        /// </summary>
        [JsonProperty("updated_by", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public string? UpdatedBy { get; set; }
    }
}