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
    /// What the group's people share: an event, an organisation, a place or another attribute.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class GroupType : IEquatable<GroupType>, IOpenEnum<string>
    {
        public static readonly GroupType Event = new GroupType("event");
        public static readonly GroupType Organisation = new GroupType("organisation");
        public static readonly GroupType Place = new GroupType("place");
        public static readonly GroupType Custom = new GroupType("custom");

        private static readonly Dictionary<string, GroupType> _knownValues =
            new Dictionary<string, GroupType>() {
                ["event"] = Event,
                ["organisation"] = Organisation,
                ["place"] = Place,
                ["custom"] = Custom
            };

        private static readonly ConcurrentDictionary<string, GroupType> _values =
            new ConcurrentDictionary<string, GroupType>(_knownValues);

        private GroupType(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static GroupType Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new GroupType(value));
        }

        public static implicit operator GroupType(string value) => Of(value);
        public static implicit operator string(GroupType grouptype) => grouptype.Value;

        public static GroupType[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as GroupType);

        public bool Equals(GroupType? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}