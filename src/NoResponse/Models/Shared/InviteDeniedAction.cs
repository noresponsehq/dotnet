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
    public class InviteDeniedAction : IEquatable<InviteDeniedAction>, IOpenEnum<string>
    {
        public static readonly InviteDeniedAction EvidenceRead = new InviteDeniedAction("evidence.read");
        public static readonly InviteDeniedAction ExportsRead = new InviteDeniedAction("exports.read");
        public static readonly InviteDeniedAction PolicyPublish = new InviteDeniedAction("policy.publish");

        private static readonly Dictionary<string, InviteDeniedAction> _knownValues =
            new Dictionary<string, InviteDeniedAction>() {
                ["evidence.read"] = EvidenceRead,
                ["exports.read"] = ExportsRead,
                ["policy.publish"] = PolicyPublish
            };

        private static readonly ConcurrentDictionary<string, InviteDeniedAction> _values =
            new ConcurrentDictionary<string, InviteDeniedAction>(_knownValues);

        private InviteDeniedAction(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static InviteDeniedAction Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new InviteDeniedAction(value));
        }

        public static implicit operator InviteDeniedAction(string value) => Of(value);
        public static implicit operator string(InviteDeniedAction invitedeniedaction) => invitedeniedaction.Value;

        public static InviteDeniedAction[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as InviteDeniedAction);

        public bool Equals(InviteDeniedAction? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}