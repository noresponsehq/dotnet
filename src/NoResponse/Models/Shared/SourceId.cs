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
    /// A NoResponse source: github (GitHub accounts), phone (the phone lookup), breach (breach exposure), rdap (the email domain's registration), mx (the email domain's mail servers) or ipintel (IP intelligence).
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class SourceId : IEquatable<SourceId>, IOpenEnum<string>
    {
        public static readonly SourceId Github = new SourceId("github");
        public static readonly SourceId Phone = new SourceId("phone");
        public static readonly SourceId Breach = new SourceId("breach");
        public static readonly SourceId Rdap = new SourceId("rdap");
        public static readonly SourceId Mx = new SourceId("mx");
        public static readonly SourceId Ipintel = new SourceId("ipintel");

        private static readonly Dictionary<string, SourceId> _knownValues =
            new Dictionary<string, SourceId>() {
                ["github"] = Github,
                ["phone"] = Phone,
                ["breach"] = Breach,
                ["rdap"] = Rdap,
                ["mx"] = Mx,
                ["ipintel"] = Ipintel
            };

        private static readonly ConcurrentDictionary<string, SourceId> _values =
            new ConcurrentDictionary<string, SourceId>(_knownValues);

        private SourceId(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static SourceId Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new SourceId(value));
        }

        public static implicit operator SourceId(string value) => Of(value);
        public static implicit operator string(SourceId sourceid) => sourceid.Value;

        public static SourceId[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as SourceId);

        public bool Equals(SourceId? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}