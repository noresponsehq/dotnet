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
    public class ErasureReceiptTargetStatus : IEquatable<ErasureReceiptTargetStatus>, IOpenEnum<string>
    {
        public static readonly ErasureReceiptTargetStatus Pending = new ErasureReceiptTargetStatus("pending");
        public static readonly ErasureReceiptTargetStatus Completed = new ErasureReceiptTargetStatus("completed");

        private static readonly Dictionary<string, ErasureReceiptTargetStatus> _knownValues =
            new Dictionary<string, ErasureReceiptTargetStatus>() {
                ["pending"] = Pending,
                ["completed"] = Completed
            };

        private static readonly ConcurrentDictionary<string, ErasureReceiptTargetStatus> _values =
            new ConcurrentDictionary<string, ErasureReceiptTargetStatus>(_knownValues);

        private ErasureReceiptTargetStatus(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ErasureReceiptTargetStatus Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ErasureReceiptTargetStatus(value));
        }

        public static implicit operator ErasureReceiptTargetStatus(string value) => Of(value);
        public static implicit operator string(ErasureReceiptTargetStatus erasurereceipttargetstatus) => erasurereceipttargetstatus.Value;

        public static ErasureReceiptTargetStatus[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ErasureReceiptTargetStatus);

        public bool Equals(ErasureReceiptTargetStatus? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}