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
    public class OtherGroupMatchedOn : IEquatable<OtherGroupMatchedOn>, IOpenEnum<string>
    {
        public static readonly OtherGroupMatchedOn PersonId = new OtherGroupMatchedOn("person_id");

        private static readonly Dictionary<string, OtherGroupMatchedOn> _knownValues =
            new Dictionary<string, OtherGroupMatchedOn>() {
                ["person_id"] = PersonId
            };

        private static readonly ConcurrentDictionary<string, OtherGroupMatchedOn> _values =
            new ConcurrentDictionary<string, OtherGroupMatchedOn>(_knownValues);

        private OtherGroupMatchedOn(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static OtherGroupMatchedOn Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new OtherGroupMatchedOn(value));
        }

        public static implicit operator OtherGroupMatchedOn(string value) => Of(value);
        public static implicit operator string(OtherGroupMatchedOn othergroupmatchedon) => othergroupmatchedon.Value;

        public static OtherGroupMatchedOn[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as OtherGroupMatchedOn);

        public bool Equals(OtherGroupMatchedOn? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}