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
    /// unique: an email or phone, which belongs to one person by design. distinctive: rare enough that a new search on it likely finds one person. common: shared by many people, so a search on it finds strangers. A new search is suggested for unique and distinctive values only; any value can still be searched.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Rating : IEquatable<Rating>, IOpenEnum<string>
    {
        public static readonly Rating Unique = new Rating("unique");
        public static readonly Rating Distinctive = new Rating("distinctive");
        public static readonly Rating Common = new Rating("common");

        private static readonly Dictionary<string, Rating> _knownValues =
            new Dictionary<string, Rating>() {
                ["unique"] = Unique,
                ["distinctive"] = Distinctive,
                ["common"] = Common
            };

        private static readonly ConcurrentDictionary<string, Rating> _values =
            new ConcurrentDictionary<string, Rating>(_knownValues);

        private Rating(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Rating Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Rating(value));
        }

        public static implicit operator Rating(string value) => Of(value);
        public static implicit operator string(Rating rating) => rating.Value;

        public static Rating[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Rating);

        public bool Equals(Rating? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}