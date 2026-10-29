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
    /// The kind of resource the change acted on.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class AuditEventTargetType : IEquatable<AuditEventTargetType>, IOpenEnum<string>
    {
        public static readonly AuditEventTargetType Workspace = new AuditEventTargetType("workspace");
        public static readonly AuditEventTargetType Member = new AuditEventTargetType("member");
        public static readonly AuditEventTargetType Invite = new AuditEventTargetType("invite");
        public static readonly AuditEventTargetType Key = new AuditEventTargetType("key");
        public static readonly AuditEventTargetType Setting = new AuditEventTargetType("setting");
        public static readonly AuditEventTargetType WebhookEndpoint = new AuditEventTargetType("webhook_endpoint");
        public static readonly AuditEventTargetType Integration = new AuditEventTargetType("integration");
        public static readonly AuditEventTargetType Billing = new AuditEventTargetType("billing");
        public static readonly AuditEventTargetType Review = new AuditEventTargetType("review");

        private static readonly Dictionary<string, AuditEventTargetType> _knownValues =
            new Dictionary<string, AuditEventTargetType>() {
                ["workspace"] = Workspace,
                ["member"] = Member,
                ["invite"] = Invite,
                ["key"] = Key,
                ["setting"] = Setting,
                ["webhook_endpoint"] = WebhookEndpoint,
                ["integration"] = Integration,
                ["billing"] = Billing,
                ["review"] = Review
            };

        private static readonly ConcurrentDictionary<string, AuditEventTargetType> _values =
            new ConcurrentDictionary<string, AuditEventTargetType>(_knownValues);

        private AuditEventTargetType(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static AuditEventTargetType Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new AuditEventTargetType(value));
        }

        public static implicit operator AuditEventTargetType(string value) => Of(value);
        public static implicit operator string(AuditEventTargetType auditeventtargettype) => auditeventtargettype.Value;

        public static AuditEventTargetType[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as AuditEventTargetType);

        public bool Equals(AuditEventTargetType? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}