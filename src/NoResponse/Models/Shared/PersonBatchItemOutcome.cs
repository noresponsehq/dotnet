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
    /// unchanged: the row matched a person with the same details.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class PersonBatchItemOutcome : IEquatable<PersonBatchItemOutcome>, IOpenEnum<string>
    {
        public static readonly PersonBatchItemOutcome Created = new PersonBatchItemOutcome("created");
        public static readonly PersonBatchItemOutcome Updated = new PersonBatchItemOutcome("updated");
        public static readonly PersonBatchItemOutcome Unchanged = new PersonBatchItemOutcome("unchanged");

        private static readonly Dictionary<string, PersonBatchItemOutcome> _knownValues =
            new Dictionary<string, PersonBatchItemOutcome>() {
                ["created"] = Created,
                ["updated"] = Updated,
                ["unchanged"] = Unchanged
            };

        private static readonly ConcurrentDictionary<string, PersonBatchItemOutcome> _values =
            new ConcurrentDictionary<string, PersonBatchItemOutcome>(_knownValues);

        private PersonBatchItemOutcome(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static PersonBatchItemOutcome Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new PersonBatchItemOutcome(value));
        }

        public static implicit operator PersonBatchItemOutcome(string value) => Of(value);
        public static implicit operator string(PersonBatchItemOutcome personbatchitemoutcome) => personbatchitemoutcome.Value;

        public static PersonBatchItemOutcome[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as PersonBatchItemOutcome);

        public bool Equals(PersonBatchItemOutcome? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}