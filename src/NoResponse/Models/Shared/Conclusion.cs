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
    public class Conclusion : IEquatable<Conclusion>, IOpenEnum<string>
    {
        public static readonly Conclusion Benign = new Conclusion("benign");
        public static readonly Conclusion Abuse = new Conclusion("abuse");
        public static readonly Conclusion Inconclusive = new Conclusion("inconclusive");

        private static readonly Dictionary<string, Conclusion> _knownValues =
            new Dictionary<string, Conclusion>() {
                ["benign"] = Benign,
                ["abuse"] = Abuse,
                ["inconclusive"] = Inconclusive
            };

        private static readonly ConcurrentDictionary<string, Conclusion> _values =
            new ConcurrentDictionary<string, Conclusion>(_knownValues);

        private Conclusion(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Conclusion Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Conclusion(value));
        }

        public static implicit operator Conclusion(string value) => Of(value);
        public static implicit operator string(Conclusion conclusion) => conclusion.Value;

        public static Conclusion[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Conclusion);

        public bool Equals(Conclusion? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}