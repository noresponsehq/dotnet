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
    /// unchanged: the row matched an attendee with the same details.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class AttendeeBatchItemOutcome : IEquatable<AttendeeBatchItemOutcome>, IOpenEnum<string>
    {
        public static readonly AttendeeBatchItemOutcome Created = new AttendeeBatchItemOutcome("created");
        public static readonly AttendeeBatchItemOutcome Updated = new AttendeeBatchItemOutcome("updated");
        public static readonly AttendeeBatchItemOutcome Unchanged = new AttendeeBatchItemOutcome("unchanged");

        private static readonly Dictionary<string, AttendeeBatchItemOutcome> _knownValues =
            new Dictionary<string, AttendeeBatchItemOutcome>() {
                ["created"] = Created,
                ["updated"] = Updated,
                ["unchanged"] = Unchanged
            };

        private static readonly ConcurrentDictionary<string, AttendeeBatchItemOutcome> _values =
            new ConcurrentDictionary<string, AttendeeBatchItemOutcome>(_knownValues);

        private AttendeeBatchItemOutcome(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static AttendeeBatchItemOutcome Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new AttendeeBatchItemOutcome(value));
        }

        public static implicit operator AttendeeBatchItemOutcome(string value) => Of(value);
        public static implicit operator string(AttendeeBatchItemOutcome attendeebatchitemoutcome) => attendeebatchitemoutcome.Value;

        public static AttendeeBatchItemOutcome[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as AttendeeBatchItemOutcome);

        public bool Equals(AttendeeBatchItemOutcome? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}