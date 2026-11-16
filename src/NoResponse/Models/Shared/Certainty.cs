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
    /// confirmed: a linked record or the searched identifier states it directly. likely: a strong inference. possible: a weak inference.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Certainty : IEquatable<Certainty>, IOpenEnum<string>
    {
        public static readonly Certainty Confirmed = new Certainty("confirmed");
        public static readonly Certainty Likely = new Certainty("likely");
        public static readonly Certainty Possible = new Certainty("possible");

        private static readonly Dictionary<string, Certainty> _knownValues =
            new Dictionary<string, Certainty>() {
                ["confirmed"] = Confirmed,
                ["likely"] = Likely,
                ["possible"] = Possible
            };

        private static readonly ConcurrentDictionary<string, Certainty> _values =
            new ConcurrentDictionary<string, Certainty>(_knownValues);

        private Certainty(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Certainty Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Certainty(value));
        }

        public static implicit operator Certainty(string value) => Of(value);
        public static implicit operator string(Certainty certainty) => certainty.Value;

        public static Certainty[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Certainty);

        public bool Equals(Certainty? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}