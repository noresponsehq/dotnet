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
    /// How much of the planned evidence answered; it says nothing about risk.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class RiskConfidence : IEquatable<RiskConfidence>, IOpenEnum<string>
    {
        public static readonly RiskConfidence Low = new RiskConfidence("low");
        public static readonly RiskConfidence Medium = new RiskConfidence("medium");
        public static readonly RiskConfidence High = new RiskConfidence("high");

        private static readonly Dictionary<string, RiskConfidence> _knownValues =
            new Dictionary<string, RiskConfidence>() {
                ["low"] = Low,
                ["medium"] = Medium,
                ["high"] = High
            };

        private static readonly ConcurrentDictionary<string, RiskConfidence> _values =
            new ConcurrentDictionary<string, RiskConfidence>(_knownValues);

        private RiskConfidence(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static RiskConfidence Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new RiskConfidence(value));
        }

        public static implicit operator RiskConfidence(string value) => Of(value);
        public static implicit operator string(RiskConfidence riskconfidence) => riskconfidence.Value;

        public static RiskConfidence[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as RiskConfidence);

        public bool Equals(RiskConfidence? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}