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
    /// decision, recommendation and band: the first answer's. flow: the assessment's. list_match: the action of the list entry that decided the account when it was first answered. reason: a reason the first answer fired, with its impact band. lane: an evidence lane the first answer listed in incomplete_sources. country and asn: the submitted IP's, from the IP data.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class AggregateDimension : IEquatable<AggregateDimension>, IOpenEnum<string>
    {
        public static readonly AggregateDimension Decision = new AggregateDimension("decision");
        public static readonly AggregateDimension Flow = new AggregateDimension("flow");
        public static readonly AggregateDimension Recommendation = new AggregateDimension("recommendation");
        public static readonly AggregateDimension Band = new AggregateDimension("band");
        public static readonly AggregateDimension Reason = new AggregateDimension("reason");
        public static readonly AggregateDimension Lane = new AggregateDimension("lane");
        public static readonly AggregateDimension Country = new AggregateDimension("country");
        public static readonly AggregateDimension Asn = new AggregateDimension("asn");
        public static readonly AggregateDimension ListMatch = new AggregateDimension("list_match");

        private static readonly Dictionary<string, AggregateDimension> _knownValues =
            new Dictionary<string, AggregateDimension>() {
                ["decision"] = Decision,
                ["flow"] = Flow,
                ["recommendation"] = Recommendation,
                ["band"] = Band,
                ["reason"] = Reason,
                ["lane"] = Lane,
                ["country"] = Country,
                ["asn"] = Asn,
                ["list_match"] = ListMatch
            };

        private static readonly ConcurrentDictionary<string, AggregateDimension> _values =
            new ConcurrentDictionary<string, AggregateDimension>(_knownValues);

        private AggregateDimension(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static AggregateDimension Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new AggregateDimension(value));
        }

        public static implicit operator AggregateDimension(string value) => Of(value);
        public static implicit operator string(AggregateDimension aggregatedimension) => aggregatedimension.Value;

        public static AggregateDimension[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as AggregateDimension);

        public bool Equals(AggregateDimension? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}