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
    public class Topic : IEquatable<Topic>, IOpenEnum<string>
    {
        public static readonly Topic Identity = new Topic("identity");
        public static readonly Topic Location = new Topic("location");
        public static readonly Topic Movement = new Topic("movement");
        public static readonly Topic Timeline = new Topic("timeline");
        public static readonly Topic Interests = new Topic("interests");
        public static readonly Topic Relationships = new Topic("relationships");
        public static readonly Topic Work = new Topic("work");
        public static readonly Topic Education = new Topic("education");
        public static readonly Topic Accounts = new Topic("accounts");
        public static readonly Topic Credentials = new Topic("credentials");
        public static readonly Topic Devices = new Topic("devices");
        public static readonly Topic Absence = new Topic("absence");

        private static readonly Dictionary<string, Topic> _knownValues =
            new Dictionary<string, Topic>() {
                ["identity"] = Identity,
                ["location"] = Location,
                ["movement"] = Movement,
                ["timeline"] = Timeline,
                ["interests"] = Interests,
                ["relationships"] = Relationships,
                ["work"] = Work,
                ["education"] = Education,
                ["accounts"] = Accounts,
                ["credentials"] = Credentials,
                ["devices"] = Devices,
                ["absence"] = Absence
            };

        private static readonly ConcurrentDictionary<string, Topic> _values =
            new ConcurrentDictionary<string, Topic>(_knownValues);

        private Topic(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Topic Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Topic(value));
        }

        public static implicit operator Topic(string value) => Of(value);
        public static implicit operator string(Topic topic) => topic.Value;

        public static Topic[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Topic);

        public bool Equals(Topic? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}