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
    /// How long the synchronous answer waits for sources. quick waits only for the fast sources, those that answer within 500 ms of ingress (or deadline_ms when lower); full waits for the whole budget, up to 3000 ms. Sources still running at the cut continue asynchronously and update the assessment either way.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Depth : IEquatable<Depth>, IOpenEnum<string>
    {
        public static readonly Depth Quick = new Depth("quick");
        public static readonly Depth Full = new Depth("full");

        private static readonly Dictionary<string, Depth> _knownValues =
            new Dictionary<string, Depth>() {
                ["quick"] = Quick,
                ["full"] = Full
            };

        private static readonly ConcurrentDictionary<string, Depth> _values =
            new ConcurrentDictionary<string, Depth>(_knownValues);

        private Depth(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Depth Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Depth(value));
        }

        public static implicit operator Depth(string value) => Of(value);
        public static implicit operator string(Depth depth) => depth.Value;

        public static Depth[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Depth);

        public bool Equals(Depth? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}