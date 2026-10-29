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
    public class Band : IEquatable<Band>, IOpenEnum<string>
    {
        public static readonly Band Low = new Band("low");
        public static readonly Band Medium = new Band("medium");
        public static readonly Band High = new Band("high");
        public static readonly Band Critical = new Band("critical");

        private static readonly Dictionary<string, Band> _knownValues =
            new Dictionary<string, Band>() {
                ["low"] = Low,
                ["medium"] = Medium,
                ["high"] = High,
                ["critical"] = Critical
            };

        private static readonly ConcurrentDictionary<string, Band> _values =
            new ConcurrentDictionary<string, Band>(_knownValues);

        private Band(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Band Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Band(value));
        }

        public static implicit operator Band(string value) => Of(value);
        public static implicit operator string(Band band) => band.Value;

        public static Band[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Band);

        public bool Equals(Band? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}