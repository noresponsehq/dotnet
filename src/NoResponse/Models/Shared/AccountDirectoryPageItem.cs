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

    public class AccountDirectoryPageItem
    {
        /// <summary>
        /// Present for needs_action searches. outcome means a current review/block decision lacks a retained effective outcome; review means a retained open or in-progress review. Both can apply.
        /// </summary>
        [JsonProperty("waiting_for", Required = Newtonsoft.Json.Required.DisallowNull)]
        public List<WaitingFor>? WaitingFor { get; set; }

        /// <summary>
        /// Your own opaque identifier for the account being checked, scoped to your workspace. Use an id you mint, with no direct identifier (email, phone, name or national id) in it. '|' is accepted so an Auth0 user id such as 'auth0|64abc' passes unchanged; '@', '+', '$' and whitespace are refused by the pattern, as is an exact-shape JWT; a hex digest such as a SHA-256 is allowed, because hashing an internal id is a common pseudonymous scheme. A value such as '07700900123' or 'jane.doe.gmail.com' cannot be told apart from an opaque id. It travels in bodies, and in a URL only as the path segment of GET /v1/accounts/{account_id} and its /assessments. An assessment requested by a provider hook at pre_registration, which has no account yet, carries the server-issued account_id pre: followed by its assessment_id.
        /// </summary>
        [JsonProperty("account_id", Required = Newtonsoft.Json.Required.Always)]
        public string AccountId { get; set; } = default!;

        /// <summary>
        /// Retrieve a retained assessment version with its original decision, policy, flow and depth. This response excludes the current account decision. Current retention and response-mode permissions still apply.
        /// </summary>
        [JsonProperty("latest", Required = Newtonsoft.Json.Required.Always)]
        public AssessmentRevision Latest { get; set; } = new();

        /// <summary>
        /// Body of the 200 answer to GET /v1/accounts/{account_id}: the one current risk of one of the calling workspace's accounts. It is the risk of the account's latest assessment that has a decision (status complete or partial), by created_at and then assessment_id, scored exactly as GET /v1/assessments/{assessment_id} scores that assessment, plus the account's own reason shared_api_key when a key issued to the account was used from many IP addresses or countries (POST /v1/key_usage/batches). Its assessments are the account's history (GET /v1/accounts/{account_id}/assessments).
        /// </summary>
        [JsonProperty("current", Required = Newtonsoft.Json.Required.DisallowNull)]
        public Account? Current { get; set; }

        /// <summary>
        /// Count of admitted retained assessments belonging to this account, not just the returned page.
        /// </summary>
        [JsonProperty("assessments", Required = Newtonsoft.Json.Required.Always)]
        public long Assessments { get; set; } = default!;
    }
}