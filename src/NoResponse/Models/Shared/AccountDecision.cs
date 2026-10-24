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
    /// block when the score reached your block threshold (risk.block of the assessment), otherwise the assessment's recommendation. held only for a flow whose admission is held (settings flows.&lt;flow&gt;.admission), when the account's current assessment was made for that flow and its investigation has not completed (status partial): the final decision arrives as account.risk_changed when the investigation completes, decided by its score, or when the hold's deadline (hold_seconds after the assessment was created) passes first, as the flow's on_expiry action or the decision the evidence so far gives, whichever is stricter. Your application applies it.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class AccountDecision : IEquatable<AccountDecision>, IOpenEnum<string>
    {
        public static readonly AccountDecision Allow = new AccountDecision("allow");
        public static readonly AccountDecision StepUp = new AccountDecision("step_up");
        public static readonly AccountDecision Review = new AccountDecision("review");
        public static readonly AccountDecision Block = new AccountDecision("block");
        public static readonly AccountDecision Held = new AccountDecision("held");

        private static readonly Dictionary<string, AccountDecision> _knownValues =
            new Dictionary<string, AccountDecision>() {
                ["allow"] = Allow,
                ["step_up"] = StepUp,
                ["review"] = Review,
                ["block"] = Block,
                ["held"] = Held
            };

        private static readonly ConcurrentDictionary<string, AccountDecision> _values =
            new ConcurrentDictionary<string, AccountDecision>(_knownValues);

        private AccountDecision(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static AccountDecision Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new AccountDecision(value));
        }

        public static implicit operator AccountDecision(string value) => Of(value);
        public static implicit operator string(AccountDecision accountdecision) => accountdecision.Value;

        public static AccountDecision[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as AccountDecision);

        public bool Equals(AccountDecision? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}