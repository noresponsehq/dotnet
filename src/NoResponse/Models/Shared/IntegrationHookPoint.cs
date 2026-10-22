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
    /// pre_registration runs before the user exists; post_login runs after a successful sign-in.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class IntegrationHookPoint : IEquatable<IntegrationHookPoint>, IOpenEnum<string>
    {
        public static readonly IntegrationHookPoint PreRegistration = new IntegrationHookPoint("pre_registration");
        public static readonly IntegrationHookPoint PostLogin = new IntegrationHookPoint("post_login");

        private static readonly Dictionary<string, IntegrationHookPoint> _knownValues =
            new Dictionary<string, IntegrationHookPoint>() {
                ["pre_registration"] = PreRegistration,
                ["post_login"] = PostLogin
            };

        private static readonly ConcurrentDictionary<string, IntegrationHookPoint> _values =
            new ConcurrentDictionary<string, IntegrationHookPoint>(_knownValues);

        private IntegrationHookPoint(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static IntegrationHookPoint Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new IntegrationHookPoint(value));
        }

        public static implicit operator IntegrationHookPoint(string value) => Of(value);
        public static implicit operator string(IntegrationHookPoint integrationhookpoint) => integrationhookpoint.Value;

        public static IntegrationHookPoint[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as IntegrationHookPoint);

        public bool Equals(IntegrationHookPoint? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}