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
    /// name_gender is the records' gender against the submitted first name's usual one.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Attribute : IEquatable<Attribute>, IOpenEnum<string>
    {
        public static readonly Attribute BirthDate = new Attribute("birth_date");
        public static readonly Attribute Gender = new Attribute("gender");
        public static readonly Attribute Nationality = new Attribute("nationality");
        public static readonly Attribute NameGender = new Attribute("name_gender");

        private static readonly Dictionary<string, Attribute> _knownValues =
            new Dictionary<string, Attribute>() {
                ["birth_date"] = BirthDate,
                ["gender"] = Gender,
                ["nationality"] = Nationality,
                ["name_gender"] = NameGender
            };

        private static readonly ConcurrentDictionary<string, Attribute> _values =
            new ConcurrentDictionary<string, Attribute>(_knownValues);

        private Attribute(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Attribute Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Attribute(value));
        }

        public static implicit operator Attribute(string value) => Of(value);
        public static implicit operator string(Attribute attribute) => attribute.Value;

        public static Attribute[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Attribute);

        public bool Equals(Attribute? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}