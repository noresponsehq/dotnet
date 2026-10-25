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
    /// The environment the key belongs to. A live key assesses with live sources; a test key answers from the deterministic simulator.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class KeyEnvironment : IEquatable<KeyEnvironment>, IOpenEnum<string>
    {
        public static readonly KeyEnvironment Live = new KeyEnvironment("live");
        public static readonly KeyEnvironment Test = new KeyEnvironment("test");

        private static readonly Dictionary<string, KeyEnvironment> _knownValues =
            new Dictionary<string, KeyEnvironment>() {
                ["live"] = Live,
                ["test"] = Test
            };

        private static readonly ConcurrentDictionary<string, KeyEnvironment> _values =
            new ConcurrentDictionary<string, KeyEnvironment>(_knownValues);

        private KeyEnvironment(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static KeyEnvironment Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new KeyEnvironment(value));
        }

        public static implicit operator KeyEnvironment(string value) => Of(value);
        public static implicit operator string(KeyEnvironment keyenvironment) => keyenvironment.Value;

        public static KeyEnvironment[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as KeyEnvironment);

        public bool Equals(KeyEnvironment? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}