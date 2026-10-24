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
    /// applied when the identity provider accepted the action, failed when it refused it or could not be reached. Only applied counts as enforced.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class EnforcementStatus : IEquatable<EnforcementStatus>, IOpenEnum<string>
    {
        public static readonly EnforcementStatus Applied = new EnforcementStatus("applied");
        public static readonly EnforcementStatus Failed = new EnforcementStatus("failed");

        private static readonly Dictionary<string, EnforcementStatus> _knownValues =
            new Dictionary<string, EnforcementStatus>() {
                ["applied"] = Applied,
                ["failed"] = Failed
            };

        private static readonly ConcurrentDictionary<string, EnforcementStatus> _values =
            new ConcurrentDictionary<string, EnforcementStatus>(_knownValues);

        private EnforcementStatus(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static EnforcementStatus Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new EnforcementStatus(value));
        }

        public static implicit operator EnforcementStatus(string value) => Of(value);
        public static implicit operator string(EnforcementStatus enforcementstatus) => enforcementstatus.Value;

        public static EnforcementStatus[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as EnforcementStatus);

        public bool Equals(EnforcementStatus? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}