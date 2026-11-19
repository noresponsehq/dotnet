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
    /// ai_typing and analysis appear only on searches recorded before the engine stopped analysing results.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class Stage : IEquatable<Stage>, IOpenEnum<string>
    {
        public static readonly Stage Lookup = new Stage("lookup");
        public static readonly Stage LookupTyping = new Stage("lookup_typing");
        public static readonly Stage Platforms = new Stage("platforms");
        public static readonly Stage Written = new Stage("written");
        public static readonly Stage LatePlatforms = new Stage("late_platforms");
        public static readonly Stage LateLookup = new Stage("late_lookup");
        public static readonly Stage AiTyping = new Stage("ai_typing");
        public static readonly Stage Analysis = new Stage("analysis");
        public static readonly Stage Rating = new Stage("rating");

        private static readonly Dictionary<string, Stage> _knownValues =
            new Dictionary<string, Stage>() {
                ["lookup"] = Lookup,
                ["lookup_typing"] = LookupTyping,
                ["platforms"] = Platforms,
                ["written"] = Written,
                ["late_platforms"] = LatePlatforms,
                ["late_lookup"] = LateLookup,
                ["ai_typing"] = AiTyping,
                ["analysis"] = Analysis,
                ["rating"] = Rating
            };

        private static readonly ConcurrentDictionary<string, Stage> _values =
            new ConcurrentDictionary<string, Stage>(_knownValues);

        private Stage(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static Stage Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new Stage(value));
        }

        public static implicit operator Stage(string value) => Of(value);
        public static implicit operator string(Stage stage) => stage.Value;

        public static Stage[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as Stage);

        public bool Equals(Stage? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}