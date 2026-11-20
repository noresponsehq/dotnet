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
    /// How sure the screening is that a profile is the person's, from the likelihood its record scores. Confirmed reaches the join bar of 0.9. Probable reaches 0.85, which takes a shared value beyond the one that found the account. Possible is the rest.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class ProfileMatchState : IEquatable<ProfileMatchState>, IOpenEnum<string>
    {
        public static readonly ProfileMatchState Confirmed = new ProfileMatchState("confirmed");
        public static readonly ProfileMatchState Probable = new ProfileMatchState("probable");
        public static readonly ProfileMatchState Possible = new ProfileMatchState("possible");

        private static readonly Dictionary<string, ProfileMatchState> _knownValues =
            new Dictionary<string, ProfileMatchState>() {
                ["confirmed"] = Confirmed,
                ["probable"] = Probable,
                ["possible"] = Possible
            };

        private static readonly ConcurrentDictionary<string, ProfileMatchState> _values =
            new ConcurrentDictionary<string, ProfileMatchState>(_knownValues);

        private ProfileMatchState(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ProfileMatchState Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ProfileMatchState(value));
        }

        public static implicit operator ProfileMatchState(string value) => Of(value);
        public static implicit operator string(ProfileMatchState profilematchstate) => profilematchstate.Value;

        public static ProfileMatchState[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ProfileMatchState);

        public bool Equals(ProfileMatchState? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}