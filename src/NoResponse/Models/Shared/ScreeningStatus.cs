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
    /// partial finished with some people or platforms not covered; people_failed and people_partial count them.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class ScreeningStatus : IEquatable<ScreeningStatus>, IOpenEnum<string>
    {
        public static readonly ScreeningStatus Queued = new ScreeningStatus("queued");
        public static readonly ScreeningStatus Running = new ScreeningStatus("running");
        public static readonly ScreeningStatus Completed = new ScreeningStatus("completed");
        public static readonly ScreeningStatus Partial = new ScreeningStatus("partial");
        public static readonly ScreeningStatus Failed = new ScreeningStatus("failed");
        public static readonly ScreeningStatus Cancelled = new ScreeningStatus("cancelled");

        private static readonly Dictionary<string, ScreeningStatus> _knownValues =
            new Dictionary<string, ScreeningStatus>() {
                ["queued"] = Queued,
                ["running"] = Running,
                ["completed"] = Completed,
                ["partial"] = Partial,
                ["failed"] = Failed,
                ["cancelled"] = Cancelled
            };

        private static readonly ConcurrentDictionary<string, ScreeningStatus> _values =
            new ConcurrentDictionary<string, ScreeningStatus>(_knownValues);

        private ScreeningStatus(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ScreeningStatus Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ScreeningStatus(value));
        }

        public static implicit operator ScreeningStatus(string value) => Of(value);
        public static implicit operator string(ScreeningStatus screeningstatus) => screeningstatus.Value;

        public static ScreeningStatus[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ScreeningStatus);

        public bool Equals(ScreeningStatus? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}