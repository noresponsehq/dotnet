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
    public class Environment : IEquatable<Environment>, IOpenEnum<string>
    {
        public static readonly Environment Live = new Environment("live");
        public static readonly Environment Test = new Environment("test");
        public static readonly Environment Workspace = new Environment("workspace");

        private static readonly Dictionary<string, Environment> _knownValues =
            new Dictionary<string, Environment>() {
                ["live"] = Live,
                ["test"] = Test,
                ["workspace"] = Workspace
            };

        private static readonly ConcurrentDictionary<string, Environment> _values =
            new ConcurrentDictionary<string, Environment>(_knownValues);

        private Environment(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Environment Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Environment(value));
        }

        public static implicit operator Environment(string value) => Of(value);
        public static implicit operator string(Environment environment) => environment.Value;

        public static Environment[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Environment);

        public bool Equals(Environment? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}