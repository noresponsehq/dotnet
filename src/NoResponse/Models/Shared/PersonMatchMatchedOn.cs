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
    public class PersonMatchMatchedOn : IEquatable<PersonMatchMatchedOn>, IOpenEnum<string>
    {
        public static readonly PersonMatchMatchedOn Email = new PersonMatchMatchedOn("email");
        public static readonly PersonMatchMatchedOn Phone = new PersonMatchMatchedOn("phone");

        private static readonly Dictionary<string, PersonMatchMatchedOn> _knownValues =
            new Dictionary<string, PersonMatchMatchedOn>() {
                ["email"] = Email,
                ["phone"] = Phone
            };

        private static readonly ConcurrentDictionary<string, PersonMatchMatchedOn> _values =
            new ConcurrentDictionary<string, PersonMatchMatchedOn>(_knownValues);

        private PersonMatchMatchedOn(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static PersonMatchMatchedOn Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new PersonMatchMatchedOn(value));
        }

        public static implicit operator PersonMatchMatchedOn(string value) => Of(value);
        public static implicit operator string(PersonMatchMatchedOn personmatchmatchedon) => personmatchmatchedon.Value;

        public static PersonMatchMatchedOn[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as PersonMatchMatchedOn);

        public bool Equals(PersonMatchMatchedOn? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}