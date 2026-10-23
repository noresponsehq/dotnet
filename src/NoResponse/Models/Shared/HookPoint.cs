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
    /// The provider event a provider_hook context was sent for; absent for any other transport.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class HookPoint : IEquatable<HookPoint>, IOpenEnum<string>
    {
        public static readonly HookPoint PreRegistration = new HookPoint("pre_registration");
        public static readonly HookPoint PostRegistration = new HookPoint("post_registration");
        public static readonly HookPoint PreLogin = new HookPoint("pre_login");
        public static readonly HookPoint PostLogin = new HookPoint("post_login");
        public static readonly HookPoint TokenIssuance = new HookPoint("token_issuance");

        private static readonly Dictionary<string, HookPoint> _knownValues =
            new Dictionary<string, HookPoint>() {
                ["pre_registration"] = PreRegistration,
                ["post_registration"] = PostRegistration,
                ["pre_login"] = PreLogin,
                ["post_login"] = PostLogin,
                ["token_issuance"] = TokenIssuance
            };

        private static readonly ConcurrentDictionary<string, HookPoint> _values =
            new ConcurrentDictionary<string, HookPoint>(_knownValues);

        private HookPoint(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static HookPoint Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new HookPoint(value));
        }

        public static implicit operator HookPoint(string value) => Of(value);
        public static implicit operator string(HookPoint hookpoint) => hookpoint.Value;

        public static HookPoint[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as HookPoint);

        public bool Equals(HookPoint? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}