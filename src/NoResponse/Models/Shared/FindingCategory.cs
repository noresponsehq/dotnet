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
    /// What a finding is about: a direct threat to the principal or the group, intent to harm without a named target, fixation on the principal, a grievance against the host, research into the venue, route or security, weapons tied to any of these, disruption or protest planning toward this group, violent extremist content or identification, prior harassment or stalking of staff or principals, or posting the principal's addresses or movements.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class FindingCategory : IEquatable<FindingCategory>, IOpenEnum<string>
    {
        public static readonly FindingCategory DirectThreat = new FindingCategory("direct_threat");
        public static readonly FindingCategory HarmIntent = new FindingCategory("harm_intent");
        public static readonly FindingCategory Fixation = new FindingCategory("fixation");
        public static readonly FindingCategory Grievance = new FindingCategory("grievance");
        public static readonly FindingCategory SecurityResearch = new FindingCategory("security_research");
        public static readonly FindingCategory Weapons = new FindingCategory("weapons");
        public static readonly FindingCategory Disruption = new FindingCategory("disruption");
        public static readonly FindingCategory ViolentExtremism = new FindingCategory("violent_extremism");
        public static readonly FindingCategory Harassment = new FindingCategory("harassment");
        public static readonly FindingCategory MovementExposure = new FindingCategory("movement_exposure");

        private static readonly Dictionary<string, FindingCategory> _knownValues =
            new Dictionary<string, FindingCategory>() {
                ["direct_threat"] = DirectThreat,
                ["harm_intent"] = HarmIntent,
                ["fixation"] = Fixation,
                ["grievance"] = Grievance,
                ["security_research"] = SecurityResearch,
                ["weapons"] = Weapons,
                ["disruption"] = Disruption,
                ["violent_extremism"] = ViolentExtremism,
                ["harassment"] = Harassment,
                ["movement_exposure"] = MovementExposure
            };

        private static readonly ConcurrentDictionary<string, FindingCategory> _values =
            new ConcurrentDictionary<string, FindingCategory>(_knownValues);

        private FindingCategory(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static FindingCategory Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new FindingCategory(value));
        }

        public static implicit operator FindingCategory(string value) => Of(value);
        public static implicit operator string(FindingCategory findingcategory) => findingcategory.Value;

        public static FindingCategory[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as FindingCategory);

        public bool Equals(FindingCategory? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}