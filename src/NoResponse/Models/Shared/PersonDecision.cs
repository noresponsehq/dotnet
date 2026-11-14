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
    public class PersonDecision : IEquatable<PersonDecision>, IOpenEnum<string>
    {
        public static readonly PersonDecision Cleared = new PersonDecision("cleared");
        public static readonly PersonDecision Watch = new PersonDecision("watch");
        public static readonly PersonDecision Escalated = new PersonDecision("escalated");

        private static readonly Dictionary<string, PersonDecision> _knownValues =
            new Dictionary<string, PersonDecision>() {
                ["cleared"] = Cleared,
                ["watch"] = Watch,
                ["escalated"] = Escalated
            };

        private static readonly ConcurrentDictionary<string, PersonDecision> _values =
            new ConcurrentDictionary<string, PersonDecision>(_knownValues);

        private PersonDecision(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static PersonDecision Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new PersonDecision(value));
        }

        public static implicit operator PersonDecision(string value) => Of(value);
        public static implicit operator string(PersonDecision persondecision) => persondecision.Value;

        public static PersonDecision[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as PersonDecision);

        public bool Equals(PersonDecision? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}