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
    /// The recommended action: allow, step_up or review. Your application applies it.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Recommendation : IEquatable<Recommendation>, IOpenEnum<string>
    {
        public static readonly Recommendation Allow = new Recommendation("allow");
        public static readonly Recommendation StepUp = new Recommendation("step_up");
        public static readonly Recommendation Review = new Recommendation("review");

        private static readonly Dictionary<string, Recommendation> _knownValues =
            new Dictionary<string, Recommendation>() {
                ["allow"] = Allow,
                ["step_up"] = StepUp,
                ["review"] = Review
            };

        private static readonly ConcurrentDictionary<string, Recommendation> _values =
            new ConcurrentDictionary<string, Recommendation>(_knownValues);

        private Recommendation(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Recommendation Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Recommendation(value));
        }

        public static implicit operator Recommendation(string value) => Of(value);
        public static implicit operator string(Recommendation recommendation) => recommendation.Value;

        public static Recommendation[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Recommendation);

        public bool Equals(Recommendation? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}