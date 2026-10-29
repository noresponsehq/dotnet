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
    public class ListChangeState : IEquatable<ListChangeState>, IOpenEnum<string>
    {
        public static readonly ListChangeState Propagating = new ListChangeState("propagating");
        public static readonly ListChangeState Completed = new ListChangeState("completed");

        private static readonly Dictionary<string, ListChangeState> _knownValues =
            new Dictionary<string, ListChangeState>() {
                ["propagating"] = Propagating,
                ["completed"] = Completed
            };

        private static readonly ConcurrentDictionary<string, ListChangeState> _values =
            new ConcurrentDictionary<string, ListChangeState>(_knownValues);

        private ListChangeState(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ListChangeState Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ListChangeState(value));
        }

        public static implicit operator ListChangeState(string value) => Of(value);
        public static implicit operator string(ListChangeState listchangestate) => listchangestate.Value;

        public static ListChangeState[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ListChangeState);

        public bool Equals(ListChangeState? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}