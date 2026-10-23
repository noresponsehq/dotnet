namespace NoResponse.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using NoResponse.Models.Errors;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using NoResponse.Utils.Retries;
    using Xunit;

    public class ClientTests
    {
        private sealed class Scripted : NoResponseHttpClient
        {
            private readonly Func<int, (HttpStatusCode Status, string Body, string Type)> answer;

            public Scripted(Func<int, (HttpStatusCode, string, string)> answer)
            {
                this.answer = answer;
            }

            public List<string?> Keys { get; } = new();

            public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken? cancellationToken = null)
            {
                Keys.Add(request.Headers.TryGetValues("Idempotency-Key", out var values) ? values.Single() : null);
                var (status, body, type) = answer(Keys.Count);
                return Task.FromResult(new HttpResponseMessage(status) { Content = new StringContent(body, Encoding.UTF8, type), RequestMessage = request });
            }
        }

        private static string Fixture(string name) =>
            File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "fixtures", "valid", name));

        private static NoResponseClient Client(Scripted http) =>
            new NoResponseClient(apiKey: "nrk_test_" + new string('k', 20), serverUrl: "https://api.example.test", client: http);

        private static AssessmentRequest Request() => new AssessmentRequest()
        {
            AccountId = "acct_7f3a9c",
            Identifiers = new AssessmentRequestIdentifiers() { Email = "new.user@example.com" },
        };

        [Fact]
        public async Task CreateSendsAGeneratedIdempotencyKey()
        {
            var http = new Scripted(_ => (HttpStatusCode.OK, Fixture("assessment-complete.json"), "application/json"));
            var client = Client(http);
            await client.Assessments.CreateAsync(Request());
            await client.Assessments.CreateAsync(Request());
            Assert.All(http.Keys, key => Assert.Matches(new Regex("^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$"), key));
            Assert.NotEqual(http.Keys[0], http.Keys[1]);
        }

        [Fact]
        public async Task CreateKeepsTheCallersIdempotencyKey()
        {
            var http = new Scripted(_ => (HttpStatusCode.OK, Fixture("assessment-complete.json"), "application/json"));
            await Client(http).Assessments.CreateAsync(Request(), idempotencyKey: "signup-usr_8f2k1-0001");
            Assert.Equal("signup-usr_8f2k1-0001", http.Keys.Single());
        }

        [Fact]
        public async Task ARetrySendsTheSameIdempotencyKey()
        {
            var http = new Scripted(n => n == 1
                ? (HttpStatusCode.ServiceUnavailable, "{}", "application/problem+json")
                : (HttpStatusCode.OK, Fixture("assessment-complete.json"), "application/json"));
            var retry = new RetryConfig(RetryConfig.RetryStrategy.BACKOFF, new BackoffStrategy(1L, 5L, 1000L, 1.1), false);
            await Client(http).Assessments.CreateAsync(Request(), retryConfig: retry);
            Assert.Equal(2, http.Keys.Count);
            Assert.Equal(http.Keys[0], http.Keys[1]);
        }

        private sealed class Authorizations : NoResponseHttpClient
        {
            public List<string?> Seen { get; } = new();

            public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken? cancellationToken = null)
            {
                Seen.Add(request.Headers.Authorization?.ToString());
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(Fixture("assessment-complete.json"), Encoding.UTF8, "application/json"), RequestMessage = request });
            }
        }

        // NORESPONSE_API_KEY is process-wide: only this test sets it, and it puts the old value back.
        [Fact]
        public async Task TheKeyFallsBackToTheEnvironment()
        {
            var saved = Environment.GetEnvironmentVariable("NORESPONSE_API_KEY");
            var envKey = "nrk_test_" + new string('e', 20);
            try
            {
                Environment.SetEnvironmentVariable("NORESPONSE_API_KEY", envKey);
                var http = new Authorizations();
                await new NoResponseClient(serverUrl: "https://api.example.test", client: http).Assessments.GetAsync("asm_01JA2Z8K6Q4V7W9XBCDEFGHJKM");
                await NoResponseClient.Builder().WithServerUrl("https://api.example.test").WithClient(http).Build().Assessments.GetAsync("asm_01JA2Z8K6Q4V7W9XBCDEFGHJKM");
                var passed = "nrk_test_" + new string('p', 20);
                await new NoResponseClient(apiKey: passed, serverUrl: "https://api.example.test", client: http).Assessments.GetAsync("asm_01JA2Z8K6Q4V7W9XBCDEFGHJKM");
                Assert.Equal(new[] { "Bearer " + envKey, "Bearer " + envKey, "Bearer " + passed }, http.Seen);

                Environment.SetEnvironmentVariable("NORESPONSE_API_KEY", null);
                Assert.Throws<ArgumentException>(() => new NoResponseClient(serverUrl: "https://api.example.test", client: http));
                Assert.Throws<ArgumentException>(() => NoResponseClient.Builder().WithClient(http).Build());
                Environment.SetEnvironmentVariable("NORESPONSE_API_KEY", "");
                Assert.Throws<ArgumentException>(() => new NoResponseClient(serverUrl: "https://api.example.test", client: http));
            }
            finally
            {
                Environment.SetEnvironmentVariable("NORESPONSE_API_KEY", saved);
            }
        }

        [Fact]
        public async Task EveryApiErrorIsANoResponseException()
        {
            var problem = "{\"type\":\"https://docs.noresponse.org/errors/not_found\",\"title\":\"Not found\",\"status\":404,\"detail\":\"No such assessment.\",\"code\":\"not_found\",\"request_id\":\"" + new string('a', 32) + "\"}";
            var http = new Scripted(_ => (HttpStatusCode.NotFound, problem, "application/problem+json"));
            var error = await Assert.ThrowsAsync<NotFoundException>(() => Client(http).Assessments.GetAsync("asm_01JA2Z8K6Q4V7W9XBCDEFGHJKM"));
            Assert.IsAssignableFrom<NoResponseException>(error);
            Assert.Equal(404, error.StatusCode);
            Assert.Equal(new string('a', 32), error.Payload.RequestId);
        }
    }
}
