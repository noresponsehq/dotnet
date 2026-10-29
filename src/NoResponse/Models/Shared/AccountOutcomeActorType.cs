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
    public class AccountOutcomeActorType : IEquatable<AccountOutcomeActorType>, IOpenEnum<string>
    {
        public static readonly AccountOutcomeActorType ApiKey = new AccountOutcomeActorType("api_key");
        public static readonly AccountOutcomeActorType User = new AccountOutcomeActorType("user");

        private static readonly Dictionary<string, AccountOutcomeActorType> _knownValues =
            new Dictionary<string, AccountOutcomeActorType>() {
                ["api_key"] = ApiKey,
                ["user"] = User
            };

        private static readonly ConcurrentDictionary<string, AccountOutcomeActorType> _values =
            new ConcurrentDictionary<string, AccountOutcomeActorType>(_knownValues);

        private AccountOutcomeActorType(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static AccountOutcomeActorType Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new AccountOutcomeActorType(value));
        }

        public static implicit operator AccountOutcomeActorType(string value) => Of(value);
        public static implicit operator string(AccountOutcomeActorType accountoutcomeactortype) => accountoutcomeactortype.Value;

        public static AccountOutcomeActorType[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as AccountOutcomeActorType);

        public bool Equals(AccountOutcomeActorType? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}