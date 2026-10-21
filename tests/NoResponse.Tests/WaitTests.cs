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
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using Xunit;

    public class WaitTests
    {
        private const string Id = "asm_01JA2Z8K6Q4V7W9XBCDEFGHJKM";

        private sealed class Scripted : DefaultHttpClient
        {
            private readonly (string Body, string? RetryAfter)[] answers;

            public Scripted(params (string, string?)[] answers)
            {
                this.answers = answers;
            }

            public List<HttpRequestMessage> Requests { get; } = new();

            public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken? cancellationToken = null)
            {
                Requests.Add(request);
                var (body, retryAfter) = answers[Math.Min(Requests.Count, answers.Length) - 1];
                var json = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "fixtures", "valid", body));
                var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(json, Encoding.UTF8, "application/json"), RequestMessage = request };
                if (retryAfter != null)
                {
                    response.Headers.TryAddWithoutValidation("Retry-After", retryAfter);
                }
                return Task.FromResult(response);
            }
        }

        private static NoResponseSDK Client(Scripted http) =>
            NoResponseSDK.Builder().WithApiKey("nrk_test_" + new string('k', 20)).WithServerUrl("https://api.example.test").WithClient(http).Build();

        [Fact]
        public async Task PollsUntilCompleteHonouringRetryAfter()
        {
            var http = new Scripted(("assessment-pending.json", "3"), ("assessment-pending.json", "0"), ("assessment-complete.json", null));
            var slept = new List<TimeSpan>();
            var now = TimeSpan.Zero;
            var result = await Client(http).WaitAsync(Id, delay: (d, _) => { slept.Add(d); now += d; return Task.CompletedTask; }, clock: () => now);
            Assert.Equal(Status.Complete, result.Status);
            Assert.Equal(new[] { TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(1) }, slept);
            Assert.Equal(3, http.Requests.Count);
            Assert.Equal("/v1/assessments/" + Id, http.Requests[0].RequestUri!.AbsolutePath);
            Assert.Equal("Bearer nrk_test_" + new string('k', 20), http.Requests[0].Headers.Authorization!.ToString());
        }

        [Fact]
        public async Task TimesOutWithTheLastPendingAssessment()
        {
            var http = new Scripted(("assessment-pending.json", "2"));
            var slept = new List<TimeSpan>();
            var now = TimeSpan.Zero;
            var e = await Assert.ThrowsAsync<WaitTimeoutException>(() => Client(http).WaitAsync(Id, TimeSpan.FromSeconds(5), delay: (d, _) => { slept.Add(d); now += d; return Task.CompletedTask; }, clock: () => now));
            Assert.Equal(Status.Pending, e.Last.Status);
            Assert.Equal(new[] { TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2) }, slept);
        }
    }
}
