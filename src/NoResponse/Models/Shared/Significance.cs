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
    /// One rule ranks every change: identity changes (accounts, breaches, identifiers) are high, profile fields and findings are medium, new posts are low.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Significance : IEquatable<Significance>, IOpenEnum<string>
    {
        public static readonly Significance High = new Significance("high");
        public static readonly Significance Medium = new Significance("medium");
        public static readonly Significance Low = new Significance("low");

        private static readonly Dictionary<string, Significance> _knownValues =
            new Dictionary<string, Significance>() {
                ["high"] = High,
                ["medium"] = Medium,
                ["low"] = Low
            };

        private static readonly ConcurrentDictionary<string, Significance> _values =
            new ConcurrentDictionary<string, Significance>(_knownValues);

        private Significance(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Significance Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Significance(value));
        }

        public static implicit operator Significance(string value) => Of(value);
        public static implicit operator string(Significance significance) => significance.Value;

        public static Significance[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Significance);

        public bool Equals(Significance? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}