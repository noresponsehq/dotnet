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
    /// found: a source found the identifier. not_found: every source answered and none found it. partial: the sources that answered found nothing and the others did not answer. not_covered: no source answered. A lookup that failed reads partial or not_covered, not not_found.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class ScreeningLookupCheckStatus : IEquatable<ScreeningLookupCheckStatus>, IOpenEnum<string>
    {
        public static readonly ScreeningLookupCheckStatus Found = new ScreeningLookupCheckStatus("found");
        public static readonly ScreeningLookupCheckStatus NotFound = new ScreeningLookupCheckStatus("not_found");
        public static readonly ScreeningLookupCheckStatus Partial = new ScreeningLookupCheckStatus("partial");
        public static readonly ScreeningLookupCheckStatus NotCovered = new ScreeningLookupCheckStatus("not_covered");

        private static readonly Dictionary<string, ScreeningLookupCheckStatus> _knownValues =
            new Dictionary<string, ScreeningLookupCheckStatus>() {
                ["found"] = Found,
                ["not_found"] = NotFound,
                ["partial"] = Partial,
                ["not_covered"] = NotCovered
            };

        private static readonly ConcurrentDictionary<string, ScreeningLookupCheckStatus> _values =
            new ConcurrentDictionary<string, ScreeningLookupCheckStatus>(_knownValues);

        private ScreeningLookupCheckStatus(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ScreeningLookupCheckStatus Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ScreeningLookupCheckStatus(value));
        }

        public static implicit operator ScreeningLookupCheckStatus(string value) => Of(value);
        public static implicit operator string(ScreeningLookupCheckStatus screeninglookupcheckstatus) => screeninglookupcheckstatus.Value;

        public static ScreeningLookupCheckStatus[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ScreeningLookupCheckStatus);

        public bool Equals(ScreeningLookupCheckStatus? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}