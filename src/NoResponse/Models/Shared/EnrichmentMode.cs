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
    /// off: enrichment starts no search. suggest: enrichment starts no search by itself. automatic: after a workspace person's own investigation, and each time a search on that person finishes, enrichment starts searches by itself.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class EnrichmentMode : IEquatable<EnrichmentMode>, IOpenEnum<string>
    {
        public static readonly EnrichmentMode Off = new EnrichmentMode("off");
        public static readonly EnrichmentMode Suggest = new EnrichmentMode("suggest");
        public static readonly EnrichmentMode Automatic = new EnrichmentMode("automatic");

        private static readonly Dictionary<string, EnrichmentMode> _knownValues =
            new Dictionary<string, EnrichmentMode>() {
                ["off"] = Off,
                ["suggest"] = Suggest,
                ["automatic"] = Automatic
            };

        private static readonly ConcurrentDictionary<string, EnrichmentMode> _values =
            new ConcurrentDictionary<string, EnrichmentMode>(_knownValues);

        private EnrichmentMode(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static EnrichmentMode Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new EnrichmentMode(value));
        }

        public static implicit operator EnrichmentMode(string value) => Of(value);
        public static implicit operator string(EnrichmentMode enrichmentmode) => enrichmentmode.Value;

        public static EnrichmentMode[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as EnrichmentMode);

        public bool Equals(EnrichmentMode? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}