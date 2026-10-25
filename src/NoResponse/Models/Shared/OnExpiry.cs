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
    /// The decision a held assessment takes when hold_seconds pass before its investigation completes, unless the evidence so far gives a stricter one (default review). It is announced as account.risk_changed like any other change, so an expired hold is always announced. A hosted provider hook, which cannot wait, answers a held account by it at once: deny for block, allow otherwise. Kept while the flow is immediate.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class OnExpiry : IEquatable<OnExpiry>, IOpenEnum<string>
    {
        public static readonly OnExpiry Allow = new OnExpiry("allow");
        public static readonly OnExpiry Review = new OnExpiry("review");
        public static readonly OnExpiry Block = new OnExpiry("block");

        private static readonly Dictionary<string, OnExpiry> _knownValues =
            new Dictionary<string, OnExpiry>() {
                ["allow"] = Allow,
                ["review"] = Review,
                ["block"] = Block
            };

        private static readonly ConcurrentDictionary<string, OnExpiry> _values =
            new ConcurrentDictionary<string, OnExpiry>(_knownValues);

        private OnExpiry(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static OnExpiry Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new OnExpiry(value));
        }

        public static implicit operator OnExpiry(string value) => Of(value);
        public static implicit operator string(OnExpiry onexpiry) => onexpiry.Value;

        public static OnExpiry[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as OnExpiry);

        public bool Equals(OnExpiry? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}