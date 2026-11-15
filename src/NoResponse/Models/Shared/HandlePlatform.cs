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
    /// A platform a screening can cover. github and scratch through flickr are each one read of the public profile a handle names, with no posts; chesscom also reads the profile's stats, clubs and last two months of games, for its activity and contacts. synthetic is the deterministic connector's own platform, for exercising the product end to end with no network.
    /// </summary>
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
        public static readonly HandlePlatform Github = new HandlePlatform("github");
        public static readonly HandlePlatform Scratch = new HandlePlatform("scratch");
        public static readonly HandlePlatform Dailymotion = new HandlePlatform("dailymotion");
        public static readonly HandlePlatform Substack = new HandlePlatform("substack");
        public static readonly HandlePlatform Duolingo = new HandlePlatform("duolingo");
        public static readonly HandlePlatform Dockerhub = new HandlePlatform("dockerhub");
        public static readonly HandlePlatform Wikipedia = new HandlePlatform("wikipedia");
        public static readonly HandlePlatform Internetarchive = new HandlePlatform("internetarchive");
        public static readonly HandlePlatform Spotify = new HandlePlatform("spotify");
        public static readonly HandlePlatform Patreon = new HandlePlatform("patreon");
        public static readonly HandlePlatform Wordpress = new HandlePlatform("wordpress");
        public static readonly HandlePlatform Okru = new HandlePlatform("okru");
        public static readonly HandlePlatform Flickr = new HandlePlatform("flickr");
        public static readonly HandlePlatform Chesscom = new HandlePlatform("chesscom");
        public static readonly HandlePlatform Synthetic = new HandlePlatform("synthetic");

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
                ["linkedin"] = Linkedin,
                ["github"] = Github,
                ["scratch"] = Scratch,
                ["dailymotion"] = Dailymotion,
                ["substack"] = Substack,
                ["duolingo"] = Duolingo,
                ["dockerhub"] = Dockerhub,
                ["wikipedia"] = Wikipedia,
                ["internetarchive"] = Internetarchive,
                ["spotify"] = Spotify,
                ["patreon"] = Patreon,
                ["wordpress"] = Wordpress,
                ["okru"] = Okru,
                ["flickr"] = Flickr,
                ["chesscom"] = Chesscom,
                ["synthetic"] = Synthetic
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