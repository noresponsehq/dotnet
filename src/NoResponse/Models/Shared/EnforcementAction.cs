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
    /// What the workspace did in its identity provider: revoke_sessions (signed the account out everywhere), restrict (limited what the account can do), unrestrict (lifted an earlier restriction), fail_onboarding (stopped an account that was still onboarding), reject_activation (refused to activate a provisional account), activate (let a held onboarding or provisional account in) or none (decided to take no action).
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class EnforcementAction : IEquatable<EnforcementAction>, IOpenEnum<string>
    {
        public static readonly EnforcementAction RevokeSessions = new EnforcementAction("revoke_sessions");
        public static readonly EnforcementAction Restrict = new EnforcementAction("restrict");
        public static readonly EnforcementAction Unrestrict = new EnforcementAction("unrestrict");
        public static readonly EnforcementAction FailOnboarding = new EnforcementAction("fail_onboarding");
        public static readonly EnforcementAction RejectActivation = new EnforcementAction("reject_activation");
        public static readonly EnforcementAction Activate = new EnforcementAction("activate");
        public static readonly EnforcementAction None = new EnforcementAction("none");

        private static readonly Dictionary<string, EnforcementAction> _knownValues =
            new Dictionary<string, EnforcementAction>() {
                ["revoke_sessions"] = RevokeSessions,
                ["restrict"] = Restrict,
                ["unrestrict"] = Unrestrict,
                ["fail_onboarding"] = FailOnboarding,
                ["reject_activation"] = RejectActivation,
                ["activate"] = Activate,
                ["none"] = None
            };

        private static readonly ConcurrentDictionary<string, EnforcementAction> _values =
            new ConcurrentDictionary<string, EnforcementAction>(_knownValues);

        private EnforcementAction(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static EnforcementAction Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new EnforcementAction(value));
        }

        public static implicit operator EnforcementAction(string value) => Of(value);
        public static implicit operator string(EnforcementAction enforcementaction) => enforcementaction.Value;

        public static EnforcementAction[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as EnforcementAction);

        public bool Equals(EnforcementAction? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}