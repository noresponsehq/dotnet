#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// One online-account search of the signup's email or username, detailed view only. name and domain name the platform searched, not a provider. outcome is the search's signal outcome: found, not_found, or a failure (blocked, unavailable, inconclusive) with its reason; a failure is not reported as not_found. The members after reason are what the platform returned beside an account it found, each absent when it did not say.
    /// </summary>
    public class OnlineAccount
    {
        /// <summary>
        /// The platform's name, such as Adobe or GitHub.
        /// </summary>
        [JsonProperty("name", Required = Newtonsoft.Json.Required.Always)]
        public string Name { get; set; } = default!;

        /// <summary>
        /// The platform's domain, lower case: the host its logo is fetched for.
        /// </summary>
        [JsonProperty("domain", Required = Newtonsoft.Json.Required.Always)]
        public string Domain { get; set; } = default!;

        [JsonProperty("identifier", Required = Newtonsoft.Json.Required.Always)]
        public Identifier Identifier { get; set; } = default!;

        /// <summary>
        /// Per-signal outcome. A source that timed out, was blocked or errored reports blocked, unavailable or inconclusive with a reason and is listed in incomplete_sources; not_found is a definite absence.
        /// </summary>
        [JsonProperty("outcome", Required = Newtonsoft.Json.Required.Always)]
        public SignalOutcome Outcome { get; set; } = default!;

        /// <summary>
        /// Why a signal or source did not produce a definite found/not_found. timed_out means the source missed the request deadline; a continuation may still settle it. deferred means the (source, identifier) pair is async-only and was not attempted on the synchronous path; a continuation settles it.
        /// </summary>
        [JsonProperty("reason", Required = Newtonsoft.Json.Required.DisallowNull)]
        public FailureReason? Reason { get; set; }

        /// <summary>
        /// How the account signs in, as the platform reports it, sorted: a password, a Google, Facebook, Apple or Microsoft account, the organisation's single sign-on, or another method.
        /// </summary>
        [JsonProperty("sign_in", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<SignIn>? SignIn { get; set; }

        /// <summary>
        /// Whether the platform says the account's email is verified.
        /// </summary>
        [JsonProperty("email_verified", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? EmailVerified { get; set; }

        /// <summary>
        /// Whether the platform says the account is blocked or suspended.
        /// </summary>
        [JsonProperty("blocked", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? Blocked { get; set; }

        /// <summary>
        /// personal, or business: an account a company or school manages.
        /// </summary>
        [JsonProperty("account_type", Required = Newtonsoft.Json.Required.DisallowNull)]
        public AccountType? AccountType { get; set; }

        /// <summary>
        /// Whether the account has its own profile picture rather than the platform's default. The picture is not returned.
        /// </summary>
        [JsonProperty("has_avatar", Required = Newtonsoft.Json.Required.DisallowNull)]
        public bool? HasAvatar { get; set; }

        /// <summary>
        /// The first letters of the account holder's first name, as the platform shows them (Life360 shows two and hides the rest). It is evidence only: it does not change the score or the consistency results.
        /// </summary>
        [JsonProperty("first_name_prefix", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? FirstNamePrefix { get; set; }
    }
}