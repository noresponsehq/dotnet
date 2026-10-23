// Smoke test of the client against the mock server.
//
// Skipped unless NORESPONSE_MOCK_URL names a running mock server
// (`generate_sdks.py mockserver --serve`). The mock server answers with the
// examples in the SDK document, so this proves the client's wiring (URL, auth
// header, request encoding, response parsing) and nothing about the live API.
namespace NoResponse.Tests
{
    using System;
    using System.Threading.Tasks;
    using NoResponse.Models.Shared;
    using Xunit;

    public sealed class MockServerFactAttribute : FactAttribute
    {
        public MockServerFactAttribute()
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("NORESPONSE_MOCK_URL")))
            {
                Skip = "set NORESPONSE_MOCK_URL to a running mock server";
            }
        }
    }

    public class MockServerTests
    {
        private static NoResponseClient Client() =>
            NoResponseClient.Builder()
                .WithApiKey("nrk_mock_" + new string('k', 20))
                .WithServerUrl(Environment.GetEnvironmentVariable("NORESPONSE_MOCK_URL")!)
                .Build();

        [MockServerFact]
        public async Task CreateReturnsTheDocumentedAssessment()
        {
            var res = await Client().Assessments.CreateAsync(
                idempotencyKey: "mock-server-smoke-0001",
                body: new AssessmentRequest()
                {
                    AccountId = "acct_7f3a9c",
                    Flow = Flow.Signup,
                    Identifiers = new AssessmentRequestIdentifiers() { Email = "new.user@example.com" },
                });
            Assert.StartsWith("asm_", res.Result.Id);
            Assert.Equal(res.Result.Id, res.Result.Detail.AssessmentId);
            Assert.Equal("acct_7f3a9c", res.Result.Detail.AccountId);
        }

        [MockServerFact]
        public async Task GetReturnsTheDocumentedAssessment()
        {
            var res = await Client().Assessments.GetAsync("asm_01JA2Z8K6Q4V7W9XBCDEFGHJKM");
            Assert.StartsWith("asm_", res.Result.Id);
            Assert.Equal(res.Result.Status, res.Result.Detail.Status);
        }
    }
}
