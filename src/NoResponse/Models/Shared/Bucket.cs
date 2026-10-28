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
    /// The period each count covers, in UTC; a week starts on Monday.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Bucket : IEquatable<Bucket>, IOpenEnum<string>
    {
        public static readonly Bucket Hour = new Bucket("hour");
        public static readonly Bucket Day = new Bucket("day");
        public static readonly Bucket Week = new Bucket("week");

        private static readonly Dictionary<string, Bucket> _knownValues =
            new Dictionary<string, Bucket>() {
                ["hour"] = Hour,
                ["day"] = Day,
                ["week"] = Week
            };

        private static readonly ConcurrentDictionary<string, Bucket> _values =
            new ConcurrentDictionary<string, Bucket>(_knownValues);

        private Bucket(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Bucket Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Bucket(value));
        }

        public static implicit operator Bucket(string value) => Of(value);
        public static implicit operator string(Bucket bucket) => bucket.Value;

        public static Bucket[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Bucket);

        public bool Equals(Bucket? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}