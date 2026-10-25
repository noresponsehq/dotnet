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
    /// An evidence lane: accounts (account presence for email and username), phone, breach, domain (the email domain's registration) and ip.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Lane : IEquatable<Lane>, IOpenEnum<string>
    {
        public static readonly Lane Accounts = new Lane("accounts");
        public static readonly Lane Phone = new Lane("phone");
        public static readonly Lane Breach = new Lane("breach");
        public static readonly Lane Domain = new Lane("domain");
        public static readonly Lane Ip = new Lane("ip");

        private static readonly Dictionary<string, Lane> _knownValues =
            new Dictionary<string, Lane>() {
                ["accounts"] = Accounts,
                ["phone"] = Phone,
                ["breach"] = Breach,
                ["domain"] = Domain,
                ["ip"] = Ip
            };

        private static readonly ConcurrentDictionary<string, Lane> _values =
            new ConcurrentDictionary<string, Lane>(_knownValues);

        private Lane(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Lane Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Lane(value));
        }

        public static implicit operator Lane(string value) => Of(value);
        public static implicit operator string(Lane lane) => lane.Value;

        public static Lane[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Lane);

        public bool Equals(Lane? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}