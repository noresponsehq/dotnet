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
    public class EnrichmentKind : IEquatable<EnrichmentKind>, IOpenEnum<string>
    {
        public static readonly EnrichmentKind Username = new EnrichmentKind("username");
        public static readonly EnrichmentKind Email = new EnrichmentKind("email");
        public static readonly EnrichmentKind Phone = new EnrichmentKind("phone");
        public static readonly EnrichmentKind PersonName = new EnrichmentKind("person_name");

        private static readonly Dictionary<string, EnrichmentKind> _knownValues =
            new Dictionary<string, EnrichmentKind>() {
                ["username"] = Username,
                ["email"] = Email,
                ["phone"] = Phone,
                ["person_name"] = PersonName
            };

        private static readonly ConcurrentDictionary<string, EnrichmentKind> _values =
            new ConcurrentDictionary<string, EnrichmentKind>(_knownValues);

        private EnrichmentKind(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static EnrichmentKind Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new EnrichmentKind(value));
        }

        public static implicit operator EnrichmentKind(string value) => Of(value);
        public static implicit operator string(EnrichmentKind enrichmentkind) => enrichmentkind.Value;

        public static EnrichmentKind[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as EnrichmentKind);

        public bool Equals(EnrichmentKind? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}