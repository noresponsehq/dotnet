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
    /// Why a failed search found nothing: its sources did not answer, none could be asked, or it could not run before its attempts were spent.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class ErrorCode : IEquatable<ErrorCode>, IOpenEnum<string>
    {
        public static readonly ErrorCode NotAnswered = new ErrorCode("not_answered");
        public static readonly ErrorCode NoSource = new ErrorCode("no_source");
        public static readonly ErrorCode WorkerAttemptsExhausted = new ErrorCode("worker_attempts_exhausted");

        private static readonly Dictionary<string, ErrorCode> _knownValues =
            new Dictionary<string, ErrorCode>() {
                ["not_answered"] = NotAnswered,
                ["no_source"] = NoSource,
                ["worker_attempts_exhausted"] = WorkerAttemptsExhausted
            };

        private static readonly ConcurrentDictionary<string, ErrorCode> _values =
            new ConcurrentDictionary<string, ErrorCode>(_knownValues);

        private ErrorCode(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ErrorCode Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ErrorCode(value));
        }

        public static implicit operator ErrorCode(string value) => Of(value);
        public static implicit operator string(ErrorCode errorcode) => errorcode.Value;

        public static ErrorCode[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ErrorCode);

        public bool Equals(ErrorCode? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}