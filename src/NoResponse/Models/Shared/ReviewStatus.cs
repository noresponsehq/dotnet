#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    [JsonConverter(typeof(OpenEnumConverter))]
    public class ReviewStatus : IEquatable<ReviewStatus>, IOpenEnum<string>
    {
        public static readonly ReviewStatus Open = new ReviewStatus("open");
        public static readonly ReviewStatus InProgress = new ReviewStatus("in_progress");
        public static readonly ReviewStatus Resolved = new ReviewStatus("resolved");

        private static readonly Dictionary<string, ReviewStatus> _knownValues =
            new Dictionary<string, ReviewStatus>() {
                ["open"] = Open,
                ["in_progress"] = InProgress,
                ["resolved"] = Resolved
            };

        private static readonly ConcurrentDictionary<string, ReviewStatus> _values =
            new ConcurrentDictionary<string, ReviewStatus>(_knownValues);

        private ReviewStatus(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ReviewStatus Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ReviewStatus(value));
        }

        public static implicit operator ReviewStatus(string value) => Of(value);
        public static implicit operator string(ReviewStatus reviewstatus) => reviewstatus.Value;

        public static ReviewStatus[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ReviewStatus);

        public bool Equals(ReviewStatus? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}