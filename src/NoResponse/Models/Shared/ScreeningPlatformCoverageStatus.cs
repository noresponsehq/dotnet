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
    /// covered read at least one profile; none_found looked and found no account; inaccessible found accounts that are private or locked; failed could not finish looking; unavailable has no resolver or collector enabled.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class ScreeningPlatformCoverageStatus : IEquatable<ScreeningPlatformCoverageStatus>, IOpenEnum<string>
    {
        public static readonly ScreeningPlatformCoverageStatus Covered = new ScreeningPlatformCoverageStatus("covered");
        public static readonly ScreeningPlatformCoverageStatus NoneFound = new ScreeningPlatformCoverageStatus("none_found");
        public static readonly ScreeningPlatformCoverageStatus Inaccessible = new ScreeningPlatformCoverageStatus("inaccessible");
        public static readonly ScreeningPlatformCoverageStatus Failed = new ScreeningPlatformCoverageStatus("failed");
        public static readonly ScreeningPlatformCoverageStatus Unavailable = new ScreeningPlatformCoverageStatus("unavailable");

        private static readonly Dictionary<string, ScreeningPlatformCoverageStatus> _knownValues =
            new Dictionary<string, ScreeningPlatformCoverageStatus>() {
                ["covered"] = Covered,
                ["none_found"] = NoneFound,
                ["inaccessible"] = Inaccessible,
                ["failed"] = Failed,
                ["unavailable"] = Unavailable
            };

        private static readonly ConcurrentDictionary<string, ScreeningPlatformCoverageStatus> _values =
            new ConcurrentDictionary<string, ScreeningPlatformCoverageStatus>(_knownValues);

        private ScreeningPlatformCoverageStatus(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ScreeningPlatformCoverageStatus Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ScreeningPlatformCoverageStatus(value));
        }

        public static implicit operator ScreeningPlatformCoverageStatus(string value) => Of(value);
        public static implicit operator string(ScreeningPlatformCoverageStatus screeningplatformcoveragestatus) => screeningplatformcoveragestatus.Value;

        public static ScreeningPlatformCoverageStatus[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ScreeningPlatformCoverageStatus);

        public bool Equals(ScreeningPlatformCoverageStatus? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}