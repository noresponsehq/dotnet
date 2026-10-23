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
    public class EventType : IEquatable<EventType>, IOpenEnum<string>
    {
        public static readonly EventType AssessmentUpdated = new EventType("assessment.updated");

        private static readonly Dictionary<string, EventType> _knownValues =
            new Dictionary<string, EventType>() {
                ["assessment.updated"] = AssessmentUpdated
            };

        private static readonly ConcurrentDictionary<string, EventType> _values =
            new ConcurrentDictionary<string, EventType>(_knownValues);

        private EventType(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static EventType Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new EventType(value));
        }

        public static implicit operator EventType(string value) => Of(value);
        public static implicit operator string(EventType eventtype) => eventtype.Value;

        public static EventType[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as EventType);

        public bool Equals(EventType? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}