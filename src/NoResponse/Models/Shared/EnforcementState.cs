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
    /// The account's state in the workspace's identity provider just before the action: onboarding, provisional (created, not yet activated), active, restricted or rejected.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class EnforcementState : IEquatable<EnforcementState>, IOpenEnum<string>
    {
        public static readonly EnforcementState Onboarding = new EnforcementState("onboarding");
        public static readonly EnforcementState Provisional = new EnforcementState("provisional");
        public static readonly EnforcementState Active = new EnforcementState("active");
        public static readonly EnforcementState Restricted = new EnforcementState("restricted");
        public static readonly EnforcementState Rejected = new EnforcementState("rejected");

        private static readonly Dictionary<string, EnforcementState> _knownValues =
            new Dictionary<string, EnforcementState>() {
                ["onboarding"] = Onboarding,
                ["provisional"] = Provisional,
                ["active"] = Active,
                ["restricted"] = Restricted,
                ["rejected"] = Rejected
            };

        private static readonly ConcurrentDictionary<string, EnforcementState> _values =
            new ConcurrentDictionary<string, EnforcementState>(_knownValues);

        private EnforcementState(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static EnforcementState Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new EnforcementState(value));
        }

        public static implicit operator EnforcementState(string value) => Of(value);
        public static implicit operator string(EnforcementState enforcementstate) => enforcementstate.Value;

        public static EnforcementState[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as EnforcementState);

        public bool Equals(EnforcementState? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}