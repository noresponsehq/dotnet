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
    public class KeyDeniedAction : IEquatable<KeyDeniedAction>, IOpenEnum<string>
    {
        public static readonly KeyDeniedAction EvidenceRead = new KeyDeniedAction("evidence.read");
        public static readonly KeyDeniedAction ExportsRead = new KeyDeniedAction("exports.read");
        public static readonly KeyDeniedAction PolicyPublish = new KeyDeniedAction("policy.publish");

        private static readonly Dictionary<string, KeyDeniedAction> _knownValues =
            new Dictionary<string, KeyDeniedAction>() {
                ["evidence.read"] = EvidenceRead,
                ["exports.read"] = ExportsRead,
                ["policy.publish"] = PolicyPublish
            };

        private static readonly ConcurrentDictionary<string, KeyDeniedAction> _values =
            new ConcurrentDictionary<string, KeyDeniedAction>(_knownValues);

        private KeyDeniedAction(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static KeyDeniedAction Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new KeyDeniedAction(value));
        }

        public static implicit operator KeyDeniedAction(string value) => Of(value);
        public static implicit operator string(KeyDeniedAction keydeniedaction) => keydeniedaction.Value;

        public static KeyDeniedAction[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as KeyDeniedAction);

        public bool Equals(KeyDeniedAction? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}