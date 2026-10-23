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
    /// The customer event an assessment is made for: signup (a new account), login (a returning sign-in) or purchase (a payment or credit grant). The account's risk is reported with the decision for this flow.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Flow : IEquatable<Flow>, IOpenEnum<string>
    {
        public static readonly Flow Signup = new Flow("signup");
        public static readonly Flow Login = new Flow("login");
        public static readonly Flow Purchase = new Flow("purchase");

        private static readonly Dictionary<string, Flow> _knownValues =
            new Dictionary<string, Flow>() {
                ["signup"] = Signup,
                ["login"] = Login,
                ["purchase"] = Purchase
            };

        private static readonly ConcurrentDictionary<string, Flow> _values =
            new ConcurrentDictionary<string, Flow>(_knownValues);

        private Flow(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Flow Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Flow(value));
        }

        public static implicit operator Flow(string value) => Of(value);
        public static implicit operator string(Flow flow) => flow.Value;

        public static Flow[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Flow);

        public bool Equals(Flow? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}