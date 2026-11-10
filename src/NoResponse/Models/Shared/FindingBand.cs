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
    /// How severe a finding is, most severe first.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class FindingBand : IEquatable<FindingBand>, IOpenEnum<string>
    {
        public static readonly FindingBand Critical = new FindingBand("critical");
        public static readonly FindingBand High = new FindingBand("high");
        public static readonly FindingBand Moderate = new FindingBand("moderate");
        public static readonly FindingBand Low = new FindingBand("low");
        public static readonly FindingBand Info = new FindingBand("info");

        private static readonly Dictionary<string, FindingBand> _knownValues =
            new Dictionary<string, FindingBand>() {
                ["critical"] = Critical,
                ["high"] = High,
                ["moderate"] = Moderate,
                ["low"] = Low,
                ["info"] = Info
            };

        private static readonly ConcurrentDictionary<string, FindingBand> _values =
            new ConcurrentDictionary<string, FindingBand>(_knownValues);

        private FindingBand(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static FindingBand Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new FindingBand(value));
        }

        public static implicit operator FindingBand(string value) => Of(value);
        public static implicit operator string(FindingBand findingband) => findingband.Value;

        public static FindingBand[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as FindingBand);

        public bool Equals(FindingBand? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}