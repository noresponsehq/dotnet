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
    /// A reviewer's overrule (POST .../people/{person_id}/records), which decides joined whatever the likelihood and sticks for as long as a lookup finds the same record. Absent when nobody overruled it.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Verdict : IEquatable<Verdict>, IOpenEnum<string>
    {
        public static readonly Verdict Joined = new Verdict("joined");
        public static readonly Verdict Rejected = new Verdict("rejected");

        private static readonly Dictionary<string, Verdict> _knownValues =
            new Dictionary<string, Verdict>() {
                ["joined"] = Joined,
                ["rejected"] = Rejected
            };

        private static readonly ConcurrentDictionary<string, Verdict> _values =
            new ConcurrentDictionary<string, Verdict>(_knownValues);

        private Verdict(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Verdict Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Verdict(value));
        }

        public static implicit operator Verdict(string value) => Of(value);
        public static implicit operator string(Verdict verdict) => verdict.Value;

        public static Verdict[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Verdict);

        public bool Equals(Verdict? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}