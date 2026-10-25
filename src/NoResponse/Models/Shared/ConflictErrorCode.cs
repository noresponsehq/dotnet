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
    /// Machine codes an HTTP 409 answer can carry.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class ConflictErrorCode : IEquatable<ConflictErrorCode>, IOpenEnum<string>
    {
        public static readonly ConflictErrorCode IdempotencyKeyReused = new ConflictErrorCode("idempotency_key_reused");
        public static readonly ConflictErrorCode KeyRotationInProgress = new ConflictErrorCode("key_rotation_in_progress");
        public static readonly ConflictErrorCode WebhookEndpointLimitReached = new ConflictErrorCode("webhook_endpoint_limit_reached");
        public static readonly ConflictErrorCode WebhookEndpointUnverified = new ConflictErrorCode("webhook_endpoint_unverified");
        public static readonly ConflictErrorCode TooManyIntegrations = new ConflictErrorCode("too_many_integrations");
        public static readonly ConflictErrorCode RiskVersionAhead = new ConflictErrorCode("risk_version_ahead");
        public static readonly ConflictErrorCode KeyLimitReached = new ConflictErrorCode("key_limit_reached");
        public static readonly ConflictErrorCode SelfRevocation = new ConflictErrorCode("self_revocation");

        private static readonly Dictionary<string, ConflictErrorCode> _knownValues =
            new Dictionary<string, ConflictErrorCode>() {
                ["idempotency_key_reused"] = IdempotencyKeyReused,
                ["key_rotation_in_progress"] = KeyRotationInProgress,
                ["webhook_endpoint_limit_reached"] = WebhookEndpointLimitReached,
                ["webhook_endpoint_unverified"] = WebhookEndpointUnverified,
                ["too_many_integrations"] = TooManyIntegrations,
                ["risk_version_ahead"] = RiskVersionAhead,
                ["key_limit_reached"] = KeyLimitReached,
                ["self_revocation"] = SelfRevocation
            };

        private static readonly ConcurrentDictionary<string, ConflictErrorCode> _values =
            new ConcurrentDictionary<string, ConflictErrorCode>(_knownValues);

        private ConflictErrorCode(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ConflictErrorCode Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ConflictErrorCode(value));
        }

        public static implicit operator ConflictErrorCode(string value) => Of(value);
        public static implicit operator string(ConflictErrorCode conflicterrorcode) => conflicterrorcode.Value;

        public static ConflictErrorCode[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ConflictErrorCode);

        public bool Equals(ConflictErrorCode? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}