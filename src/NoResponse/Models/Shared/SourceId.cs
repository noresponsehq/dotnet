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
    /// A NoResponse source: github (GitHub accounts), phone (the phone lookup), breach (breach exposure), rdap (the email domain's registration), mx (the email domain's mail servers), ipintel (IP intelligence) or an email account source that searches one platform for an account by email address (adobe, nvidia, wix, life360, zapier, x_email, snapchat_email, amazon, spotify_email, google, duolingo_email). x_email, snapchat_email, amazon and spotify_email only say whether the email has an account on X, Snapchat, Amazon or Spotify. google runs only for Gmail addresses. scratch, dailymotion, substack, duolingo, dockerhub, wikipedia, internetarchive, telegram, spotify, patreon, wordpress, okru, flickr and chesscom each search that platform for an account by username; telegram also searches by phone number, through a logged-in Telegram lookup account. whatsapp searches WhatsApp for an account by phone number, and life360_phone searches Life360 for one. opplysningen (1881, Norway), ratsit (Sweden), numlookup and usphonebook (the US) and reverseaustralia (Australia) each look up a phone number of their own country on that site. youtube searches YouTube for a channel by username.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class SourceId : IEquatable<SourceId>, IOpenEnum<string>
    {
        public static readonly SourceId Github = new SourceId("github");
        public static readonly SourceId Phone = new SourceId("phone");
        public static readonly SourceId Breach = new SourceId("breach");
        public static readonly SourceId Rdap = new SourceId("rdap");
        public static readonly SourceId Mx = new SourceId("mx");
        public static readonly SourceId Ipintel = new SourceId("ipintel");
        public static readonly SourceId Adobe = new SourceId("adobe");
        public static readonly SourceId Nvidia = new SourceId("nvidia");
        public static readonly SourceId Wix = new SourceId("wix");
        public static readonly SourceId Life360 = new SourceId("life360");
        public static readonly SourceId Life360Phone = new SourceId("life360_phone");
        public static readonly SourceId Zapier = new SourceId("zapier");
        public static readonly SourceId XEmail = new SourceId("x_email");
        public static readonly SourceId SnapchatEmail = new SourceId("snapchat_email");
        public static readonly SourceId Amazon = new SourceId("amazon");
        public static readonly SourceId SpotifyEmail = new SourceId("spotify_email");
        public static readonly SourceId Google = new SourceId("google");
        public static readonly SourceId Scratch = new SourceId("scratch");
        public static readonly SourceId Dailymotion = new SourceId("dailymotion");
        public static readonly SourceId Substack = new SourceId("substack");
        public static readonly SourceId Duolingo = new SourceId("duolingo");
        public static readonly SourceId DuolingoEmail = new SourceId("duolingo_email");
        public static readonly SourceId Dockerhub = new SourceId("dockerhub");
        public static readonly SourceId Wikipedia = new SourceId("wikipedia");
        public static readonly SourceId Internetarchive = new SourceId("internetarchive");
        public static readonly SourceId Telegram = new SourceId("telegram");
        public static readonly SourceId Youtube = new SourceId("youtube");
        public static readonly SourceId Spotify = new SourceId("spotify");
        public static readonly SourceId Patreon = new SourceId("patreon");
        public static readonly SourceId Wordpress = new SourceId("wordpress");
        public static readonly SourceId Okru = new SourceId("okru");
        public static readonly SourceId Flickr = new SourceId("flickr");
        public static readonly SourceId Chesscom = new SourceId("chesscom");
        public static readonly SourceId Whatsapp = new SourceId("whatsapp");
        public static readonly SourceId Opplysningen = new SourceId("opplysningen");
        public static readonly SourceId Ratsit = new SourceId("ratsit");
        public static readonly SourceId Numlookup = new SourceId("numlookup");
        public static readonly SourceId Usphonebook = new SourceId("usphonebook");
        public static readonly SourceId Reverseaustralia = new SourceId("reverseaustralia");

        private static readonly Dictionary<string, SourceId> _knownValues =
            new Dictionary<string, SourceId>() {
                ["github"] = Github,
                ["phone"] = Phone,
                ["breach"] = Breach,
                ["rdap"] = Rdap,
                ["mx"] = Mx,
                ["ipintel"] = Ipintel,
                ["adobe"] = Adobe,
                ["nvidia"] = Nvidia,
                ["wix"] = Wix,
                ["life360"] = Life360,
                ["life360_phone"] = Life360Phone,
                ["zapier"] = Zapier,
                ["x_email"] = XEmail,
                ["snapchat_email"] = SnapchatEmail,
                ["amazon"] = Amazon,
                ["spotify_email"] = SpotifyEmail,
                ["google"] = Google,
                ["scratch"] = Scratch,
                ["dailymotion"] = Dailymotion,
                ["substack"] = Substack,
                ["duolingo"] = Duolingo,
                ["duolingo_email"] = DuolingoEmail,
                ["dockerhub"] = Dockerhub,
                ["wikipedia"] = Wikipedia,
                ["internetarchive"] = Internetarchive,
                ["telegram"] = Telegram,
                ["youtube"] = Youtube,
                ["spotify"] = Spotify,
                ["patreon"] = Patreon,
                ["wordpress"] = Wordpress,
                ["okru"] = Okru,
                ["flickr"] = Flickr,
                ["chesscom"] = Chesscom,
                ["whatsapp"] = Whatsapp,
                ["opplysningen"] = Opplysningen,
                ["ratsit"] = Ratsit,
                ["numlookup"] = Numlookup,
                ["usphonebook"] = Usphonebook,
                ["reverseaustralia"] = Reverseaustralia
            };

        private static readonly ConcurrentDictionary<string, SourceId> _values =
            new ConcurrentDictionary<string, SourceId>(_knownValues);

        private SourceId(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static SourceId Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new SourceId(value));
        }

        public static implicit operator SourceId(string value) => Of(value);
        public static implicit operator string(SourceId sourceid) => sourceid.Value;

        public static SourceId[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as SourceId);

        public bool Equals(SourceId? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}