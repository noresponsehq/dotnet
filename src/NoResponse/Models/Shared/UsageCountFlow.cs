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
    /// assessments only.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class UsageCountFlow : IEquatable<UsageCountFlow>, IOpenEnum<string>
    {
        public static readonly UsageCountFlow AccountCreation = new UsageCountFlow("account_creation");
        public static readonly UsageCountFlow Login = new UsageCountFlow("login");
        public static readonly UsageCountFlow Purchase = new UsageCountFlow("purchase");

        private static readonly Dictionary<string, UsageCountFlow> _knownValues =
            new Dictionary<string, UsageCountFlow>() {
                ["account_creation"] = AccountCreation,
                ["login"] = Login,
                ["purchase"] = Purchase
            };

        private static readonly ConcurrentDictionary<string, UsageCountFlow> _values =
            new ConcurrentDictionary<string, UsageCountFlow>(_knownValues);

        private UsageCountFlow(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static UsageCountFlow Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new UsageCountFlow(value));
        }

        public static implicit operator UsageCountFlow(string value) => Of(value);
        public static implicit operator string(UsageCountFlow usagecountflow) => usagecountflow.Value;

        public static UsageCountFlow[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as UsageCountFlow);

        public bool Equals(UsageCountFlow? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}