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
    /// Why the attempt failed: the URL is no longer valid, the host did not resolve, it resolved to an address that is not public, no answer within 5 seconds, the connection failed, a 3xx (redirects are not followed) or another non-2xx status. Absent for a delivered attempt.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Failure : IEquatable<Failure>, IOpenEnum<string>
    {
        public static readonly Failure InvalidUrl = new Failure("invalid_url");
        public static readonly Failure ResolveFailed = new Failure("resolve_failed");
        public static readonly Failure AddressRefused = new Failure("address_refused");
        public static readonly Failure Timeout = new Failure("timeout");
        public static readonly Failure ConnectionFailed = new Failure("connection_failed");
        public static readonly Failure Redirect = new Failure("redirect");
        public static readonly Failure Non2xx = new Failure("non_2xx");

        private static readonly Dictionary<string, Failure> _knownValues =
            new Dictionary<string, Failure>() {
                ["invalid_url"] = InvalidUrl,
                ["resolve_failed"] = ResolveFailed,
                ["address_refused"] = AddressRefused,
                ["timeout"] = Timeout,
                ["connection_failed"] = ConnectionFailed,
                ["redirect"] = Redirect,
                ["non_2xx"] = Non2xx
            };

        private static readonly ConcurrentDictionary<string, Failure> _values =
            new ConcurrentDictionary<string, Failure>(_knownValues);

        private Failure(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Failure Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Failure(value));
        }

        public static implicit operator Failure(string value) => Of(value);
        public static implicit operator string(Failure failure) => failure.Value;

        public static Failure[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Failure);

        public bool Equals(Failure? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}