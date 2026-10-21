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
    /// Why a lane did not settle. deferred means the lane is asynchronous and still running; a continuation settles it. timed_out means the lane missed its deadline. insufficient_evidence is a settled inconclusive signal rather than an incomplete lane.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class IncompleteReason : IEquatable<IncompleteReason>, IOpenEnum<string>
    {
        public static readonly IncompleteReason TimedOut = new IncompleteReason("timed_out");
        public static readonly IncompleteReason RateLimited = new IncompleteReason("rate_limited");
        public static readonly IncompleteReason Blocked = new IncompleteReason("blocked");
        public static readonly IncompleteReason SourceError = new IncompleteReason("source_error");
        public static readonly IncompleteReason Unavailable = new IncompleteReason("unavailable");
        public static readonly IncompleteReason Deferred = new IncompleteReason("deferred");

        private static readonly Dictionary<string, IncompleteReason> _knownValues =
            new Dictionary<string, IncompleteReason>() {
                ["timed_out"] = TimedOut,
                ["rate_limited"] = RateLimited,
                ["blocked"] = Blocked,
                ["source_error"] = SourceError,
                ["unavailable"] = Unavailable,
                ["deferred"] = Deferred
            };

        private static readonly ConcurrentDictionary<string, IncompleteReason> _values =
            new ConcurrentDictionary<string, IncompleteReason>(_knownValues);

        private IncompleteReason(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static IncompleteReason Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new IncompleteReason(value));
        }

        public static implicit operator IncompleteReason(string value) => Of(value);
        public static implicit operator string(IncompleteReason incompletereason) => incompletereason.Value;

        public static IncompleteReason[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as IncompleteReason);

        public bool Equals(IncompleteReason? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}