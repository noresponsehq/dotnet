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
    public class AuditSettingsEnvironment : IEquatable<AuditSettingsEnvironment>, IOpenEnum<string>
    {
        public static readonly AuditSettingsEnvironment Live = new AuditSettingsEnvironment("live");
        public static readonly AuditSettingsEnvironment Test = new AuditSettingsEnvironment("test");

        private static readonly Dictionary<string, AuditSettingsEnvironment> _knownValues =
            new Dictionary<string, AuditSettingsEnvironment>() {
                ["live"] = Live,
                ["test"] = Test
            };

        private static readonly ConcurrentDictionary<string, AuditSettingsEnvironment> _values =
            new ConcurrentDictionary<string, AuditSettingsEnvironment>(_knownValues);

        private AuditSettingsEnvironment(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static AuditSettingsEnvironment Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new AuditSettingsEnvironment(value));
        }

        public static implicit operator AuditSettingsEnvironment(string value) => Of(value);
        public static implicit operator string(AuditSettingsEnvironment auditsettingsenvironment) => auditsettingsenvironment.Value;

        public static AuditSettingsEnvironment[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as AuditSettingsEnvironment);

        public bool Equals(AuditSettingsEnvironment? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}