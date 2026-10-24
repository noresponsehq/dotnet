namespace NoResponse.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
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

        private static NoResponseClient Client(NoResponseHttpClient http) =>
            new NoResponseClient(apiKey: "nrk_test_" + new string('k', 20), serverUrl: "https://api.example.test", client: http);

        private static AssessmentRequest Request() => new AssessmentRequest()
        {
            AccountId = "acct_7f3a9c",
            Flow = Flow.AccountCreation,
            Identifiers = new AssessmentRequestIdentifiers() { Email = "new.user@example.com" },
        };

        [Fact]
        public async Task CreateSendsAGeneratedIdempotencyKey()
        {
            var http = new Scripted(_ => (HttpStatusCode.OK, Fixture("assessment-view-complete.json"), "application/json"));
            var client = Client(http);
            await client.Assessments.CreateAsync(Request());
            await client.Assessments.CreateAsync(Request());
            Assert.All(http.Keys, key => Assert.Matches(new Regex("^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$"), key));
            Assert.NotEqual(http.Keys[0], http.Keys[1]);
        }

        [Fact]
        public async Task CreateKeepsTheCallersIdempotencyKey()
        {
            var http = new Scripted(_ => (HttpStatusCode.OK, Fixture("assessment-view-complete.json"), "application/json"));
            await Client(http).Assessments.CreateAsync(Request(), idempotencyKey: "account-usr_8f2k1-0001");
            Assert.Equal("account-usr_8f2k1-0001", http.Keys.Single());
        }

        [Fact]
        public async Task ARetrySendsTheSameIdempotencyKey()
        {
            var http = new Scripted(n => n == 1
                ? (HttpStatusCode.ServiceUnavailable, "{}", "application/problem+json")
                : (HttpStatusCode.OK, Fixture("assessment-view-complete.json"), "application/json"));
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
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(Fixture("assessment-view-complete.json"), Encoding.UTF8, "application/json"), RequestMessage = request });
            }
        }

        // NORESPONSE_API_KEY is process-wide: only this test sets it, and it puts the old value back.
        [Fact]
        public async Task TheKeyFallsBackToTheEnvironment()
        {
            var saved = System.Environment.GetEnvironmentVariable("NORESPONSE_API_KEY");
            var envKey = "nrk_test_" + new string('e', 20);
            try
            {
                System.Environment.SetEnvironmentVariable("NORESPONSE_API_KEY", envKey);
                var http = new Authorizations();
                await new NoResponseClient(serverUrl: "https://api.example.test", client: http).Assessments.GetAsync("asm_01JA2Z8K6Q4V7W9XBCDEFGHJKM");
                await NoResponseClient.Builder().WithServerUrl("https://api.example.test").WithClient(http).Build().Assessments.GetAsync("asm_01JA2Z8K6Q4V7W9XBCDEFGHJKM");
                var passed = "nrk_test_" + new string('p', 20);
                await new NoResponseClient(apiKey: passed, serverUrl: "https://api.example.test", client: http).Assessments.GetAsync("asm_01JA2Z8K6Q4V7W9XBCDEFGHJKM");
                Assert.Equal(new[] { "Bearer " + envKey, "Bearer " + envKey, "Bearer " + passed }, http.Seen);

                System.Environment.SetEnvironmentVariable("NORESPONSE_API_KEY", null);
                Assert.Throws<ArgumentException>(() => new NoResponseClient(serverUrl: "https://api.example.test", client: http));
                Assert.Throws<ArgumentException>(() => NoResponseClient.Builder().WithClient(http).Build());
                System.Environment.SetEnvironmentVariable("NORESPONSE_API_KEY", "");
                Assert.Throws<ArgumentException>(() => new NoResponseClient(serverUrl: "https://api.example.test", client: http));
            }
            finally
            {
                System.Environment.SetEnvironmentVariable("NORESPONSE_API_KEY", saved);
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

        private sealed class Hanging : NoResponseHttpClient
        {
            public int Attempts;

            public override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken? cancellationToken = null)
            {
                Interlocked.Increment(ref Attempts);
                await Task.Delay(Timeout.Infinite, cancellationToken ?? CancellationToken.None);
                throw new InvalidOperationException("unreachable");
            }
        }

        [Fact]
        public async Task ATimedOutRequestFailsWithinItsTimeout()
        {
            var http = new Hanging();
            using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(1));
            var clock = Stopwatch.StartNew();
            var request = Client(http).Assessments.CreateAsync(Request(), cancellationToken: timeout.Token);
            Assert.Same(request, await Task.WhenAny(request, Task.Delay(TimeSpan.FromSeconds(10))));
            await Assert.ThrowsAnyAsync<OperationCanceledException>(() => request);
            Assert.InRange(clock.Elapsed, TimeSpan.FromSeconds(0.9), TimeSpan.FromSeconds(3));
            Assert.Equal(1, http.Attempts);
        }

        [Fact]
        public async Task ATimeoutStopsTheBackoffBetweenRetries()
        {
            var http = new Scripted(_ => (HttpStatusCode.ServiceUnavailable, "{}", "application/problem+json"));
            var retry = new RetryConfig(RetryConfig.RetryStrategy.BACKOFF, new BackoffStrategy(5000L, 5000L, 20000L, 1.0), false);
            using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(1));
            var clock = Stopwatch.StartNew();
            await Assert.ThrowsAnyAsync<OperationCanceledException>(() => Client(http).Assessments.CreateAsync(Request(), retryConfig: retry, cancellationToken: timeout.Token));
            Assert.InRange(clock.Elapsed, TimeSpan.FromSeconds(0.9), TimeSpan.FromSeconds(3));
            Assert.Single(http.Keys);
        }
    }
}
