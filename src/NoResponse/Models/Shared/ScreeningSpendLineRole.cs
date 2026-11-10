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
    public class ScreeningSpendLineRole : IEquatable<ScreeningSpendLineRole>, IOpenEnum<string>
    {
        public static readonly ScreeningSpendLineRole Resolver = new ScreeningSpendLineRole("resolver");
        public static readonly ScreeningSpendLineRole Collector = new ScreeningSpendLineRole("collector");
        public static readonly ScreeningSpendLineRole Analyser = new ScreeningSpendLineRole("analyser");

        private static readonly Dictionary<string, ScreeningSpendLineRole> _knownValues =
            new Dictionary<string, ScreeningSpendLineRole>() {
                ["resolver"] = Resolver,
                ["collector"] = Collector,
                ["analyser"] = Analyser
            };

        private static readonly ConcurrentDictionary<string, ScreeningSpendLineRole> _values =
            new ConcurrentDictionary<string, ScreeningSpendLineRole>(_knownValues);

        private ScreeningSpendLineRole(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ScreeningSpendLineRole Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ScreeningSpendLineRole(value));
        }

        public static implicit operator ScreeningSpendLineRole(string value) => Of(value);
        public static implicit operator string(ScreeningSpendLineRole screeningspendlinerole) => screeningspendlinerole.Value;

        public static ScreeningSpendLineRole[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ScreeningSpendLineRole);

        public bool Equals(ScreeningSpendLineRole? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}