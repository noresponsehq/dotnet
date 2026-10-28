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
    /// How long after the assessment's creation the change was made.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Delay : IEquatable<Delay>, IOpenEnum<string>
    {
        public static readonly Delay Under10s = new Delay("under_10s");
        public static readonly Delay Under1m = new Delay("under_1m");
        public static readonly Delay Under10m = new Delay("under_10m");
        public static readonly Delay Under1h = new Delay("under_1h");
        public static readonly Delay Under1d = new Delay("under_1d");
        public static readonly Delay Over1d = new Delay("over_1d");

        private static readonly Dictionary<string, Delay> _knownValues =
            new Dictionary<string, Delay>() {
                ["under_10s"] = Under10s,
                ["under_1m"] = Under1m,
                ["under_10m"] = Under10m,
                ["under_1h"] = Under1h,
                ["under_1d"] = Under1d,
                ["over_1d"] = Over1d
            };

        private static readonly ConcurrentDictionary<string, Delay> _values =
            new ConcurrentDictionary<string, Delay>(_knownValues);

        private Delay(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Delay Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Delay(value));
        }

        public static implicit operator Delay(string value) => Of(value);
        public static implicit operator string(Delay delay) => delay.Value;

        public static Delay[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Delay);

        public bool Equals(Delay? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}