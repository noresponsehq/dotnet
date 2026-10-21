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
    /// Which submitted identifier a signal concerns. ip is a selector of breach_exposure only.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class IdentifierKind : IEquatable<IdentifierKind>, IOpenEnum<string>
    {
        public static readonly IdentifierKind Email = new IdentifierKind("email");
        public static readonly IdentifierKind Username = new IdentifierKind("username");
        public static readonly IdentifierKind Phone = new IdentifierKind("phone");
        public static readonly IdentifierKind Ip = new IdentifierKind("ip");

        private static readonly Dictionary<string, IdentifierKind> _knownValues =
            new Dictionary<string, IdentifierKind>() {
                ["email"] = Email,
                ["username"] = Username,
                ["phone"] = Phone,
                ["ip"] = Ip
            };

        private static readonly ConcurrentDictionary<string, IdentifierKind> _values =
            new ConcurrentDictionary<string, IdentifierKind>(_knownValues);

        private IdentifierKind(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static IdentifierKind Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new IdentifierKind(value));
        }

        public static implicit operator IdentifierKind(string value) => Of(value);
        public static implicit operator string(IdentifierKind identifierkind) => identifierkind.Value;

        public static IdentifierKind[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as IdentifierKind);

        public bool Equals(IdentifierKind? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}