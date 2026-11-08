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
    public class ScreeningDepth : IEquatable<ScreeningDepth>, IOpenEnum<string>
    {
        public static readonly ScreeningDepth Standard = new ScreeningDepth("standard");

        private static readonly Dictionary<string, ScreeningDepth> _knownValues =
            new Dictionary<string, ScreeningDepth>() {
                ["standard"] = Standard
            };

        private static readonly ConcurrentDictionary<string, ScreeningDepth> _values =
            new ConcurrentDictionary<string, ScreeningDepth>(_knownValues);

        private ScreeningDepth(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ScreeningDepth Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ScreeningDepth(value));
        }

        public static implicit operator ScreeningDepth(string value) => Of(value);
        public static implicit operator string(ScreeningDepth screeningdepth) => screeningdepth.Value;

        public static ScreeningDepth[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ScreeningDepth);

        public bool Equals(ScreeningDepth? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}