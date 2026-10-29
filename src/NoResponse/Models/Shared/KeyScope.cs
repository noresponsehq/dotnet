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
    public class KeyScope : IEquatable<KeyScope>, IOpenEnum<string>
    {
        public static readonly KeyScope Assess = new KeyScope("assess");
        public static readonly KeyScope History = new KeyScope("history");
        public static readonly KeyScope Delete = new KeyScope("delete");
        public static readonly KeyScope Webhooks = new KeyScope("webhooks");
        public static readonly KeyScope Detailed = new KeyScope("detailed");
        public static readonly KeyScope Integrations = new KeyScope("integrations");
        public static readonly KeyScope Management = new KeyScope("management");
        public static readonly KeyScope Audit = new KeyScope("audit");
        public static readonly KeyScope Usage = new KeyScope("usage");
        public static readonly KeyScope Investigate = new KeyScope("investigate");
        public static readonly KeyScope Evidence = new KeyScope("evidence");
        public static readonly KeyScope Exports = new KeyScope("exports");
        public static readonly KeyScope AuditManage = new KeyScope("audit_manage");
        public static readonly KeyScope Settings = new KeyScope("settings");
        public static readonly KeyScope PolicyPublish = new KeyScope("policy_publish");
        public static readonly KeyScope Members = new KeyScope("members");
        public static readonly KeyScope MembersManage = new KeyScope("members_manage");

        private static readonly Dictionary<string, KeyScope> _knownValues =
            new Dictionary<string, KeyScope>() {
                ["assess"] = Assess,
                ["history"] = History,
                ["delete"] = Delete,
                ["webhooks"] = Webhooks,
                ["detailed"] = Detailed,
                ["integrations"] = Integrations,
                ["management"] = Management,
                ["audit"] = Audit,
                ["usage"] = Usage,
                ["investigate"] = Investigate,
                ["evidence"] = Evidence,
                ["exports"] = Exports,
                ["audit_manage"] = AuditManage,
                ["settings"] = Settings,
                ["policy_publish"] = PolicyPublish,
                ["members"] = Members,
                ["members_manage"] = MembersManage
            };

        private static readonly ConcurrentDictionary<string, KeyScope> _values =
            new ConcurrentDictionary<string, KeyScope>(_knownValues);

        private KeyScope(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static KeyScope Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new KeyScope(value));
        }

        public static implicit operator KeyScope(string value) => Of(value);
        public static implicit operator string(KeyScope keyscope) => keyscope.Value;

        public static KeyScope[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as KeyScope);

        public bool Equals(KeyScope? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}