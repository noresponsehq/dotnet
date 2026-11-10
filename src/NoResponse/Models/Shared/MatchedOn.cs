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
    public class MatchedOn : IEquatable<MatchedOn>, IOpenEnum<string>
    {
        public static readonly MatchedOn Email = new MatchedOn("email");
        public static readonly MatchedOn Phone = new MatchedOn("phone");
        public static readonly MatchedOn ExternalId = new MatchedOn("external_id");

        private static readonly Dictionary<string, MatchedOn> _knownValues =
            new Dictionary<string, MatchedOn>() {
                ["email"] = Email,
                ["phone"] = Phone,
                ["external_id"] = ExternalId
            };

        private static readonly ConcurrentDictionary<string, MatchedOn> _values =
            new ConcurrentDictionary<string, MatchedOn>(_knownValues);

        private MatchedOn(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static MatchedOn Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new MatchedOn(value));
        }

        public static implicit operator MatchedOn(string value) => Of(value);
        public static implicit operator string(MatchedOn matchedon) => matchedon.Value;

        public static MatchedOn[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as MatchedOn);

        public bool Equals(MatchedOn? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}