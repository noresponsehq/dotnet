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
    /// How sure the latest screening is that it found the attendee's accounts. unscreened until a screening reaches the attendee.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class MatchState : IEquatable<MatchState>, IOpenEnum<string>
    {
        public static readonly MatchState Unscreened = new MatchState("unscreened");
        public static readonly MatchState Confirmed = new MatchState("confirmed");
        public static readonly MatchState Probable = new MatchState("probable");
        public static readonly MatchState Possible = new MatchState("possible");
        public static readonly MatchState NoneFound = new MatchState("none_found");
        public static readonly MatchState Inaccessible = new MatchState("inaccessible");

        private static readonly Dictionary<string, MatchState> _knownValues =
            new Dictionary<string, MatchState>() {
                ["unscreened"] = Unscreened,
                ["confirmed"] = Confirmed,
                ["probable"] = Probable,
                ["possible"] = Possible,
                ["none_found"] = NoneFound,
                ["inaccessible"] = Inaccessible
            };

        private static readonly ConcurrentDictionary<string, MatchState> _values =
            new ConcurrentDictionary<string, MatchState>(_knownValues);

        private MatchState(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static MatchState Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new MatchState(value));
        }

        public static implicit operator MatchState(string value) => Of(value);
        public static implicit operator string(MatchState matchstate) => matchstate.Value;

        public static MatchState[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as MatchState);

        public bool Equals(MatchState? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}