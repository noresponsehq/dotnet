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
    public class Exclusion : IEquatable<Exclusion>, IOpenEnum<string>
    {
        public static readonly Exclusion ExpiredOrErasedRecords = new Exclusion("expired_or_erased_records");
        public static readonly Exclusion SecretsAndCredentialDigests = new Exclusion("secrets_and_credential_digests");
        public static readonly Exclusion PrivateSourceAndWorkPayloads = new Exclusion("private_source_and_work_payloads");
        public static readonly Exclusion CrossCustomerData = new Exclusion("cross_customer_data");
        public static readonly Exclusion LegacyOutcomeNotes = new Exclusion("legacy_outcome_notes");
        public static readonly Exclusion UncapturedHistoricalDecisions = new Exclusion("uncaptured_historical_decisions");

        private static readonly Dictionary<string, Exclusion> _knownValues =
            new Dictionary<string, Exclusion>() {
                ["expired_or_erased_records"] = ExpiredOrErasedRecords,
                ["secrets_and_credential_digests"] = SecretsAndCredentialDigests,
                ["private_source_and_work_payloads"] = PrivateSourceAndWorkPayloads,
                ["cross_customer_data"] = CrossCustomerData,
                ["legacy_outcome_notes"] = LegacyOutcomeNotes,
                ["uncaptured_historical_decisions"] = UncapturedHistoricalDecisions
            };

        private static readonly ConcurrentDictionary<string, Exclusion> _values =
            new ConcurrentDictionary<string, Exclusion>(_knownValues);

        private Exclusion(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Exclusion Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Exclusion(value));
        }

        public static implicit operator Exclusion(string value) => Of(value);
        public static implicit operator string(Exclusion exclusion) => exclusion.Value;

        public static Exclusion[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Exclusion);

        public bool Equals(Exclusion? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}