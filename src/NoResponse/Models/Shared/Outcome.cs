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
    /// What the provider later learned about the assessed account. confirmed_abuse, account_takeover and chargeback report abuse; false_positive and legitimate withdraw an earlier abuse report of this tenant for the assessment; unknown is recorded only. Outcomes are append-only and the latest one recorded for an assessment wins.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Outcome : IEquatable<Outcome>, IOpenEnum<string>
    {
        public static readonly Outcome ConfirmedAbuse = new Outcome("confirmed_abuse");
        public static readonly Outcome AccountTakeover = new Outcome("account_takeover");
        public static readonly Outcome Chargeback = new Outcome("chargeback");
        public static readonly Outcome FalsePositive = new Outcome("false_positive");
        public static readonly Outcome Legitimate = new Outcome("legitimate");
        public static readonly Outcome Unknown = new Outcome("unknown");

        private static readonly Dictionary<string, Outcome> _knownValues =
            new Dictionary<string, Outcome>() {
                ["confirmed_abuse"] = ConfirmedAbuse,
                ["account_takeover"] = AccountTakeover,
                ["chargeback"] = Chargeback,
                ["false_positive"] = FalsePositive,
                ["legitimate"] = Legitimate,
                ["unknown"] = Unknown
            };

        private static readonly ConcurrentDictionary<string, Outcome> _values =
            new ConcurrentDictionary<string, Outcome>(_knownValues);

        private Outcome(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Outcome Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Outcome(value));
        }

        public static implicit operator Outcome(string value) => Of(value);
        public static implicit operator string(Outcome outcome) => outcome.Value;

        public static Outcome[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Outcome);

        public bool Equals(Outcome? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}