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
    /// How often the person is investigated again: daily, weekly or monthly, counted from when the schedule was set; off runs no scheduled investigation.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class SchedulePeriod : IEquatable<SchedulePeriod>, IOpenEnum<string>
    {
        public static readonly SchedulePeriod Daily = new SchedulePeriod("daily");
        public static readonly SchedulePeriod Weekly = new SchedulePeriod("weekly");
        public static readonly SchedulePeriod Monthly = new SchedulePeriod("monthly");
        public static readonly SchedulePeriod Off = new SchedulePeriod("off");

        private static readonly Dictionary<string, SchedulePeriod> _knownValues =
            new Dictionary<string, SchedulePeriod>() {
                ["daily"] = Daily,
                ["weekly"] = Weekly,
                ["monthly"] = Monthly,
                ["off"] = Off
            };

        private static readonly ConcurrentDictionary<string, SchedulePeriod> _values =
            new ConcurrentDictionary<string, SchedulePeriod>(_knownValues);

        private SchedulePeriod(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static SchedulePeriod Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new SchedulePeriod(value));
        }

        public static implicit operator SchedulePeriod(string value) => Of(value);
        public static implicit operator string(SchedulePeriod scheduleperiod) => scheduleperiod.Value;

        public static SchedulePeriod[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as SchedulePeriod);

        public bool Equals(SchedulePeriod? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}