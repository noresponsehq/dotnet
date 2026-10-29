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
    public class RequestAggregateRequestBucket : IEquatable<RequestAggregateRequestBucket>, IOpenEnum<string>
    {
        public static readonly RequestAggregateRequestBucket Hour = new RequestAggregateRequestBucket("hour");
        public static readonly RequestAggregateRequestBucket Day = new RequestAggregateRequestBucket("day");

        private static readonly Dictionary<string, RequestAggregateRequestBucket> _knownValues =
            new Dictionary<string, RequestAggregateRequestBucket>() {
                ["hour"] = Hour,
                ["day"] = Day
            };

        private static readonly ConcurrentDictionary<string, RequestAggregateRequestBucket> _values =
            new ConcurrentDictionary<string, RequestAggregateRequestBucket>(_knownValues);

        private RequestAggregateRequestBucket(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static RequestAggregateRequestBucket Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new RequestAggregateRequestBucket(value));
        }

        public static implicit operator RequestAggregateRequestBucket(string value) => Of(value);
        public static implicit operator string(RequestAggregateRequestBucket requestaggregaterequestbucket) => requestaggregaterequestbucket.Value;

        public static RequestAggregateRequestBucket[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as RequestAggregateRequestBucket);

        public bool Equals(RequestAggregateRequestBucket? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}