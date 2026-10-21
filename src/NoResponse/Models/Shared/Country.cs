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
    /// IP country, phone country, breach countries and policy_context.region together.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Country : IEquatable<Country>, IOpenEnum<string>
    {
        public static readonly Country Consistent = new Country("consistent");
        public static readonly Country Mismatch = new Country("mismatch");
        public static readonly Country Unknown = new Country("unknown");

        private static readonly Dictionary<string, Country> _knownValues =
            new Dictionary<string, Country>() {
                ["consistent"] = Consistent,
                ["mismatch"] = Mismatch,
                ["unknown"] = Unknown
            };

        private static readonly ConcurrentDictionary<string, Country> _values =
            new ConcurrentDictionary<string, Country>(_knownValues);

        private Country(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Country Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Country(value));
        }

        public static implicit operator Country(string value) => Of(value);
        public static implicit operator string(Country country) => country.Value;

        public static Country[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Country);

        public bool Equals(Country? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}