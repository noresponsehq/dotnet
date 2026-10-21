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
    /// The kind of data a breach table held beside the identifier. password means a plaintext password was present and password_hash means a hash, salt or token was present. The value itself is not stored or returned; the schema has no member for one.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class BreachExposed : IEquatable<BreachExposed>, IOpenEnum<string>
    {
        public static readonly BreachExposed Email = new BreachExposed("email");
        public static readonly BreachExposed Username = new BreachExposed("username");
        public static readonly BreachExposed Name = new BreachExposed("name");
        public static readonly BreachExposed Password = new BreachExposed("password");
        public static readonly BreachExposed PasswordHash = new BreachExposed("password_hash");
        public static readonly BreachExposed Phone = new BreachExposed("phone");
        public static readonly BreachExposed Address = new BreachExposed("address");
        public static readonly BreachExposed Dob = new BreachExposed("dob");
        public static readonly BreachExposed Ip = new BreachExposed("ip");
        public static readonly BreachExposed Payment = new BreachExposed("payment");
        public static readonly BreachExposed SecurityQa = new BreachExposed("security_qa");

        private static readonly Dictionary<string, BreachExposed> _knownValues =
            new Dictionary<string, BreachExposed>() {
                ["email"] = Email,
                ["username"] = Username,
                ["name"] = Name,
                ["password"] = Password,
                ["password_hash"] = PasswordHash,
                ["phone"] = Phone,
                ["address"] = Address,
                ["dob"] = Dob,
                ["ip"] = Ip,
                ["payment"] = Payment,
                ["security_qa"] = SecurityQa
            };

        private static readonly ConcurrentDictionary<string, BreachExposed> _values =
            new ConcurrentDictionary<string, BreachExposed>(_knownValues);

        private BreachExposed(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static BreachExposed Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new BreachExposed(value));
        }

        public static implicit operator BreachExposed(string value) => Of(value);
        public static implicit operator string(BreachExposed breachexposed) => breachexposed.Value;

        public static BreachExposed[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as BreachExposed);

        public bool Equals(BreachExposed? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}