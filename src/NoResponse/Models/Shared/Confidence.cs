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
    /// Evidence coverage: how much of the planned source evidence gave a definite answer. This is not confidence that an identity is genuine, agreement between identifiers, a risk score or a probability. unavailable means no usable evidence answered; a terminal uncovered assessment can have this value and recommend review.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Confidence : IEquatable<Confidence>, IOpenEnum<string>
    {
        public static readonly Confidence Low = new Confidence("low");
        public static readonly Confidence Medium = new Confidence("medium");
        public static readonly Confidence High = new Confidence("high");
        public static readonly Confidence Unavailable = new Confidence("unavailable");

        private static readonly Dictionary<string, Confidence> _knownValues =
            new Dictionary<string, Confidence>() {
                ["low"] = Low,
                ["medium"] = Medium,
                ["high"] = High,
                ["unavailable"] = Unavailable
            };

        private static readonly ConcurrentDictionary<string, Confidence> _values =
            new ConcurrentDictionary<string, Confidence>(_knownValues);

        private Confidence(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Confidence Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Confidence(value));
        }

        public static implicit operator Confidence(string value) => Of(value);
        public static implicit operator string(Confidence confidence) => confidence.Value;

        public static Confidence[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Confidence);

        public bool Equals(Confidence? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}