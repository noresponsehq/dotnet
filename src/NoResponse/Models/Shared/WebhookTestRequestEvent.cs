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
    /// endpoint.verification sends a fresh challenge now, in this call, and the endpoint is verified if it echoes it. assessment.updated queues an event whose data is a fixed example assessment, with test true, delivered and retried like any other; the endpoint must be verified.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class WebhookTestRequestEvent : IEquatable<WebhookTestRequestEvent>, IOpenEnum<string>
    {
        public static readonly WebhookTestRequestEvent EndpointVerification = new WebhookTestRequestEvent("endpoint.verification");
        public static readonly WebhookTestRequestEvent AssessmentUpdated = new WebhookTestRequestEvent("assessment.updated");

        private static readonly Dictionary<string, WebhookTestRequestEvent> _knownValues =
            new Dictionary<string, WebhookTestRequestEvent>() {
                ["endpoint.verification"] = EndpointVerification,
                ["assessment.updated"] = AssessmentUpdated
            };

        private static readonly ConcurrentDictionary<string, WebhookTestRequestEvent> _values =
            new ConcurrentDictionary<string, WebhookTestRequestEvent>(_knownValues);

        private WebhookTestRequestEvent(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static WebhookTestRequestEvent Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new WebhookTestRequestEvent(value));
        }

        public static implicit operator WebhookTestRequestEvent(string value) => Of(value);
        public static implicit operator string(WebhookTestRequestEvent webhooktestrequestevent) => webhooktestrequestevent.Value;

        public static WebhookTestRequestEvent[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as WebhookTestRequestEvent);

        public bool Equals(WebhookTestRequestEvent? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}