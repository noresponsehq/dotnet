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
    /// Who is in the table: site_breach, a site's own users; credential_list, a combolist or compilation; stealer_log, malware or botnet victims; scrape; hacking_forum, a criminal hacking, cracking or carding community; abuse_service, the customers of DDoS-for-hire, account generators, dump or card shops, darknet markets, stalkerware or breach-lookup services; darkweb_site, an onion site of unknown purpose.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class BreachKind : IEquatable<BreachKind>, IOpenEnum<string>
    {
        public static readonly BreachKind SiteBreach = new BreachKind("site_breach");
        public static readonly BreachKind CredentialList = new BreachKind("credential_list");
        public static readonly BreachKind StealerLog = new BreachKind("stealer_log");
        public static readonly BreachKind Scrape = new BreachKind("scrape");
        public static readonly BreachKind HackingForum = new BreachKind("hacking_forum");
        public static readonly BreachKind AbuseService = new BreachKind("abuse_service");
        public static readonly BreachKind DarkwebSite = new BreachKind("darkweb_site");

        private static readonly Dictionary<string, BreachKind> _knownValues =
            new Dictionary<string, BreachKind>() {
                ["site_breach"] = SiteBreach,
                ["credential_list"] = CredentialList,
                ["stealer_log"] = StealerLog,
                ["scrape"] = Scrape,
                ["hacking_forum"] = HackingForum,
                ["abuse_service"] = AbuseService,
                ["darkweb_site"] = DarkwebSite
            };

        private static readonly ConcurrentDictionary<string, BreachKind> _values =
            new ConcurrentDictionary<string, BreachKind>(_knownValues);

        private BreachKind(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static BreachKind Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new BreachKind(value));
        }

        public static implicit operator BreachKind(string value) => Of(value);
        public static implicit operator string(BreachKind breachkind) => breachkind.Value;

        public static BreachKind[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as BreachKind);

        public bool Equals(BreachKind? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}