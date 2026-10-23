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
    /// delivered when the endpoint answered 2xx; failed otherwise, with failure naming why.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class WebhookDeliveryAttemptStatus : IEquatable<WebhookDeliveryAttemptStatus>, IOpenEnum<string>
    {
        public static readonly WebhookDeliveryAttemptStatus Delivered = new WebhookDeliveryAttemptStatus("delivered");
        public static readonly WebhookDeliveryAttemptStatus Failed = new WebhookDeliveryAttemptStatus("failed");

        private static readonly Dictionary<string, WebhookDeliveryAttemptStatus> _knownValues =
            new Dictionary<string, WebhookDeliveryAttemptStatus>() {
                ["delivered"] = Delivered,
                ["failed"] = Failed
            };

        private static readonly ConcurrentDictionary<string, WebhookDeliveryAttemptStatus> _values =
            new ConcurrentDictionary<string, WebhookDeliveryAttemptStatus>(_knownValues);

        private WebhookDeliveryAttemptStatus(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static WebhookDeliveryAttemptStatus Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new WebhookDeliveryAttemptStatus(value));
        }

        public static implicit operator WebhookDeliveryAttemptStatus(string value) => Of(value);
        public static implicit operator string(WebhookDeliveryAttemptStatus webhookdeliveryattemptstatus) => webhookdeliveryattemptstatus.Value;

        public static WebhookDeliveryAttemptStatus[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as WebhookDeliveryAttemptStatus);

        public bool Equals(WebhookDeliveryAttemptStatus? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}