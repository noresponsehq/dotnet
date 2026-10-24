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
    /// The identity provider the action ran in; custom for the workspace's own user store.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class EnforcementProvider : IEquatable<EnforcementProvider>, IOpenEnum<string>
    {
        public static readonly EnforcementProvider Clerk = new EnforcementProvider("clerk");
        public static readonly EnforcementProvider Workos = new EnforcementProvider("workos");
        public static readonly EnforcementProvider Auth0 = new EnforcementProvider("auth0");
        public static readonly EnforcementProvider Custom = new EnforcementProvider("custom");

        private static readonly Dictionary<string, EnforcementProvider> _knownValues =
            new Dictionary<string, EnforcementProvider>() {
                ["clerk"] = Clerk,
                ["workos"] = Workos,
                ["auth0"] = Auth0,
                ["custom"] = Custom
            };

        private static readonly ConcurrentDictionary<string, EnforcementProvider> _values =
            new ConcurrentDictionary<string, EnforcementProvider>(_knownValues);

        private EnforcementProvider(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static EnforcementProvider Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new EnforcementProvider(value));
        }

        public static implicit operator EnforcementProvider(string value) => Of(value);
        public static implicit operator string(EnforcementProvider enforcementprovider) => enforcementprovider.Value;

        public static EnforcementProvider[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as EnforcementProvider);

        public bool Equals(EnforcementProvider? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}