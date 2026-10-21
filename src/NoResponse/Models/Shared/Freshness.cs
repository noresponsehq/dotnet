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
    /// Freshness class of the evidence behind a signal. unknown whenever no evidence was obtained.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Freshness : IEquatable<Freshness>, IOpenEnum<string>
    {
        public static readonly Freshness Fresh = new Freshness("fresh");
        public static readonly Freshness Stale = new Freshness("stale");
        public static readonly Freshness Unknown = new Freshness("unknown");

        private static readonly Dictionary<string, Freshness> _knownValues =
            new Dictionary<string, Freshness>() {
                ["fresh"] = Fresh,
                ["stale"] = Stale,
                ["unknown"] = Unknown
            };

        private static readonly ConcurrentDictionary<string, Freshness> _values =
            new ConcurrentDictionary<string, Freshness>(_knownValues);

        private Freshness(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Freshness Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Freshness(value));
        }

        public static implicit operator Freshness(string value) => Of(value);
        public static implicit operator string(Freshness freshness) => freshness.Value;

        public static Freshness[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Freshness);

        public bool Equals(Freshness? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}