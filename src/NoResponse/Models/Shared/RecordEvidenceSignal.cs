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
    /// found_by: what found the record, which sets where its likelihood starts. agrees: a value the record shares with what the investigator gave or with a joined record. clashes: a birth date, full name, gender or country that differs from theirs.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class RecordEvidenceSignal : IEquatable<RecordEvidenceSignal>, IOpenEnum<string>
    {
        public static readonly RecordEvidenceSignal FoundBy = new RecordEvidenceSignal("found_by");
        public static readonly RecordEvidenceSignal Agrees = new RecordEvidenceSignal("agrees");
        public static readonly RecordEvidenceSignal Clashes = new RecordEvidenceSignal("clashes");

        private static readonly Dictionary<string, RecordEvidenceSignal> _knownValues =
            new Dictionary<string, RecordEvidenceSignal>() {
                ["found_by"] = FoundBy,
                ["agrees"] = Agrees,
                ["clashes"] = Clashes
            };

        private static readonly ConcurrentDictionary<string, RecordEvidenceSignal> _values =
            new ConcurrentDictionary<string, RecordEvidenceSignal>(_knownValues);

        private RecordEvidenceSignal(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static RecordEvidenceSignal Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new RecordEvidenceSignal(value));
        }

        public static implicit operator RecordEvidenceSignal(string value) => Of(value);
        public static implicit operator string(RecordEvidenceSignal recordevidencesignal) => recordevidencesignal.Value;

        public static RecordEvidenceSignal[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as RecordEvidenceSignal);

        public bool Equals(RecordEvidenceSignal? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}