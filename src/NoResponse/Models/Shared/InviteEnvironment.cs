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
    public class InviteEnvironment : IEquatable<InviteEnvironment>, IOpenEnum<string>
    {
        public static readonly InviteEnvironment Live = new InviteEnvironment("live");
        public static readonly InviteEnvironment Test = new InviteEnvironment("test");

        private static readonly Dictionary<string, InviteEnvironment> _knownValues =
            new Dictionary<string, InviteEnvironment>() {
                ["live"] = Live,
                ["test"] = Test
            };

        private static readonly ConcurrentDictionary<string, InviteEnvironment> _values =
            new ConcurrentDictionary<string, InviteEnvironment>(_knownValues);

        private InviteEnvironment(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static InviteEnvironment Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new InviteEnvironment(value));
        }

        public static implicit operator InviteEnvironment(string value) => Of(value);
        public static implicit operator string(InviteEnvironment inviteenvironment) => inviteenvironment.Value;

        public static InviteEnvironment[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as InviteEnvironment);

        public bool Equals(InviteEnvironment? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}