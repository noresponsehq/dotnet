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
    public class RiskDetailsEventKind : IEquatable<RiskDetailsEventKind>, IOpenEnum<string>
    {
        public static readonly RiskDetailsEventKind Created = new RiskDetailsEventKind("created");
        public static readonly RiskDetailsEventKind LastActive = new RiskDetailsEventKind("last_active");
        public static readonly RiskDetailsEventKind Breached = new RiskDetailsEventKind("breached");

        private static readonly Dictionary<string, RiskDetailsEventKind> _knownValues =
            new Dictionary<string, RiskDetailsEventKind>() {
                ["created"] = Created,
                ["last_active"] = LastActive,
                ["breached"] = Breached
            };

        private static readonly ConcurrentDictionary<string, RiskDetailsEventKind> _values =
            new ConcurrentDictionary<string, RiskDetailsEventKind>(_knownValues);

        private RiskDetailsEventKind(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static RiskDetailsEventKind Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new RiskDetailsEventKind(value));
        }

        public static implicit operator RiskDetailsEventKind(string value) => Of(value);
        public static implicit operator string(RiskDetailsEventKind riskdetailseventkind) => riskdetailseventkind.Value;

        public static RiskDetailsEventKind[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as RiskDetailsEventKind);

        public bool Equals(RiskDetailsEventKind? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}