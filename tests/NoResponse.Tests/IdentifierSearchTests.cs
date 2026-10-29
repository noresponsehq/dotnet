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
    }
}
