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
    public class PolicyEvaluationStatus : IEquatable<PolicyEvaluationStatus>, IOpenEnum<string>
    {
        public static readonly PolicyEvaluationStatus Queued = new PolicyEvaluationStatus("queued");
        public static readonly PolicyEvaluationStatus Running = new PolicyEvaluationStatus("running");
        public static readonly PolicyEvaluationStatus Completed = new PolicyEvaluationStatus("completed");
        public static readonly PolicyEvaluationStatus Failed = new PolicyEvaluationStatus("failed");
        public static readonly PolicyEvaluationStatus Cancelled = new PolicyEvaluationStatus("cancelled");

        private static readonly Dictionary<string, PolicyEvaluationStatus> _knownValues =
            new Dictionary<string, PolicyEvaluationStatus>() {
                ["queued"] = Queued,
                ["running"] = Running,
                ["completed"] = Completed,
                ["failed"] = Failed,
                ["cancelled"] = Cancelled
            };

        private static readonly ConcurrentDictionary<string, PolicyEvaluationStatus> _values =
            new ConcurrentDictionary<string, PolicyEvaluationStatus>(_knownValues);

        private PolicyEvaluationStatus(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static PolicyEvaluationStatus Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new PolicyEvaluationStatus(value));
        }

        public static implicit operator PolicyEvaluationStatus(string value) => Of(value);
        public static implicit operator string(PolicyEvaluationStatus policyevaluationstatus) => policyevaluationstatus.Value;

        public static PolicyEvaluationStatus[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as PolicyEvaluationStatus);

        public bool Equals(PolicyEvaluationStatus? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}