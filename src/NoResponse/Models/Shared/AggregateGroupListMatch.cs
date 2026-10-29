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
    /// The list action captured with the first answer. none means no matching entry was found. For retained_first_answers an absent field means the older assessment did not capture list state; this remains a separate group.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class AggregateGroupListMatch : IEquatable<AggregateGroupListMatch>, IOpenEnum<string>
    {
        public static readonly AggregateGroupListMatch Block = new AggregateGroupListMatch("block");
        public static readonly AggregateGroupListMatch Allow = new AggregateGroupListMatch("allow");
        public static readonly AggregateGroupListMatch None = new AggregateGroupListMatch("none");

        private static readonly Dictionary<string, AggregateGroupListMatch> _knownValues =
            new Dictionary<string, AggregateGroupListMatch>() {
                ["block"] = Block,
                ["allow"] = Allow,
                ["none"] = None
            };

        private static readonly ConcurrentDictionary<string, AggregateGroupListMatch> _values =
            new ConcurrentDictionary<string, AggregateGroupListMatch>(_knownValues);

        private AggregateGroupListMatch(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static AggregateGroupListMatch Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new AggregateGroupListMatch(value));
        }

        public static implicit operator AggregateGroupListMatch(string value) => Of(value);
        public static implicit operator string(AggregateGroupListMatch aggregategrouplistmatch) => aggregategrouplistmatch.Value;

        public static AggregateGroupListMatch[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as AggregateGroupListMatch);

        public bool Equals(AggregateGroupListMatch? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}