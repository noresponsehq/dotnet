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
    public class ListImpactKind : IEquatable<ListImpactKind>, IOpenEnum<string>
    {
        public static readonly ListImpactKind Email = new ListImpactKind("email");
        public static readonly ListImpactKind EmailDomain = new ListImpactKind("email_domain");

        private static readonly Dictionary<string, ListImpactKind> _knownValues =
            new Dictionary<string, ListImpactKind>() {
                ["email"] = Email,
                ["email_domain"] = EmailDomain
            };

        private static readonly ConcurrentDictionary<string, ListImpactKind> _values =
            new ConcurrentDictionary<string, ListImpactKind>(_knownValues);

        private ListImpactKind(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ListImpactKind Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ListImpactKind(value));
        }

        public static implicit operator ListImpactKind(string value) => Of(value);
        public static implicit operator string(ListImpactKind listimpactkind) => listimpactkind.Value;

        public static ListImpactKind[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ListImpactKind);

        public bool Equals(ListImpactKind? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}