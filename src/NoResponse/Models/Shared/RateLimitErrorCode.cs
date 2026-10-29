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
    /// Machine codes an HTTP 429 answer can carry.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class RateLimitErrorCode : IEquatable<RateLimitErrorCode>, IOpenEnum<string>
    {
        public static readonly RateLimitErrorCode RateLimited = new RateLimitErrorCode("rate_limited");
        public static readonly RateLimitErrorCode AssessmentBudgetExhausted = new RateLimitErrorCode("assessment_budget_exhausted");
        public static readonly RateLimitErrorCode ExportLimitReached = new RateLimitErrorCode("export_limit_reached");

        private static readonly Dictionary<string, RateLimitErrorCode> _knownValues =
            new Dictionary<string, RateLimitErrorCode>() {
                ["rate_limited"] = RateLimited,
                ["assessment_budget_exhausted"] = AssessmentBudgetExhausted,
                ["export_limit_reached"] = ExportLimitReached
            };

        private static readonly ConcurrentDictionary<string, RateLimitErrorCode> _values =
            new ConcurrentDictionary<string, RateLimitErrorCode>(_knownValues);

        private RateLimitErrorCode(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static RateLimitErrorCode Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new RateLimitErrorCode(value));
        }

        public static implicit operator RateLimitErrorCode(string value) => Of(value);
        public static implicit operator string(RateLimitErrorCode ratelimiterrorcode) => ratelimiterrorcode.Value;

        public static RateLimitErrorCode[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as RateLimitErrorCode);

        public bool Equals(RateLimitErrorCode? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}