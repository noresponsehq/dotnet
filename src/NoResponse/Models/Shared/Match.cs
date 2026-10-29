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
    /// exact compares the original submitted value; canonical uses identifier-specific normalisation; mailbox explicitly selects the email alias rules used by lists.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Match : IEquatable<Match>, IOpenEnum<string>
    {
        public static readonly Match Exact = new Match("exact");
        public static readonly Match Canonical = new Match("canonical");
        public static readonly Match Mailbox = new Match("mailbox");

        private static readonly Dictionary<string, Match> _knownValues =
            new Dictionary<string, Match>() {
                ["exact"] = Exact,
                ["canonical"] = Canonical,
                ["mailbox"] = Mailbox
            };

        private static readonly ConcurrentDictionary<string, Match> _values =
            new ConcurrentDictionary<string, Match>(_knownValues);

        private Match(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Match Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Match(value));
        }

        public static implicit operator Match(string value) => Of(value);
        public static implicit operator string(Match match) => match.Value;

        public static Match[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Match);

        public bool Equals(Match? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}