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
    /// Present only when status is failed. no_writer: no profile writer serves this environment. refused: the model refused. not_written: the answer was not a profile, or no statement survived grounding. uncited_statement: a statement or lead cited a record the person does not hold. not_answered: the writer did not answer. worker_attempts_exhausted: the worker stopped retrying.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class WrittenProfileErrorCode : IEquatable<WrittenProfileErrorCode>, IOpenEnum<string>
    {
        public static readonly WrittenProfileErrorCode NoWriter = new WrittenProfileErrorCode("no_writer");
        public static readonly WrittenProfileErrorCode Refused = new WrittenProfileErrorCode("refused");
        public static readonly WrittenProfileErrorCode NotWritten = new WrittenProfileErrorCode("not_written");
        public static readonly WrittenProfileErrorCode UncitedStatement = new WrittenProfileErrorCode("uncited_statement");
        public static readonly WrittenProfileErrorCode NotAnswered = new WrittenProfileErrorCode("not_answered");
        public static readonly WrittenProfileErrorCode WorkerAttemptsExhausted = new WrittenProfileErrorCode("worker_attempts_exhausted");

        private static readonly Dictionary<string, WrittenProfileErrorCode> _knownValues =
            new Dictionary<string, WrittenProfileErrorCode>() {
                ["no_writer"] = NoWriter,
                ["refused"] = Refused,
                ["not_written"] = NotWritten,
                ["uncited_statement"] = UncitedStatement,
                ["not_answered"] = NotAnswered,
                ["worker_attempts_exhausted"] = WorkerAttemptsExhausted
            };

        private static readonly ConcurrentDictionary<string, WrittenProfileErrorCode> _values =
            new ConcurrentDictionary<string, WrittenProfileErrorCode>(_knownValues);

        private WrittenProfileErrorCode(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static WrittenProfileErrorCode Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new WrittenProfileErrorCode(value));
        }

        public static implicit operator WrittenProfileErrorCode(string value) => Of(value);
        public static implicit operator string(WrittenProfileErrorCode writtenprofileerrorcode) => writtenprofileerrorcode.Value;

        public static WrittenProfileErrorCode[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as WrittenProfileErrorCode);

        public bool Equals(WrittenProfileErrorCode? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}