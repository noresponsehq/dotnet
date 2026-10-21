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
    /// The address's network type.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class IpSummaryType : IEquatable<IpSummaryType>, IOpenEnum<string>
    {
        public static readonly IpSummaryType Residential = new IpSummaryType("residential");
        public static readonly IpSummaryType Hosting = new IpSummaryType("hosting");
        public static readonly IpSummaryType Vpn = new IpSummaryType("vpn");
        public static readonly IpSummaryType Proxy = new IpSummaryType("proxy");
        public static readonly IpSummaryType Tor = new IpSummaryType("tor");
        public static readonly IpSummaryType Mobile = new IpSummaryType("mobile");
        public static readonly IpSummaryType Unknown = new IpSummaryType("unknown");

        private static readonly Dictionary<string, IpSummaryType> _knownValues =
            new Dictionary<string, IpSummaryType>() {
                ["residential"] = Residential,
                ["hosting"] = Hosting,
                ["vpn"] = Vpn,
                ["proxy"] = Proxy,
                ["tor"] = Tor,
                ["mobile"] = Mobile,
                ["unknown"] = Unknown
            };

        private static readonly ConcurrentDictionary<string, IpSummaryType> _values =
            new ConcurrentDictionary<string, IpSummaryType>(_knownValues);

        private IpSummaryType(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static IpSummaryType Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new IpSummaryType(value));
        }

        public static implicit operator IpSummaryType(string value) => Of(value);
        public static implicit operator string(IpSummaryType ipsummarytype) => ipsummarytype.Value;

        public static IpSummaryType[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as IpSummaryType);

        public bool Equals(IpSummaryType? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}