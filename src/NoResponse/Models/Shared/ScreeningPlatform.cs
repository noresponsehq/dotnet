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
    public class ScreeningPlatform : IEquatable<ScreeningPlatform>, IOpenEnum<string>
    {
        public static readonly ScreeningPlatform Instagram = new ScreeningPlatform("instagram");
        public static readonly ScreeningPlatform X = new ScreeningPlatform("x");
        public static readonly ScreeningPlatform Bluesky = new ScreeningPlatform("bluesky");
        public static readonly ScreeningPlatform Youtube = new ScreeningPlatform("youtube");
        public static readonly ScreeningPlatform Tiktok = new ScreeningPlatform("tiktok");
        public static readonly ScreeningPlatform Facebook = new ScreeningPlatform("facebook");
        public static readonly ScreeningPlatform Threads = new ScreeningPlatform("threads");
        public static readonly ScreeningPlatform Reddit = new ScreeningPlatform("reddit");
        public static readonly ScreeningPlatform Telegram = new ScreeningPlatform("telegram");
        public static readonly ScreeningPlatform Linkedin = new ScreeningPlatform("linkedin");

        private static readonly Dictionary<string, ScreeningPlatform> _knownValues =
            new Dictionary<string, ScreeningPlatform>() {
                ["instagram"] = Instagram,
                ["x"] = X,
                ["bluesky"] = Bluesky,
                ["youtube"] = Youtube,
                ["tiktok"] = Tiktok,
                ["facebook"] = Facebook,
                ["threads"] = Threads,
                ["reddit"] = Reddit,
                ["telegram"] = Telegram,
                ["linkedin"] = Linkedin
            };

        private static readonly ConcurrentDictionary<string, ScreeningPlatform> _values =
            new ConcurrentDictionary<string, ScreeningPlatform>(_knownValues);

        private ScreeningPlatform(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ScreeningPlatform Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ScreeningPlatform(value));
        }

        public static implicit operator ScreeningPlatform(string value) => Of(value);
        public static implicit operator string(ScreeningPlatform screeningplatform) => screeningplatform.Value;

        public static ScreeningPlatform[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ScreeningPlatform);

        public bool Equals(ScreeningPlatform? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}