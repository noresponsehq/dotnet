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
    /// Machine codes an HTTP 400 answer can carry.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class BadRequestErrorCode : IEquatable<BadRequestErrorCode>, IOpenEnum<string>
    {
        public static readonly BadRequestErrorCode InvalidRequest = new BadRequestErrorCode("invalid_request");
        public static readonly BadRequestErrorCode InvalidIdempotencyKey = new BadRequestErrorCode("invalid_idempotency_key");
        public static readonly BadRequestErrorCode InvalidCursor = new BadRequestErrorCode("invalid_cursor");

        private static readonly Dictionary<string, BadRequestErrorCode> _knownValues =
            new Dictionary<string, BadRequestErrorCode>() {
                ["invalid_request"] = InvalidRequest,
                ["invalid_idempotency_key"] = InvalidIdempotencyKey,
                ["invalid_cursor"] = InvalidCursor
            };

        private static readonly ConcurrentDictionary<string, BadRequestErrorCode> _values =
            new ConcurrentDictionary<string, BadRequestErrorCode>(_knownValues);

        private BadRequestErrorCode(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static BadRequestErrorCode Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new BadRequestErrorCode(value));
        }

        public static implicit operator BadRequestErrorCode(string value) => Of(value);
        public static implicit operator string(BadRequestErrorCode badrequesterrorcode) => badrequesterrorcode.Value;

        public static BadRequestErrorCode[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as BadRequestErrorCode);

        public bool Equals(BadRequestErrorCode? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}