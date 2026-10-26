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
    public class Impact : IEquatable<Impact>, IOpenEnum<string>
    {
        public static readonly Impact None = new Impact("none");
        public static readonly Impact Low = new Impact("low");
        public static readonly Impact Medium = new Impact("medium");
        public static readonly Impact High = new Impact("high");

        private static readonly Dictionary<string, Impact> _knownValues =
            new Dictionary<string, Impact>() {
                ["none"] = None,
                ["low"] = Low,
                ["medium"] = Medium,
                ["high"] = High
            };

        private static readonly ConcurrentDictionary<string, Impact> _values =
            new ConcurrentDictionary<string, Impact>(_knownValues);

        private Impact(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Impact Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Impact(value));
        }

        public static implicit operator Impact(string value) => Of(value);
        public static implicit operator string(Impact impact) => impact.Value;

        public static Impact[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Impact);

        public bool Equals(Impact? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}