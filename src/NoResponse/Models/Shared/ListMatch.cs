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
    /// The action of the list entry that decided the account.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class ListMatch : IEquatable<ListMatch>, IOpenEnum<string>
    {
        public static readonly ListMatch Block = new ListMatch("block");
        public static readonly ListMatch Allow = new ListMatch("allow");

        private static readonly Dictionary<string, ListMatch> _knownValues =
            new Dictionary<string, ListMatch>() {
                ["block"] = Block,
                ["allow"] = Allow
            };

        private static readonly ConcurrentDictionary<string, ListMatch> _values =
            new ConcurrentDictionary<string, ListMatch>(_knownValues);

        private ListMatch(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ListMatch Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ListMatch(value));
        }

        public static implicit operator ListMatch(string value) => Of(value);
        public static implicit operator string(ListMatch listmatch) => listmatch.Value;

        public static ListMatch[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ListMatch);

        public bool Equals(ListMatch? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}