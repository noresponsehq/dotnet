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
    /// How a submitted identifier agrees with what the evidence holds for the other identifiers. unknown is a definite 'cannot tell' (for example a truncated or too-common breach result); absent means the comparison was not made.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class ConsistencyValue : IEquatable<ConsistencyValue>, IOpenEnum<string>
    {
        public static readonly ConsistencyValue Match = new ConsistencyValue("match");
        public static readonly ConsistencyValue Partial = new ConsistencyValue("partial");
        public static readonly ConsistencyValue Mismatch = new ConsistencyValue("mismatch");
        public static readonly ConsistencyValue Unknown = new ConsistencyValue("unknown");

        private static readonly Dictionary<string, ConsistencyValue> _knownValues =
            new Dictionary<string, ConsistencyValue>() {
                ["match"] = Match,
                ["partial"] = Partial,
                ["mismatch"] = Mismatch,
                ["unknown"] = Unknown
            };

        private static readonly ConcurrentDictionary<string, ConsistencyValue> _values =
            new ConcurrentDictionary<string, ConsistencyValue>(_knownValues);

        private ConsistencyValue(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ConsistencyValue Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ConsistencyValue(value));
        }

        public static implicit operator ConsistencyValue(string value) => Of(value);
        public static implicit operator string(ConsistencyValue consistencyvalue) => consistencyvalue.Value;

        public static ConsistencyValue[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ConsistencyValue);

        public bool Equals(ConsistencyValue? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}