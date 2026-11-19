#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    [JsonConverter(typeof(OpenEnumConverter))]
    public class RiskDetailsEventIdentifier : IEquatable<RiskDetailsEventIdentifier>, IOpenEnum<string>
    {
        public static readonly RiskDetailsEventIdentifier Email = new RiskDetailsEventIdentifier("email");
        public static readonly RiskDetailsEventIdentifier Username = new RiskDetailsEventIdentifier("username");

        private static readonly Dictionary<string, RiskDetailsEventIdentifier> _knownValues =
            new Dictionary<string, RiskDetailsEventIdentifier>() {
                ["email"] = Email,
                ["username"] = Username
            };

        private static readonly ConcurrentDictionary<string, RiskDetailsEventIdentifier> _values =
            new ConcurrentDictionary<string, RiskDetailsEventIdentifier>(_knownValues);

        private RiskDetailsEventIdentifier(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static RiskDetailsEventIdentifier Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new RiskDetailsEventIdentifier(value));
        }

        public static implicit operator RiskDetailsEventIdentifier(string value) => Of(value);
        public static implicit operator string(RiskDetailsEventIdentifier riskdetailseventidentifier) => riskdetailseventidentifier.Value;

        public static RiskDetailsEventIdentifier[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as RiskDetailsEventIdentifier);

        public bool Equals(RiskDetailsEventIdentifier? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}