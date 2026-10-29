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
    public class KeyKind : IEquatable<KeyKind>, IOpenEnum<string>
    {
        public static readonly KeyKind Service = new KeyKind("service");
        public static readonly KeyKind Personal = new KeyKind("personal");

        private static readonly Dictionary<string, KeyKind> _knownValues =
            new Dictionary<string, KeyKind>() {
                ["service"] = Service,
                ["personal"] = Personal
            };

        private static readonly ConcurrentDictionary<string, KeyKind> _values =
            new ConcurrentDictionary<string, KeyKind>(_knownValues);

        private KeyKind(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static KeyKind Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new KeyKind(value));
        }

        public static implicit operator KeyKind(string value) => Of(value);
        public static implicit operator string(KeyKind keykind) => keykind.Value;

        public static KeyKind[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as KeyKind);

        public bool Equals(KeyKind? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}