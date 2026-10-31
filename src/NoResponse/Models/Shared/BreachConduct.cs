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
    /// What a breach table's rows record against the identifier: chargeback, one or more chargebacks; declined_payment, one or more declined payments; forum_sanction, a forum ban, warning or infraction. A category and not a value: the schema has no member for one.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class BreachConduct : IEquatable<BreachConduct>, IOpenEnum<string>
    {
        public static readonly BreachConduct Chargeback = new BreachConduct("chargeback");
        public static readonly BreachConduct DeclinedPayment = new BreachConduct("declined_payment");
        public static readonly BreachConduct ForumSanction = new BreachConduct("forum_sanction");

        private static readonly Dictionary<string, BreachConduct> _knownValues =
            new Dictionary<string, BreachConduct>() {
                ["chargeback"] = Chargeback,
                ["declined_payment"] = DeclinedPayment,
                ["forum_sanction"] = ForumSanction
            };

        private static readonly ConcurrentDictionary<string, BreachConduct> _values =
            new ConcurrentDictionary<string, BreachConduct>(_knownValues);

        private BreachConduct(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static BreachConduct Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new BreachConduct(value));
        }

        public static implicit operator BreachConduct(string value) => Of(value);
        public static implicit operator string(BreachConduct breachconduct) => breachconduct.Value;

        public static BreachConduct[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as BreachConduct);

        public bool Equals(BreachConduct? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}