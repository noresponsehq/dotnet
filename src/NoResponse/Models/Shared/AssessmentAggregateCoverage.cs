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
    /// Captured in the same database snapshot as the counts. Capture start times describe when counting began, not the earliest creation time counted: an older assessment may first answer later. Older outcomes are not inferred to be absent. Erased detail cannot be returned by drill-down even while its anonymous count remains.
    /// </summary>
    public class AssessmentAggregateCoverage
    {
        [JsonProperty("as_of", Required = Newtonsoft.Json.Required.Always)]
        public string AsOf { get; set; } = default!;

        [JsonProperty("first_answers_started_at", Required = Newtonsoft.Json.Required.Always)]
        public string FirstAnswersStartedAt { get; set; } = default!;

        [JsonProperty("score_outcomes_started_at", Required = Newtonsoft.Json.Required.Always)]
        public string ScoreOutcomesStartedAt { get; set; } = default!;

        [JsonProperty("retained_from", Required = Newtonsoft.Json.Required.Always)]
        public string RetainedFrom { get; set; } = default!;

        /// <summary>
        /// first_answers uses anonymous counters that can include erased detail. retained_first_answers computes exact intersections over retained first answers within 31 days. Requests exceeding the 100000-record work budget are refused.
        /// </summary>
        [JsonProperty("population", Required = Newtonsoft.Json.Required.Always)]
        public Population Population { get; set; } = default!;

        [JsonProperty("erased_records_included", Required = Newtonsoft.Json.Required.Always)]
        public bool ErasedRecordsIncluded { get; set; } = default!;

        [JsonProperty("overlapping_groups", Required = Newtonsoft.Json.Required.Always)]
        public bool OverlappingGroups { get; set; } = default!;

        [JsonProperty("late_flip_unit", Required = Newtonsoft.Json.Required.Always)]
        public string LateFlipUnit { get; } = "decision_transitions";

        [JsonProperty("historical_backfill", Required = Newtonsoft.Json.Required.Always)]
        public bool HistoricalBackfill { get; } = false;
    }
}