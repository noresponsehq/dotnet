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
    /// The person's role in the group: guest (a ticket holder), crew, vendor or press. Leave it out when you do not know it: a person without one has no role, not guest. A later row naming the same person replaces it, and a change of role alone keeps the person's screening results.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class PersonRole : IEquatable<PersonRole>, IOpenEnum<string>
    {
        public static readonly PersonRole Guest = new PersonRole("guest");
        public static readonly PersonRole Crew = new PersonRole("crew");
        public static readonly PersonRole Vendor = new PersonRole("vendor");
        public static readonly PersonRole Press = new PersonRole("press");

        private static readonly Dictionary<string, PersonRole> _knownValues =
            new Dictionary<string, PersonRole>() {
                ["guest"] = Guest,
                ["crew"] = Crew,
                ["vendor"] = Vendor,
                ["press"] = Press
            };

        private static readonly ConcurrentDictionary<string, PersonRole> _values =
            new ConcurrentDictionary<string, PersonRole>(_knownValues);

        private PersonRole(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static PersonRole Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new PersonRole(value));
        }

        public static implicit operator PersonRole(string value) => Of(value);
        public static implicit operator string(PersonRole personrole) => personrole.Value;

        public static PersonRole[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as PersonRole);

        public bool Equals(PersonRole? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}