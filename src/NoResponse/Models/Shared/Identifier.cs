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
    public class Identifier : IEquatable<Identifier>, IOpenEnum<string>
    {
        public static readonly Identifier Email = new Identifier("email");
        public static readonly Identifier Username = new Identifier("username");

        private static readonly Dictionary<string, Identifier> _knownValues =
            new Dictionary<string, Identifier>() {
                ["email"] = Email,
                ["username"] = Username
            };

        private static readonly ConcurrentDictionary<string, Identifier> _values =
            new ConcurrentDictionary<string, Identifier>(_knownValues);

        private Identifier(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Identifier Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Identifier(value));
        }

        public static implicit operator Identifier(string value) => Of(value);
        public static implicit operator string(Identifier identifier) => identifier.Value;

        public static Identifier[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Identifier);

        public bool Equals(Identifier? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}