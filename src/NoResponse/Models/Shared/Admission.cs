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
    /// immediate (the default): an assessment of this flow is decided from the evidence its answer has, and later evidence may change the decision. held: while the account's current assessment of this flow has not completed, the account's decision for the flow is held, and the final decision arrives as account.risk_changed when the investigation completes or hold_seconds pass, whichever is first. Each flow has its own admission.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Admission : IEquatable<Admission>, IOpenEnum<string>
    {
        public static readonly Admission Immediate = new Admission("immediate");
        public static readonly Admission Held = new Admission("held");

        private static readonly Dictionary<string, Admission> _knownValues =
            new Dictionary<string, Admission>() {
                ["immediate"] = Immediate,
                ["held"] = Held
            };

        private static readonly ConcurrentDictionary<string, Admission> _values =
            new ConcurrentDictionary<string, Admission>(_knownValues);

        private Admission(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Admission Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Admission(value));
        }

        public static implicit operator Admission(string value) => Of(value);
        public static implicit operator string(Admission admission) => admission.Value;

        public static Admission[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Admission);

        public bool Equals(Admission? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}