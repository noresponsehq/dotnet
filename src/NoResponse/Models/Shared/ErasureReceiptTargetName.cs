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
    public class ErasureReceiptTargetName : IEquatable<ErasureReceiptTargetName>, IOpenEnum<string>
    {
        public static readonly ErasureReceiptTargetName Serving = new ErasureReceiptTargetName("serving");
        public static readonly ErasureReceiptTargetName Outbox = new ErasureReceiptTargetName("outbox");
        public static readonly ErasureReceiptTargetName Cache = new ErasureReceiptTargetName("cache");
        public static readonly ErasureReceiptTargetName DownstreamProjection = new ErasureReceiptTargetName("downstream_projection");

        private static readonly Dictionary<string, ErasureReceiptTargetName> _knownValues =
            new Dictionary<string, ErasureReceiptTargetName>() {
                ["serving"] = Serving,
                ["outbox"] = Outbox,
                ["cache"] = Cache,
                ["downstream_projection"] = DownstreamProjection
            };

        private static readonly ConcurrentDictionary<string, ErasureReceiptTargetName> _values =
            new ConcurrentDictionary<string, ErasureReceiptTargetName>(_knownValues);

        private ErasureReceiptTargetName(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ErasureReceiptTargetName Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ErasureReceiptTargetName(value));
        }

        public static implicit operator ErasureReceiptTargetName(string value) => Of(value);
        public static implicit operator string(ErasureReceiptTargetName erasurereceipttargetname) => erasurereceipttargetname.Value;

        public static ErasureReceiptTargetName[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ErasureReceiptTargetName);

        public bool Equals(ErasureReceiptTargetName? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}