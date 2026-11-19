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
    /// anchors_disagree: records found by two of the identifiers you gave clash with each other, so those identifiers may belong to different people.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class RecordConflictSignal : IEquatable<RecordConflictSignal>, IOpenEnum<string>
    {
        public static readonly RecordConflictSignal AnchorsDisagree = new RecordConflictSignal("anchors_disagree");

        private static readonly Dictionary<string, RecordConflictSignal> _knownValues =
            new Dictionary<string, RecordConflictSignal>() {
                ["anchors_disagree"] = AnchorsDisagree
            };

        private static readonly ConcurrentDictionary<string, RecordConflictSignal> _values =
            new ConcurrentDictionary<string, RecordConflictSignal>(_knownValues);

        private RecordConflictSignal(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static RecordConflictSignal Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new RecordConflictSignal(value));
        }

        public static implicit operator RecordConflictSignal(string value) => Of(value);
        public static implicit operator string(RecordConflictSignal recordconflictsignal) => recordconflictsignal.Value;

        public static RecordConflictSignal[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as RecordConflictSignal);

        public bool Equals(RecordConflictSignal? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}