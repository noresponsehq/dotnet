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
    /// The attendee's role at the event: guest (a ticket holder), crew, vendor or press. Leave it out when you do not know it: an attendee without one has no role, not guest. A later row naming the same attendee replaces it, and a change of role alone keeps the attendee's screening results.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class AttendeeRole : IEquatable<AttendeeRole>, IOpenEnum<string>
    {
        public static readonly AttendeeRole Guest = new AttendeeRole("guest");
        public static readonly AttendeeRole Crew = new AttendeeRole("crew");
        public static readonly AttendeeRole Vendor = new AttendeeRole("vendor");
        public static readonly AttendeeRole Press = new AttendeeRole("press");

        private static readonly Dictionary<string, AttendeeRole> _knownValues =
            new Dictionary<string, AttendeeRole>() {
                ["guest"] = Guest,
                ["crew"] = Crew,
                ["vendor"] = Vendor,
                ["press"] = Press
            };

        private static readonly ConcurrentDictionary<string, AttendeeRole> _values =
            new ConcurrentDictionary<string, AttendeeRole>(_knownValues);

        private AttendeeRole(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static AttendeeRole Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new AttendeeRole(value));
        }

        public static implicit operator AttendeeRole(string value) => Of(value);
        public static implicit operator string(AttendeeRole attendeerole) => attendeerole.Value;

        public static AttendeeRole[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as AttendeeRole);

        public bool Equals(AttendeeRole? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}