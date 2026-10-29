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
    /// The credential namespace of the creator. An external user ID equal to an API-key label does not grant ownership of that key's private view.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class OwnerType : IEquatable<OwnerType>, IOpenEnum<string>
    {
        public static readonly OwnerType User = new OwnerType("user");
        public static readonly OwnerType ApiKey = new OwnerType("api_key");

        private static readonly Dictionary<string, OwnerType> _knownValues =
            new Dictionary<string, OwnerType>() {
                ["user"] = User,
                ["api_key"] = ApiKey
            };

        private static readonly ConcurrentDictionary<string, OwnerType> _values =
            new ConcurrentDictionary<string, OwnerType>(_knownValues);

        private OwnerType(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static OwnerType Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new OwnerType(value));
        }

        public static implicit operator OwnerType(string value) => Of(value);
        public static implicit operator string(OwnerType ownertype) => ownertype.Value;

        public static OwnerType[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as OwnerType);

        public bool Equals(OwnerType? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}