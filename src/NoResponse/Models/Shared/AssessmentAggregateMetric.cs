#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    [JsonConverter(typeof(OpenEnumConverter))]
    public class AssessmentAggregateMetric : IEquatable<AssessmentAggregateMetric>, IOpenEnum<string>
    {
        public static readonly AssessmentAggregateMetric Count = new AssessmentAggregateMetric("count");
        public static readonly AssessmentAggregateMetric ScoreHistogram = new AssessmentAggregateMetric("score_histogram");
        public static readonly AssessmentAggregateMetric ScoreOutcomes = new AssessmentAggregateMetric("score_outcomes");
        public static readonly AssessmentAggregateMetric LateFlips = new AssessmentAggregateMetric("late_flips");

        private static readonly Dictionary<string, AssessmentAggregateMetric> _knownValues =
            new Dictionary<string, AssessmentAggregateMetric>() {
                ["count"] = Count,
                ["score_histogram"] = ScoreHistogram,
                ["score_outcomes"] = ScoreOutcomes,
                ["late_flips"] = LateFlips
            };

        private static readonly ConcurrentDictionary<string, AssessmentAggregateMetric> _values =
            new ConcurrentDictionary<string, AssessmentAggregateMetric>(_knownValues);

        private AssessmentAggregateMetric(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static AssessmentAggregateMetric Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new AssessmentAggregateMetric(value));
        }

        public static implicit operator AssessmentAggregateMetric(string value) => Of(value);
        public static implicit operator string(AssessmentAggregateMetric assessmentaggregatemetric) => assessmentaggregatemetric.Value;

        public static AssessmentAggregateMetric[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as AssessmentAggregateMetric);

        public bool Equals(AssessmentAggregateMetric? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}