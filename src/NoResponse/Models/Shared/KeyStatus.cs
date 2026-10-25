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
    /// active: the key authenticates. expired: a rotation's overlap window has ended (expires_at). revoked: the key was revoked (revoked_at) and answers 401.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class KeyStatus : IEquatable<KeyStatus>, IOpenEnum<string>
    {
        public static readonly KeyStatus Active = new KeyStatus("active");
        public static readonly KeyStatus Expired = new KeyStatus("expired");
        public static readonly KeyStatus Revoked = new KeyStatus("revoked");

        private static readonly Dictionary<string, KeyStatus> _knownValues =
            new Dictionary<string, KeyStatus>() {
                ["active"] = Active,
                ["expired"] = Expired,
                ["revoked"] = Revoked
            };

        private static readonly ConcurrentDictionary<string, KeyStatus> _values =
            new ConcurrentDictionary<string, KeyStatus>(_knownValues);

        private KeyStatus(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static KeyStatus Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new KeyStatus(value));
        }

        public static implicit operator KeyStatus(string value) => Of(value);
        public static implicit operator string(KeyStatus keystatus) => keystatus.Value;

        public static KeyStatus[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as KeyStatus);

        public bool Equals(KeyStatus? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}