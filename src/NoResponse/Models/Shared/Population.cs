#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// first_answers uses anonymous counters that can include erased detail. retained_first_answers computes exact intersections over retained first answers within 31 days. Requests exceeding the 100000-record work budget are refused.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Population : IEquatable<Population>, IOpenEnum<string>
    {
        public static readonly Population FirstAnswers = new Population("first_answers");
        public static readonly Population RetainedFirstAnswers = new Population("retained_first_answers");

        private static readonly Dictionary<string, Population> _knownValues =
            new Dictionary<string, Population>() {
                ["first_answers"] = FirstAnswers,
                ["retained_first_answers"] = RetainedFirstAnswers
            };

        private static readonly ConcurrentDictionary<string, Population> _values =
            new ConcurrentDictionary<string, Population>(_knownValues);

        private Population(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Population Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Population(value));
        }

        public static implicit operator Population(string value) => Of(value);
        public static implicit operator string(Population population) => population.Value;

        public static Population[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Population);

        public bool Equals(Population? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}