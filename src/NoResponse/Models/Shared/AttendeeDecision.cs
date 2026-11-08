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
    public class AttendeeDecision : IEquatable<AttendeeDecision>, IOpenEnum<string>
    {
        public static readonly AttendeeDecision Cleared = new AttendeeDecision("cleared");
        public static readonly AttendeeDecision Watch = new AttendeeDecision("watch");
        public static readonly AttendeeDecision Escalated = new AttendeeDecision("escalated");

        private static readonly Dictionary<string, AttendeeDecision> _knownValues =
            new Dictionary<string, AttendeeDecision>() {
                ["cleared"] = Cleared,
                ["watch"] = Watch,
                ["escalated"] = Escalated
            };

        private static readonly ConcurrentDictionary<string, AttendeeDecision> _values =
            new ConcurrentDictionary<string, AttendeeDecision>(_knownValues);

        private AttendeeDecision(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static AttendeeDecision Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new AttendeeDecision(value));
        }

        public static implicit operator AttendeeDecision(string value) => Of(value);
        public static implicit operator string(AttendeeDecision attendeedecision) => attendeedecision.Value;

        public static AttendeeDecision[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as AttendeeDecision);

        public bool Equals(AttendeeDecision? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}