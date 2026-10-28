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
    /// An assessment's own decision: block when its stored risk.block is true, otherwise its recommendation. It is not the account's decision or held-admission state.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Decision : IEquatable<Decision>, IOpenEnum<string>
    {
        public static readonly Decision Allow = new Decision("allow");
        public static readonly Decision StepUp = new Decision("step_up");
        public static readonly Decision Review = new Decision("review");
        public static readonly Decision Block = new Decision("block");

        private static readonly Dictionary<string, Decision> _knownValues =
            new Dictionary<string, Decision>() {
                ["allow"] = Allow,
                ["step_up"] = StepUp,
                ["review"] = Review,
                ["block"] = Block
            };

        private static readonly ConcurrentDictionary<string, Decision> _values =
            new ConcurrentDictionary<string, Decision>(_knownValues);

        private Decision(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Decision Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Decision(value));
        }

        public static implicit operator Decision(string value) => Of(value);
        public static implicit operator string(Decision decision) => decision.Value;

        public static Decision[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Decision);

        public bool Equals(Decision? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}