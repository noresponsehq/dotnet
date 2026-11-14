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
    /// Which common field a value is, where many sources share it: a join date and a birth date are both dates, with the roles joined and born. A platform account's field and a breach row's column with the same meaning carry the same kind and role. A linked_account field is named by its network. Absent for a field only its source returns.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class RecordRole : IEquatable<RecordRole>, IOpenEnum<string>
    {
        public static readonly RecordRole AccountId = new RecordRole("account_id");
        public static readonly RecordRole AccountStatus = new RecordRole("account_status");
        public static readonly RecordRole AccountType = new RecordRole("account_type");
        public static readonly RecordRole AlternatePhone = new RecordRole("alternate_phone");
        public static readonly RecordRole Balance = new RecordRole("balance");
        public static readonly RecordRole Banned = new RecordRole("banned");
        public static readonly RecordRole Bio = new RecordRole("bio");
        public static readonly RecordRole Born = new RecordRole("born");
        public static readonly RecordRole Bot = new RecordRole("bot");
        public static readonly RecordRole CallerCategory = new RecordRole("caller_category");
        public static readonly RecordRole CoverPicture = new RecordRole("cover_picture");
        public static readonly RecordRole Deleted = new RecordRole("deleted");
        public static readonly RecordRole DeletedOn = new RecordRole("deleted_on");
        public static readonly RecordRole Department = new RecordRole("department");
        public static readonly RecordRole DisplayName = new RecordRole("display_name");
        public static readonly RecordRole EmailVerified = new RecordRole("email_verified");
        public static readonly RecordRole Expires = new RecordRole("expires");
        public static readonly RecordRole FirstName = new RecordRole("first_name");
        public static readonly RecordRole Followers = new RecordRole("followers");
        public static readonly RecordRole Following = new RecordRole("following");
        public static readonly RecordRole Friends = new RecordRole("friends");
        public static readonly RecordRole FullAddress = new RecordRole("full_address");
        public static readonly RecordRole FullName = new RecordRole("full_name");
        public static readonly RecordRole Handle = new RecordRole("handle");
        public static readonly RecordRole Headline = new RecordRole("headline");
        public static readonly RecordRole Interests = new RecordRole("interests");
        public static readonly RecordRole JobTitle = new RecordRole("job_title");
        public static readonly RecordRole Joined = new RecordRole("joined");
        public static readonly RecordRole LastActive = new RecordRole("last_active");
        public static readonly RecordRole LastIp = new RecordRole("last_ip");
        public static readonly RecordRole LastName = new RecordRole("last_name");
        public static readonly RecordRole LastPost = new RecordRole("last_post");
        public static readonly RecordRole LastUpdated = new RecordRole("last_updated");
        public static readonly RecordRole Level = new RecordRole("level");
        public static readonly RecordRole LikesGiven = new RecordRole("likes_given");
        public static readonly RecordRole LikesReceived = new RecordRole("likes_received");
        public static readonly RecordRole LineType = new RecordRole("line_type");
        public static readonly RecordRole LinkedAccount = new RecordRole("linked_account");
        public static readonly RecordRole Location = new RecordRole("location");
        public static readonly RecordRole MarkedSpam = new RecordRole("marked_spam");
        public static readonly RecordRole MiddleName = new RecordRole("middle_name");
        public static readonly RecordRole Nationality = new RecordRole("nationality");
        public static readonly RecordRole Organisation = new RecordRole("organisation");
        public static readonly RecordRole OtherIp = new RecordRole("other_ip");
        public static readonly RecordRole Paid = new RecordRole("paid");
        public static readonly RecordRole PasswordChanged = new RecordRole("password_changed");
        public static readonly RecordRole Picture = new RecordRole("picture");
        public static readonly RecordRole Points = new RecordRole("points");
        public static readonly RecordRole Posts = new RecordRole("posts");
        public static readonly RecordRole Private = new RecordRole("private");
        public static readonly RecordRole ProfileLink = new RecordRole("profile_link");
        public static readonly RecordRole ProfileViews = new RecordRole("profile_views");
        public static readonly RecordRole Rating = new RecordRole("rating");
        public static readonly RecordRole Referrals = new RecordRole("referrals");
        public static readonly RecordRole Referrer = new RecordRole("referrer");
        public static readonly RecordRole ReportCount = new RecordRole("report_count");
        public static readonly RecordRole Reputation = new RecordRole("reputation");
        public static readonly RecordRole ReputationLabel = new RecordRole("reputation_label");
        public static readonly RecordRole Residence = new RecordRole("residence");
        public static readonly RecordRole Salutation = new RecordRole("salutation");
        public static readonly RecordRole School = new RecordRole("school");
        public static readonly RecordRole Signature = new RecordRole("signature");
        public static readonly RecordRole SignupIp = new RecordRole("signup_ip");
        public static readonly RecordRole SiteRole = new RecordRole("site_role");
        public static readonly RecordRole SpamScore = new RecordRole("spam_score");
        public static readonly RecordRole Street = new RecordRole("street");
        public static readonly RecordRole Verified = new RecordRole("verified");
        public static readonly RecordRole Warnings = new RecordRole("warnings");
        public static readonly RecordRole Website = new RecordRole("website");

        private static readonly Dictionary<string, RecordRole> _knownValues =
            new Dictionary<string, RecordRole>() {
                ["account_id"] = AccountId,
                ["account_status"] = AccountStatus,
                ["account_type"] = AccountType,
                ["alternate_phone"] = AlternatePhone,
                ["balance"] = Balance,
                ["banned"] = Banned,
                ["bio"] = Bio,
                ["born"] = Born,
                ["bot"] = Bot,
                ["caller_category"] = CallerCategory,
                ["cover_picture"] = CoverPicture,
                ["deleted"] = Deleted,
                ["deleted_on"] = DeletedOn,
                ["department"] = Department,
                ["display_name"] = DisplayName,
                ["email_verified"] = EmailVerified,
                ["expires"] = Expires,
                ["first_name"] = FirstName,
                ["followers"] = Followers,
                ["following"] = Following,
                ["friends"] = Friends,
                ["full_address"] = FullAddress,
                ["full_name"] = FullName,
                ["handle"] = Handle,
                ["headline"] = Headline,
                ["interests"] = Interests,
                ["job_title"] = JobTitle,
                ["joined"] = Joined,
                ["last_active"] = LastActive,
                ["last_ip"] = LastIp,
                ["last_name"] = LastName,
                ["last_post"] = LastPost,
                ["last_updated"] = LastUpdated,
                ["level"] = Level,
                ["likes_given"] = LikesGiven,
                ["likes_received"] = LikesReceived,
                ["line_type"] = LineType,
                ["linked_account"] = LinkedAccount,
                ["location"] = Location,
                ["marked_spam"] = MarkedSpam,
                ["middle_name"] = MiddleName,
                ["nationality"] = Nationality,
                ["organisation"] = Organisation,
                ["other_ip"] = OtherIp,
                ["paid"] = Paid,
                ["password_changed"] = PasswordChanged,
                ["picture"] = Picture,
                ["points"] = Points,
                ["posts"] = Posts,
                ["private"] = Private,
                ["profile_link"] = ProfileLink,
                ["profile_views"] = ProfileViews,
                ["rating"] = Rating,
                ["referrals"] = Referrals,
                ["referrer"] = Referrer,
                ["report_count"] = ReportCount,
                ["reputation"] = Reputation,
                ["reputation_label"] = ReputationLabel,
                ["residence"] = Residence,
                ["salutation"] = Salutation,
                ["school"] = School,
                ["signature"] = Signature,
                ["signup_ip"] = SignupIp,
                ["site_role"] = SiteRole,
                ["spam_score"] = SpamScore,
                ["street"] = Street,
                ["verified"] = Verified,
                ["warnings"] = Warnings,
                ["website"] = Website
            };

        private static readonly ConcurrentDictionary<string, RecordRole> _values =
            new ConcurrentDictionary<string, RecordRole>(_knownValues);

        private RecordRole(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static RecordRole Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new RecordRole(value));
        }

        public static implicit operator RecordRole(string value) => Of(value);
        public static implicit operator string(RecordRole recordrole) => recordrole.Value;

        public static RecordRole[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as RecordRole);

        public bool Equals(RecordRole? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}