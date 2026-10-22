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
    /// unverified until the endpoint echoes an endpoint.verification challenge (send one with POST /v1/webhooks/{endpoint_id}/test); verified receives assessment.updated events; disabled after 72 hours of continuous delivery failure, and verified again by the same challenge.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class WebhookEndpointState : IEquatable<WebhookEndpointState>, IOpenEnum<string>
    {
        public static readonly WebhookEndpointState Unverified = new WebhookEndpointState("unverified");
        public static readonly WebhookEndpointState Verified = new WebhookEndpointState("verified");
        public static readonly WebhookEndpointState Disabled = new WebhookEndpointState("disabled");

        private static readonly Dictionary<string, WebhookEndpointState> _knownValues =
            new Dictionary<string, WebhookEndpointState>() {
                ["unverified"] = Unverified,
                ["verified"] = Verified,
                ["disabled"] = Disabled
            };

        private static readonly ConcurrentDictionary<string, WebhookEndpointState> _values =
            new ConcurrentDictionary<string, WebhookEndpointState>(_knownValues);

        private WebhookEndpointState(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static WebhookEndpointState Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new WebhookEndpointState(value));
        }

        public static implicit operator WebhookEndpointState(string value) => Of(value);
        public static implicit operator string(WebhookEndpointState webhookendpointstate) => webhookendpointstate.Value;

        public static WebhookEndpointState[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as WebhookEndpointState);

        public bool Equals(WebhookEndpointState? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}