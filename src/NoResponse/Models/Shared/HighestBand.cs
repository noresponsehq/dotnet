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
    public class HighestBand : IEquatable<HighestBand>, IOpenEnum<string>
    {
        public static readonly HighestBand Critical = new HighestBand("critical");
        public static readonly HighestBand High = new HighestBand("high");
        public static readonly HighestBand Moderate = new HighestBand("moderate");
        public static readonly HighestBand Low = new HighestBand("low");
        public static readonly HighestBand Info = new HighestBand("info");

        private static readonly Dictionary<string, HighestBand> _knownValues =
            new Dictionary<string, HighestBand>() {
                ["critical"] = Critical,
                ["high"] = High,
                ["moderate"] = Moderate,
                ["low"] = Low,
                ["info"] = Info
            };

        private static readonly ConcurrentDictionary<string, HighestBand> _values =
            new ConcurrentDictionary<string, HighestBand>(_knownValues);

        private HighestBand(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static HighestBand Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new HighestBand(value));
        }

        public static implicit operator HighestBand(string value) => Of(value);
        public static implicit operator string(HighestBand highestband) => highestband.Value;

        public static HighestBand[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as HighestBand);

        public bool Equals(HighestBand? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}