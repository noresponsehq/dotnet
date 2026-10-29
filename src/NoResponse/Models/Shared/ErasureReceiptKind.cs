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
    public class ErasureReceiptKind : IEquatable<ErasureReceiptKind>, IOpenEnum<string>
    {
        public static readonly ErasureReceiptKind Assessment = new ErasureReceiptKind("assessment");
        public static readonly ErasureReceiptKind Account = new ErasureReceiptKind("account");
        public static readonly ErasureReceiptKind Workspace = new ErasureReceiptKind("workspace");

        private static readonly Dictionary<string, ErasureReceiptKind> _knownValues =
            new Dictionary<string, ErasureReceiptKind>() {
                ["assessment"] = Assessment,
                ["account"] = Account,
                ["workspace"] = Workspace
            };

        private static readonly ConcurrentDictionary<string, ErasureReceiptKind> _values =
            new ConcurrentDictionary<string, ErasureReceiptKind>(_knownValues);

        private ErasureReceiptKind(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ErasureReceiptKind Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ErasureReceiptKind(value));
        }

        public static implicit operator ErasureReceiptKind(string value) => Of(value);
        public static implicit operator string(ErasureReceiptKind erasurereceiptkind) => erasurereceiptkind.Value;

        public static ErasureReceiptKind[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ErasureReceiptKind);

        public bool Equals(ErasureReceiptKind? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}