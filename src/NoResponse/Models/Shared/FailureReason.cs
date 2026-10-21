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
    /// Why a signal or source did not produce a definite found/not_found. timed_out means the source missed the request deadline; a continuation may still settle it. deferred means the (source, identifier) pair is async-only and was not attempted on the synchronous path; a continuation settles it.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class FailureReason : IEquatable<FailureReason>, IOpenEnum<string>
    {
        public static readonly FailureReason TimedOut = new FailureReason("timed_out");
        public static readonly FailureReason RateLimited = new FailureReason("rate_limited");
        public static readonly FailureReason Blocked = new FailureReason("blocked");
        public static readonly FailureReason SourceError = new FailureReason("source_error");
        public static readonly FailureReason Unavailable = new FailureReason("unavailable");
        public static readonly FailureReason Deferred = new FailureReason("deferred");
        public static readonly FailureReason InsufficientEvidence = new FailureReason("insufficient_evidence");

        private static readonly Dictionary<string, FailureReason> _knownValues =
            new Dictionary<string, FailureReason>() {
                ["timed_out"] = TimedOut,
                ["rate_limited"] = RateLimited,
                ["blocked"] = Blocked,
                ["source_error"] = SourceError,
                ["unavailable"] = Unavailable,
                ["deferred"] = Deferred,
                ["insufficient_evidence"] = InsufficientEvidence
            };

        private static readonly ConcurrentDictionary<string, FailureReason> _values =
            new ConcurrentDictionary<string, FailureReason>(_knownValues);

        private FailureReason(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static FailureReason Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new FailureReason(value));
        }

        public static implicit operator FailureReason(string value) => Of(value);
        public static implicit operator string(FailureReason failurereason) => failurereason.Value;

        public static FailureReason[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as FailureReason);

        public bool Equals(FailureReason? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}