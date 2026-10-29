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
    public class IdentifierSearchKind : IEquatable<IdentifierSearchKind>, IOpenEnum<string>
    {
        public static readonly IdentifierSearchKind Email = new IdentifierSearchKind("email");
        public static readonly IdentifierSearchKind Phone = new IdentifierSearchKind("phone");
        public static readonly IdentifierSearchKind Username = new IdentifierSearchKind("username");

        private static readonly Dictionary<string, IdentifierSearchKind> _knownValues =
            new Dictionary<string, IdentifierSearchKind>() {
                ["email"] = Email,
                ["phone"] = Phone,
                ["username"] = Username
            };

        private static readonly ConcurrentDictionary<string, IdentifierSearchKind> _values =
            new ConcurrentDictionary<string, IdentifierSearchKind>(_knownValues);

        private IdentifierSearchKind(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static IdentifierSearchKind Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new IdentifierSearchKind(value));
        }

        public static implicit operator IdentifierSearchKind(string value) => Of(value);
        public static implicit operator string(IdentifierSearchKind identifiersearchkind) => identifiersearchkind.Value;

        public static IdentifierSearchKind[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as IdentifierSearchKind);

        public bool Equals(IdentifierSearchKind? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}