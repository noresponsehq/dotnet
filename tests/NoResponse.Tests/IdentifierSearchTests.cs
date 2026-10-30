namespace NoResponse.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using Xunit;

    public class IdentifierSearchTests
    {
        private sealed class Transport : NoResponseHttpClient
        {
            public readonly List<JObject> Requests = new();
            private readonly JObject revision = (JObject)JObject.Parse(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "fixtures", "investigation-responses.json")))["revision"]!;
            public readonly JObject Match = JObject.Parse("{\"kind\":\"username\",\"value\":\"JaneDoe\",\"match_basis\":\"canonical\",\"namespace\":\"customer\",\"provenance\":\"submitted\"}");

            public JArray TermMatches = new();

            public override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken? cancellationToken = null)
            {
                Assert.Equal(HttpMethod.Post, request.Method);
                Assert.Equal("Bearer nrk_test_identifier_fixture", request.Headers.Authorization?.ToString());
                Requests.Add(JObject.Parse(await request.Content!.ReadAsStringAsync()));
                Match["assessment_id"] = revision["id"];
                var item = (JObject)revision.DeepClone();
                item["matched_identifier"] = Match;
                if (request.RequestUri!.AbsolutePath == "/v1/accounts/search")
                    item = new JObject { ["account_id"] = "acct_7f3a9c", ["latest"] = revision, ["assessments"] = 2, ["matched_identifier"] = Match };
                if (Requests[^1]["identifier_expression"] != null)
                {
                    var username = (JObject)Match.DeepClone();
                    username["term_index"] = 2;
                    TermMatches = new JArray(
                        new JObject { ["kind"] = "phone", ["value"] = "+14155552671", ["match_basis"] = "canonical", ["provenance"] = "submitted", ["assessment_id"] = revision["id"], ["term_index"] = 0 },
                        username);
                    item.Remove("matched_identifier");
                    item["matched_identifiers"] = TermMatches;
                }
                var page = new JObject { ["schema_version"] = "1", ["items"] = new JArray(item), ["total"] = 1, ["total_capped"] = false };
                if (request.RequestUri!.AbsolutePath == "/v1/assessments/search") page["retention_days"] = 365;
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(page.ToString(), Encoding.UTF8, "application/json"), RequestMessage = request };
            }
        }

        [Fact]
        public async Task BothSearchesPreserveTypedIdentifierAndMatchEvidence()
        {
            var expected = JObject.Parse("{\"kind\":\"username\",\"value\":\"janedoe\",\"match\":\"canonical\",\"namespace\":\"customer\"}");
            var query = JsonConvert.DeserializeObject<IdentifierSearch>(expected.ToString())!;
            var transport = new Transport();
            var client = new NoResponseClient(apiKey: "nrk_test_identifier_fixture", serverUrl: "https://api.example.test", client: transport);
            var accounts = await client.Accounts.SearchAsync(new AccountDirectorySearchRequest { Identifier = query });
            var request = JsonConvert.DeserializeObject<AssessmentSearchRequest>("{\"from\":\"2026-10-01T00:00:00Z\",\"to\":\"2026-10-02T00:00:00Z\"}")!;
            request.Identifier = query;
            var assessments = await client.Assessments.SearchAsync(request);
            foreach (IdentifierMatch found in new[] { accounts.Result.Items[0].MatchedIdentifier!, assessments.Result.Items[0].MatchedIdentifier! })
            {
                Assert.Equal("JaneDoe", found.Value);
                Assert.Equal(transport.Match["assessment_id"]!.ToString(), found.AssessmentId);
                Assert.True(JToken.DeepEquals(transport.Match, JObject.Parse(JsonConvert.SerializeObject(found))));
            }
            Assert.Null(accounts.Result.Items[0].Current);
            Assert.Equal(2, transport.Requests.Count);
            Assert.All(transport.Requests, sent => Assert.True(JToken.DeepEquals(expected, sent["identifier"])));
        }

        [Fact]
        public async Task BothSearchesPreserveNestedExpressionsAndIndexedWitnesses()
        {
            var expected = JObject.Parse(@"{""scope"":""assessment"",""expression"":{""and"":[{""term"":{""kind"":""phone"",""value"":""+14155552671"",""match"":""canonical""}},{""or"":[{""term"":{""kind"":""email"",""value"":""other@example.com"",""match"":""exact""}},{""term"":{""kind"":""username"",""value"":""janedoe"",""match"":""canonical"",""namespace"":""customer""}}]}]}}");
            var accountExpected = (JObject)expected.DeepClone();
            accountExpected["scope"] = "account";
            var accountQuery = JsonConvert.DeserializeObject<IdentifierExpression>(accountExpected.ToString())!;
            var assessmentQuery = JsonConvert.DeserializeObject<IdentifierExpression>(expected.ToString())!;
            var transport = new Transport();
            var client = new NoResponseClient(apiKey: "nrk_test_identifier_fixture", serverUrl: "https://api.example.test", client: transport);
            var accounts = await client.Accounts.SearchAsync(new AccountDirectorySearchRequest { IdentifierExpression = accountQuery });
            var request = JsonConvert.DeserializeObject<AssessmentSearchRequest>(@"{""from"":""2026-10-01T00:00:00Z"",""to"":""2026-10-02T00:00:00Z""}")!;
            request.IdentifierExpression = assessmentQuery;
            var assessments = await client.Assessments.SearchAsync(request);
            foreach (var found in new[] { accounts.Result.Items[0].MatchedIdentifiers!, assessments.Result.Items[0].MatchedIdentifiers! })
            {
                Assert.Equal(2, found.Count);
                Assert.Equal("JaneDoe", found[1].Value);
                Assert.Equal(2, found[1].TermIndex);
                Assert.True(JToken.DeepEquals(transport.TermMatches, JArray.Parse(Utilities.SerializeJSON(found))));
            }
            Assert.Null(accounts.Result.Items[0].MatchedIdentifier);
            Assert.Null(assessments.Result.Items[0].MatchedIdentifier);
            Assert.Equal(2, transport.Requests.Count);
            Assert.True(JToken.DeepEquals(accountExpected, transport.Requests[0]["identifier_expression"]));
            Assert.True(JToken.DeepEquals(expected, transport.Requests[1]["identifier_expression"]));
        }
    }
}
