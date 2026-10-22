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
    /// The authentication provider that calls the endpoint.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class IntegrationPlatform : IEquatable<IntegrationPlatform>, IOpenEnum<string>
    {
        public static readonly IntegrationPlatform Workos = new IntegrationPlatform("workos");
        public static readonly IntegrationPlatform Auth0 = new IntegrationPlatform("auth0");

        private static readonly Dictionary<string, IntegrationPlatform> _knownValues =
            new Dictionary<string, IntegrationPlatform>() {
                ["workos"] = Workos,
                ["auth0"] = Auth0
            };

        private static readonly ConcurrentDictionary<string, IntegrationPlatform> _values =
            new ConcurrentDictionary<string, IntegrationPlatform>(_knownValues);

        private IntegrationPlatform(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static IntegrationPlatform Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new IntegrationPlatform(value));
        }

        public static implicit operator IntegrationPlatform(string value) => Of(value);
        public static implicit operator string(IntegrationPlatform integrationplatform) => integrationplatform.Value;

        public static IntegrationPlatform[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as IntegrationPlatform);

        public bool Equals(IntegrationPlatform? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}