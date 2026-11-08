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
    public class HandlePlatform : IEquatable<HandlePlatform>, IOpenEnum<string>
    {
        public static readonly HandlePlatform Instagram = new HandlePlatform("instagram");
        public static readonly HandlePlatform X = new HandlePlatform("x");
        public static readonly HandlePlatform Bluesky = new HandlePlatform("bluesky");
        public static readonly HandlePlatform Youtube = new HandlePlatform("youtube");
        public static readonly HandlePlatform Tiktok = new HandlePlatform("tiktok");
        public static readonly HandlePlatform Facebook = new HandlePlatform("facebook");
        public static readonly HandlePlatform Threads = new HandlePlatform("threads");
        public static readonly HandlePlatform Reddit = new HandlePlatform("reddit");
        public static readonly HandlePlatform Telegram = new HandlePlatform("telegram");
        public static readonly HandlePlatform Linkedin = new HandlePlatform("linkedin");

        private static readonly Dictionary<string, HandlePlatform> _knownValues =
            new Dictionary<string, HandlePlatform>() {
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

        private static readonly ConcurrentDictionary<string, HandlePlatform> _values =
            new ConcurrentDictionary<string, HandlePlatform>(_knownValues);

        private HandlePlatform(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static HandlePlatform Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new HandlePlatform(value));
        }

        public static implicit operator HandlePlatform(string value) => Of(value);
        public static implicit operator string(HandlePlatform handleplatform) => handleplatform.Value;

        public static HandlePlatform[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as HandlePlatform);

        public bool Equals(HandlePlatform? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}