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
    /// Machine codes an HTTP 401 answer can carry.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class AuthenticationErrorCode : IEquatable<AuthenticationErrorCode>, IOpenEnum<string>
    {
        public static readonly AuthenticationErrorCode Unauthorized = new AuthenticationErrorCode("unauthorized");

        private static readonly Dictionary<string, AuthenticationErrorCode> _knownValues =
            new Dictionary<string, AuthenticationErrorCode>() {
                ["unauthorized"] = Unauthorized
            };

        private static readonly ConcurrentDictionary<string, AuthenticationErrorCode> _values =
            new ConcurrentDictionary<string, AuthenticationErrorCode>(_knownValues);

        private AuthenticationErrorCode(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static AuthenticationErrorCode Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new AuthenticationErrorCode(value));
        }

        public static implicit operator AuthenticationErrorCode(string value) => Of(value);
        public static implicit operator string(AuthenticationErrorCode authenticationerrorcode) => authenticationerrorcode.Value;

        public static AuthenticationErrorCode[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as AuthenticationErrorCode);

        public bool Equals(AuthenticationErrorCode? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}