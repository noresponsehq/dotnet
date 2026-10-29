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
    /// Processing state, separate from evidence coverage. complete: final; nothing is still running, but unsupported, failed or unanswered lanes remain in incomplete_sources. partial: a recommendation exists and evidence is incomplete; assessment_url identifies continuing work when present. pending: no recommendation yet; poll or wait for the webhook. Finished processing does not mean all sources answered.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Status : IEquatable<Status>, IOpenEnum<string>
    {
        public static readonly Status Complete = new Status("complete");
        public static readonly Status Partial = new Status("partial");
        public static readonly Status Pending = new Status("pending");

        private static readonly Dictionary<string, Status> _knownValues =
            new Dictionary<string, Status>() {
                ["complete"] = Complete,
                ["partial"] = Partial,
                ["pending"] = Pending
            };

        private static readonly ConcurrentDictionary<string, Status> _values =
            new ConcurrentDictionary<string, Status>(_knownValues);

        private Status(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Status Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Status(value));
        }

        public static implicit operator Status(string value) => Of(value);
        public static implicit operator string(Status status) => status.Value;

        public static Status[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Status);

        public bool Equals(Status? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}