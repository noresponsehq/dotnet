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
    public class MemberDeniedAction : IEquatable<MemberDeniedAction>, IOpenEnum<string>
    {
        public static readonly MemberDeniedAction EvidenceRead = new MemberDeniedAction("evidence.read");
        public static readonly MemberDeniedAction ExportsRead = new MemberDeniedAction("exports.read");
        public static readonly MemberDeniedAction PolicyPublish = new MemberDeniedAction("policy.publish");

        private static readonly Dictionary<string, MemberDeniedAction> _knownValues =
            new Dictionary<string, MemberDeniedAction>() {
                ["evidence.read"] = EvidenceRead,
                ["exports.read"] = ExportsRead,
                ["policy.publish"] = PolicyPublish
            };

        private static readonly ConcurrentDictionary<string, MemberDeniedAction> _values =
            new ConcurrentDictionary<string, MemberDeniedAction>(_knownValues);

        private MemberDeniedAction(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static MemberDeniedAction Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new MemberDeniedAction(value));
        }

        public static implicit operator MemberDeniedAction(string value) => Of(value);
        public static implicit operator string(MemberDeniedAction memberdeniedaction) => memberdeniedaction.Value;

        public static MemberDeniedAction[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as MemberDeniedAction);

        public bool Equals(MemberDeniedAction? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}