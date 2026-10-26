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
    public class Direction : IEquatable<Direction>, IOpenEnum<string>
    {
        public static readonly Direction Raises = new Direction("raises");
        public static readonly Direction Lowers = new Direction("lowers");
        public static readonly Direction Neutral = new Direction("neutral");

        private static readonly Dictionary<string, Direction> _knownValues =
            new Dictionary<string, Direction>() {
                ["raises"] = Raises,
                ["lowers"] = Lowers,
                ["neutral"] = Neutral
            };

        private static readonly ConcurrentDictionary<string, Direction> _values =
            new ConcurrentDictionary<string, Direction>(_knownValues);

        private Direction(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Direction Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Direction(value));
        }

        public static implicit operator Direction(string value) => Of(value);
        public static implicit operator string(Direction direction) => direction.Value;

        public static Direction[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Direction);

        public bool Equals(Direction? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}