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
    /// Per-signal outcome. A source that timed out, was blocked or errored reports blocked, unavailable or inconclusive with a reason and is listed in incomplete_sources; not_found is a definite absence.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class SignalOutcome : IEquatable<SignalOutcome>, IOpenEnum<string>
    {
        public static readonly SignalOutcome Found = new SignalOutcome("found");
        public static readonly SignalOutcome NotFound = new SignalOutcome("not_found");
        public static readonly SignalOutcome Inconclusive = new SignalOutcome("inconclusive");
        public static readonly SignalOutcome Blocked = new SignalOutcome("blocked");
        public static readonly SignalOutcome Unavailable = new SignalOutcome("unavailable");

        private static readonly Dictionary<string, SignalOutcome> _knownValues =
            new Dictionary<string, SignalOutcome>() {
                ["found"] = Found,
                ["not_found"] = NotFound,
                ["inconclusive"] = Inconclusive,
                ["blocked"] = Blocked,
                ["unavailable"] = Unavailable
            };

        private static readonly ConcurrentDictionary<string, SignalOutcome> _values =
            new ConcurrentDictionary<string, SignalOutcome>(_knownValues);

        private SignalOutcome(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static SignalOutcome Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new SignalOutcome(value));
        }

        public static implicit operator SignalOutcome(string value) => Of(value);
        public static implicit operator string(SignalOutcome signaloutcome) => signaloutcome.Value;

        public static SignalOutcome[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as SignalOutcome);

        public bool Equals(SignalOutcome? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}