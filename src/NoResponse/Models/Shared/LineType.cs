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
    public class LineType : IEquatable<LineType>, IOpenEnum<string>
    {
        public static readonly LineType Mobile = new LineType("mobile");
        public static readonly LineType Voip = new LineType("voip");
        public static readonly LineType Landline = new LineType("landline");
        public static readonly LineType TollFree = new LineType("toll_free");
        public static readonly LineType Other = new LineType("other");

        private static readonly Dictionary<string, LineType> _knownValues =
            new Dictionary<string, LineType>() {
                ["mobile"] = Mobile,
                ["voip"] = Voip,
                ["landline"] = Landline,
                ["toll_free"] = TollFree,
                ["other"] = Other
            };

        private static readonly ConcurrentDictionary<string, LineType> _values =
            new ConcurrentDictionary<string, LineType>(_knownValues);

        private LineType(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static LineType Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new LineType(value));
        }

        public static implicit operator LineType(string value) => Of(value);
        public static implicit operator string(LineType linetype) => linetype.Value;

        public static LineType[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as LineType);

        public bool Equals(LineType? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}