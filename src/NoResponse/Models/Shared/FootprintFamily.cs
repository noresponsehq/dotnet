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
    /// The source-neutral kind of evidence: account_presence (an account found on a site), auth_provider (the account at your auth provider), breach (breach and credential tables), breach_abuse (abuse and fraud communities' tables) and code_hosting (a code hosting profile).
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class FootprintFamily : IEquatable<FootprintFamily>, IOpenEnum<string>
    {
        public static readonly FootprintFamily AccountPresence = new FootprintFamily("account_presence");
        public static readonly FootprintFamily AuthProvider = new FootprintFamily("auth_provider");
        public static readonly FootprintFamily Breach = new FootprintFamily("breach");
        public static readonly FootprintFamily BreachAbuse = new FootprintFamily("breach_abuse");
        public static readonly FootprintFamily CodeHosting = new FootprintFamily("code_hosting");

        private static readonly Dictionary<string, FootprintFamily> _knownValues =
            new Dictionary<string, FootprintFamily>() {
                ["account_presence"] = AccountPresence,
                ["auth_provider"] = AuthProvider,
                ["breach"] = Breach,
                ["breach_abuse"] = BreachAbuse,
                ["code_hosting"] = CodeHosting
            };

        private static readonly ConcurrentDictionary<string, FootprintFamily> _values =
            new ConcurrentDictionary<string, FootprintFamily>(_knownValues);

        private FootprintFamily(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static FootprintFamily Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new FootprintFamily(value));
        }

        public static implicit operator FootprintFamily(string value) => Of(value);
        public static implicit operator string(FootprintFamily footprintfamily) => footprintfamily.Value;

        public static FootprintFamily[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as FootprintFamily);

        public bool Equals(FootprintFamily? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}