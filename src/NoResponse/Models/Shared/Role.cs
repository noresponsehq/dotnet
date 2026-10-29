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
    /// A built-in capability set. Specialist roles are independent; effective access is returned by GET /v1/workspaces/{workspace_id}/capabilities.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Role : IEquatable<Role>, IOpenEnum<string>
    {
        public static readonly Role Owner = new Role("owner");
        public static readonly Role Admin = new Role("admin");
        public static readonly Role Developer = new Role("developer");
        public static readonly Role Viewer = new Role("viewer");
        public static readonly Role Investigator = new Role("investigator");
        public static readonly Role Auditor = new Role("auditor");
        public static readonly Role Billing = new Role("billing");

        private static readonly Dictionary<string, Role> _knownValues =
            new Dictionary<string, Role>() {
                ["owner"] = Owner,
                ["admin"] = Admin,
                ["developer"] = Developer,
                ["viewer"] = Viewer,
                ["investigator"] = Investigator,
                ["auditor"] = Auditor,
                ["billing"] = Billing
            };

        private static readonly ConcurrentDictionary<string, Role> _values =
            new ConcurrentDictionary<string, Role>(_knownValues);

        private Role(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Role Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Role(value));
        }

        public static implicit operator Role(string value) => Of(value);
        public static implicit operator string(Role role) => role.Value;

        public static Role[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Role);

        public bool Equals(Role? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}