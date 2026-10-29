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
    public class CustomerExportRequestKind : IEquatable<CustomerExportRequestKind>, IOpenEnum<string>
    {
        public static readonly CustomerExportRequestKind Account = new CustomerExportRequestKind("account");
        public static readonly CustomerExportRequestKind Audit = new CustomerExportRequestKind("audit");

        private static readonly Dictionary<string, CustomerExportRequestKind> _knownValues =
            new Dictionary<string, CustomerExportRequestKind>() {
                ["account"] = Account,
                ["audit"] = Audit
            };

        private static readonly ConcurrentDictionary<string, CustomerExportRequestKind> _values =
            new ConcurrentDictionary<string, CustomerExportRequestKind>(_knownValues);

        private CustomerExportRequestKind(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static CustomerExportRequestKind Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new CustomerExportRequestKind(value));
        }

        public static implicit operator CustomerExportRequestKind(string value) => Of(value);
        public static implicit operator string(CustomerExportRequestKind customerexportrequestkind) => customerexportrequestkind.Value;

        public static CustomerExportRequestKind[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as CustomerExportRequestKind);

        public bool Equals(CustomerExportRequestKind? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}