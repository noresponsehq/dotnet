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
    /// Machine codes an HTTP 403 answer can carry.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class PermissionDeniedErrorCode : IEquatable<PermissionDeniedErrorCode>, IOpenEnum<string>
    {
        public static readonly PermissionDeniedErrorCode TenantForbidden = new PermissionDeniedErrorCode("tenant_forbidden");

        private static readonly Dictionary<string, PermissionDeniedErrorCode> _knownValues =
            new Dictionary<string, PermissionDeniedErrorCode>() {
                ["tenant_forbidden"] = TenantForbidden
            };

        private static readonly ConcurrentDictionary<string, PermissionDeniedErrorCode> _values =
            new ConcurrentDictionary<string, PermissionDeniedErrorCode>(_knownValues);

        private PermissionDeniedErrorCode(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static PermissionDeniedErrorCode Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new PermissionDeniedErrorCode(value));
        }

        public static implicit operator PermissionDeniedErrorCode(string value) => Of(value);
        public static implicit operator string(PermissionDeniedErrorCode permissiondeniederrorcode) => permissiondeniederrorcode.Value;

        public static PermissionDeniedErrorCode[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as PermissionDeniedErrorCode);

        public bool Equals(PermissionDeniedErrorCode? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}