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
    /// assessments: assessments admitted. webhook_deliveries: events sent to endpoints, one per event per endpoint. key_usage_events: key-usage events accepted.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Metric : IEquatable<Metric>, IOpenEnum<string>
    {
        public static readonly Metric Assessments = new Metric("assessments");
        public static readonly Metric WebhookDeliveries = new Metric("webhook_deliveries");
        public static readonly Metric KeyUsageEvents = new Metric("key_usage_events");

        private static readonly Dictionary<string, Metric> _knownValues =
            new Dictionary<string, Metric>() {
                ["assessments"] = Assessments,
                ["webhook_deliveries"] = WebhookDeliveries,
                ["key_usage_events"] = KeyUsageEvents
            };

        private static readonly ConcurrentDictionary<string, Metric> _values =
            new ConcurrentDictionary<string, Metric>(_knownValues);

        private Metric(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Metric Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Metric(value));
        }

        public static implicit operator Metric(string value) => Of(value);
        public static implicit operator string(Metric metric) => metric.Value;

        public static Metric[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Metric);

        public bool Equals(Metric? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}