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
    public class MemberEnvironment : IEquatable<MemberEnvironment>, IOpenEnum<string>
    {
        public static readonly MemberEnvironment Live = new MemberEnvironment("live");
        public static readonly MemberEnvironment Test = new MemberEnvironment("test");

        private static readonly Dictionary<string, MemberEnvironment> _knownValues =
            new Dictionary<string, MemberEnvironment>() {
                ["live"] = Live,
                ["test"] = Test
            };

        private static readonly ConcurrentDictionary<string, MemberEnvironment> _values =
            new ConcurrentDictionary<string, MemberEnvironment>(_knownValues);

        private MemberEnvironment(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static MemberEnvironment Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new MemberEnvironment(value));
        }

        public static implicit operator MemberEnvironment(string value) => Of(value);
        public static implicit operator string(MemberEnvironment memberenvironment) => memberenvironment.Value;

        public static MemberEnvironment[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as MemberEnvironment);

        public bool Equals(MemberEnvironment? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}