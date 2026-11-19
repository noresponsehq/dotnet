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
    public class LastError : IEquatable<LastError>, IOpenEnum<string>
    {
        public static readonly LastError ScreeningCapExceeded = new LastError("screening_cap_exceeded");
        public static readonly LastError ScreeningUnavailable = new LastError("screening_unavailable");
        public static readonly LastError PersonNotFound = new LastError("person_not_found");
        public static readonly LastError IdempotencyKeyReused = new LastError("idempotency_key_reused");

        private static readonly Dictionary<string, LastError> _knownValues =
            new Dictionary<string, LastError>() {
                ["screening_cap_exceeded"] = ScreeningCapExceeded,
                ["screening_unavailable"] = ScreeningUnavailable,
                ["person_not_found"] = PersonNotFound,
                ["idempotency_key_reused"] = IdempotencyKeyReused
            };

        private static readonly ConcurrentDictionary<string, LastError> _values =
            new ConcurrentDictionary<string, LastError>(_knownValues);

        private LastError(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static LastError Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new LastError(value));
        }

        public static implicit operator LastError(string value) => Of(value);
        public static implicit operator string(LastError lasterror) => lasterror.Value;

        public static LastError[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as LastError);

        public bool Equals(LastError? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}