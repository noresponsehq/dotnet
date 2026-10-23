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
    /// A stable reason code. New codes may appear; ignore any you do not know.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class ReasonCode : IEquatable<ReasonCode>, IOpenEnum<string>
    {
        public static readonly ReasonCode NoDigitalFootprint = new ReasonCode("no_digital_footprint");
        public static readonly ReasonCode VoipNumber = new ReasonCode("voip_number");
        public static readonly ReasonCode DisposableEmail = new ReasonCode("disposable_email");
        public static readonly ReasonCode NewEmail = new ReasonCode("new_email");
        public static readonly ReasonCode SpamReports = new ReasonCode("spam_reports");
        public static readonly ReasonCode FraudFlag = new ReasonCode("fraud_flag");
        public static readonly ReasonCode IdentityMismatch = new ReasonCode("identity_mismatch");
        public static readonly ReasonCode CountryMismatch = new ReasonCode("country_mismatch");
        public static readonly ReasonCode ProxyIp = new ReasonCode("proxy_ip");
        public static readonly ReasonCode SeenBefore = new ReasonCode("seen_before");
        public static readonly ReasonCode SharedIp = new ReasonCode("shared_ip");
        public static readonly ReasonCode ManyRecentAccounts = new ReasonCode("many_recent_accounts");
        public static readonly ReasonCode EstablishedAccounts = new ReasonCode("established_accounts");
        public static readonly ReasonCode AgedIdentity = new ReasonCode("aged_identity");
        public static readonly ReasonCode BreachNameMatch = new ReasonCode("breach_name_match");
        public static readonly ReasonCode BreachCooccurrence = new ReasonCode("breach_cooccurrence");
        public static readonly ReasonCode BreachNameMismatch = new ReasonCode("breach_name_mismatch");
        public static readonly ReasonCode BreachPhoneMismatch = new ReasonCode("breach_phone_mismatch");
        public static readonly ReasonCode RecentPasswordExposure = new ReasonCode("recent_password_exposure");
        public static readonly ReasonCode CredentialListExposure = new ReasonCode("credential_list_exposure");
        public static readonly ReasonCode AbuseEcosystemMember = new ReasonCode("abuse_ecosystem_member");
        public static readonly ReasonCode NoBreachHistory = new ReasonCode("no_breach_history");
        public static readonly ReasonCode AutomationFastSubmit = new ReasonCode("automation_fast_submit");
        public static readonly ReasonCode ClientClockSkew = new ReasonCode("client_clock_skew");
        public static readonly ReasonCode BotOrAutomation = new ReasonCode("bot_or_automation");
        public static readonly ReasonCode AuthEmailVerified = new ReasonCode("auth_email_verified");
        public static readonly ReasonCode AuthPhoneVerified = new ReasonCode("auth_phone_verified");
        public static readonly ReasonCode AuthMfaRecent = new ReasonCode("auth_mfa_recent");
        public static readonly ReasonCode AuthNewSession = new ReasonCode("auth_new_session");
        public static readonly ReasonCode AuthNewAccount = new ReasonCode("auth_new_account");
        public static readonly ReasonCode AuthImpersonated = new ReasonCode("auth_impersonated");
        public static readonly ReasonCode ContextTimezoneMismatch = new ReasonCode("context_timezone_mismatch");
        public static readonly ReasonCode ContextLocaleMismatch = new ReasonCode("context_locale_mismatch");
        public static readonly ReasonCode ContextLineCountryMismatch = new ReasonCode("context_line_country_mismatch");
        public static readonly ReasonCode PriorAbuseLink = new ReasonCode("prior_abuse_link");
        public static readonly ReasonCode TestScenario = new ReasonCode("test_scenario");
        public static readonly ReasonCode SharedApiKey = new ReasonCode("shared_api_key");

        private static readonly Dictionary<string, ReasonCode> _knownValues =
            new Dictionary<string, ReasonCode>() {
                ["no_digital_footprint"] = NoDigitalFootprint,
                ["voip_number"] = VoipNumber,
                ["disposable_email"] = DisposableEmail,
                ["new_email"] = NewEmail,
                ["spam_reports"] = SpamReports,
                ["fraud_flag"] = FraudFlag,
                ["identity_mismatch"] = IdentityMismatch,
                ["country_mismatch"] = CountryMismatch,
                ["proxy_ip"] = ProxyIp,
                ["seen_before"] = SeenBefore,
                ["shared_ip"] = SharedIp,
                ["many_recent_accounts"] = ManyRecentAccounts,
                ["established_accounts"] = EstablishedAccounts,
                ["aged_identity"] = AgedIdentity,
                ["breach_name_match"] = BreachNameMatch,
                ["breach_cooccurrence"] = BreachCooccurrence,
                ["breach_name_mismatch"] = BreachNameMismatch,
                ["breach_phone_mismatch"] = BreachPhoneMismatch,
                ["recent_password_exposure"] = RecentPasswordExposure,
                ["credential_list_exposure"] = CredentialListExposure,
                ["abuse_ecosystem_member"] = AbuseEcosystemMember,
                ["no_breach_history"] = NoBreachHistory,
                ["automation_fast_submit"] = AutomationFastSubmit,
                ["client_clock_skew"] = ClientClockSkew,
                ["bot_or_automation"] = BotOrAutomation,
                ["auth_email_verified"] = AuthEmailVerified,
                ["auth_phone_verified"] = AuthPhoneVerified,
                ["auth_mfa_recent"] = AuthMfaRecent,
                ["auth_new_session"] = AuthNewSession,
                ["auth_new_account"] = AuthNewAccount,
                ["auth_impersonated"] = AuthImpersonated,
                ["context_timezone_mismatch"] = ContextTimezoneMismatch,
                ["context_locale_mismatch"] = ContextLocaleMismatch,
                ["context_line_country_mismatch"] = ContextLineCountryMismatch,
                ["prior_abuse_link"] = PriorAbuseLink,
                ["test_scenario"] = TestScenario,
                ["shared_api_key"] = SharedApiKey
            };

        private static readonly ConcurrentDictionary<string, ReasonCode> _values =
            new ConcurrentDictionary<string, ReasonCode>(_knownValues);

        private ReasonCode(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static ReasonCode Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new ReasonCode(value));
        }

        public static implicit operator ReasonCode(string value) => Of(value);
        public static implicit operator string(ReasonCode reasoncode) => reasoncode.Value;

        public static ReasonCode[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as ReasonCode);

        public bool Equals(ReasonCode? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}