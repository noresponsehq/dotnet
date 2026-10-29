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
    /// Customer-submitted input. It does not mean the customer or NoResponse verified ownership.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Provenance : IEquatable<Provenance>, IOpenEnum<string>
    {
        public static readonly Provenance Submitted = new Provenance("submitted");

        private static readonly Dictionary<string, Provenance> _knownValues =
            new Dictionary<string, Provenance>() {
                ["submitted"] = Submitted
            };

        private static readonly ConcurrentDictionary<string, Provenance> _values =
            new ConcurrentDictionary<string, Provenance>(_knownValues);

        private Provenance(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Provenance Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Provenance(value));
        }

        public static implicit operator Provenance(string value) => Of(value);
        public static implicit operator string(Provenance provenance) => provenance.Value;

        public static Provenance[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Provenance);

        public bool Equals(Provenance? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}