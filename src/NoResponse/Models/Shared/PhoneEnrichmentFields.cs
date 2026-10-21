#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System.Collections.Generic;

    public class PhoneEnrichmentFields
    {
        [JsonProperty("phone", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Phone { get; set; }

        [JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Name { get; set; }

        [JsonProperty("carrier", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Carrier { get; set; }

        [JsonProperty("line_type", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? LineType { get; set; }

        [JsonProperty("entity_type", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? EntityType { get; set; }

        [JsonProperty("country_code", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? CountryCode { get; set; }

        [JsonProperty("country", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Country { get; set; }

        [JsonProperty("state", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? State { get; set; }

        [JsonProperty("city", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? City { get; set; }

        [JsonProperty("location", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Location { get; set; }

        [JsonProperty("national_format", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? NationalFormat { get; set; }

        [JsonProperty("job_title", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? JobTitle { get; set; }

        [JsonProperty("company_name", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? CompanyName { get; set; }

        [JsonProperty("email", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Email { get; set; }

        [JsonProperty("website", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? Website { get; set; }

        [JsonProperty("first_call_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? FirstCallAt { get; set; }

        [JsonProperty("last_call_at", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? LastCallAt { get; set; }

        [JsonProperty("matched_name", Required = Newtonsoft.Json.Required.DisallowNull)]
        public string? MatchedName { get; set; }

        [JsonProperty("alternate_names", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<AlternateNameUnion>? AlternateNames { get; set; }

        [JsonProperty("name_candidates", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<NameCandidateUnion>? NameCandidates { get; set; }

        [JsonProperty("timezones", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<TimezoneUnion>? Timezones { get; set; }

        [JsonProperty("emails", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<EmailUnion>? Emails { get; set; }

        [JsonProperty("websites", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<WebsiteUnion>? Websites { get; set; }

        [JsonProperty("locations", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<LocationUnion>? Locations { get; set; }

        [JsonProperty("social_profiles", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<SocialProfileUnion>? SocialProfiles { get; set; }

        [JsonProperty("report_count", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? ReportCount { get; set; }

        [JsonProperty("tag_count", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? TagCount { get; set; }

        [JsonProperty("total_calls", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? TotalCalls { get; set; }

        [JsonProperty("calls_60_days", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Calls60Days { get; set; }

        [JsonProperty("reports_60_days", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Reports60Days { get; set; }

        [JsonProperty("searches_60_days", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? Searches60Days { get; set; }

        [JsonProperty("comment_count", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? CommentCount { get; set; }

        [JsonProperty("names_count", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? NamesCount { get; set; }

        [JsonProperty("spam_report_count", Required = Newtonsoft.Json.Required.DisallowNull)]
        public long? SpamReportCount { get; set; }
    }
}