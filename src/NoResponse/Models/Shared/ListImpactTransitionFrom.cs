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
    public class ListImpactTransitionFrom : IEquatable<ListImpactTransitionFrom>, IOpenEnum<string>
    {
        public static readonly ListImpactTransitionFrom None = new ListImpactTransitionFrom("none");
        public static readonly ListImpactTransitionFrom Allow = new ListImpactTransitionFrom("allow");
        public static readonly ListImpactTransitionFrom Block = new ListImpactTransitionFrom("block");

        private static readonly Dictionary<string, ListImpactTransitionFrom> _knownValues =
            new Dictionary<string, ListImpactTransitionFrom>() {
                ["none"] = None,
                ["allow"] = Allow,
                ["block"] = Block
            };

        private static readonly ConcurrentDictionary<string, ListImpactTransitionFrom> _values =
            new ConcurrentDictionary<string, ListImpactTransitionFrom>(_knownValues);

        private ListImpactTransitionFrom(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ListImpactTransitionFrom Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ListImpactTransitionFrom(value));
        }

        public static implicit operator ListImpactTransitionFrom(string value) => Of(value);
        public static implicit operator string(ListImpactTransitionFrom listimpacttransitionfrom) => listimpacttransitionfrom.Value;

        public static ListImpactTransitionFrom[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ListImpactTransitionFrom);

        public bool Equals(ListImpactTransitionFrom? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}