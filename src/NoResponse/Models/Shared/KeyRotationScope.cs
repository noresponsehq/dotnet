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
    public class KeyRotationScope : IEquatable<KeyRotationScope>, IOpenEnum<string>
    {
        public static readonly KeyRotationScope Assess = new KeyRotationScope("assess");
        public static readonly KeyRotationScope History = new KeyRotationScope("history");
        public static readonly KeyRotationScope Delete = new KeyRotationScope("delete");
        public static readonly KeyRotationScope Webhooks = new KeyRotationScope("webhooks");
        public static readonly KeyRotationScope Detailed = new KeyRotationScope("detailed");
        public static readonly KeyRotationScope Integrations = new KeyRotationScope("integrations");
        public static readonly KeyRotationScope Management = new KeyRotationScope("management");

        private static readonly Dictionary<string, KeyRotationScope> _knownValues =
            new Dictionary<string, KeyRotationScope>() {
                ["assess"] = Assess,
                ["history"] = History,
                ["delete"] = Delete,
                ["webhooks"] = Webhooks,
                ["detailed"] = Detailed,
                ["integrations"] = Integrations,
                ["management"] = Management
            };

        private static readonly ConcurrentDictionary<string, KeyRotationScope> _values =
            new ConcurrentDictionary<string, KeyRotationScope>(_knownValues);

        private KeyRotationScope(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static KeyRotationScope Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new KeyRotationScope(value));
        }

        public static implicit operator KeyRotationScope(string value) => Of(value);
        public static implicit operator string(KeyRotationScope keyrotationscope) => keyrotationscope.Value;

        public static KeyRotationScope[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as KeyRotationScope);

        public bool Equals(KeyRotationScope? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}