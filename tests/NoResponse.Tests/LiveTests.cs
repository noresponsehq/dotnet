// Consumer smoke test of the client against a live API.
//
// Skipped unless NORESPONSE_LIVE_URL and NORESPONSE_LIVE_API_KEY name an API
// and a key with the assess and history scopes (a test key keeps it off real
// sources). It assesses a synthetic account, replays the request, reads it
// back, waits for it to settle, lists its history and checks the typed 404.
// NORESPONSE_LIVE_WEBHOOK_VECTORS names deliveries the same API signed, in
// the shape of webhook-signature-vector.json, for the verifier. Each step
// writes its name to NORESPONSE_LIVE_REPORT, and "done" at the end, so a
// failure names the operation (integrations/regression/suites/sdks.sh).
namespace NoResponse.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using NoResponse.Models.Errors;
    using NoResponse.Models.Shared;
    using NoResponse.WebhookVerification;
    using Xunit;

    public sealed class LiveFactAttribute : FactAttribute
    {
        public LiveFactAttribute()
        {
            if (string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("NORESPONSE_LIVE_URL")) ||
                string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("NORESPONSE_LIVE_API_KEY")))
            {
                Skip = "set NORESPONSE_LIVE_URL and NORESPONSE_LIVE_API_KEY to run against a live API";
            }
        }
    }

    public class LiveTests
    {
        private static void Step(string name)
        {
            var report = System.Environment.GetEnvironmentVariable("NORESPONSE_LIVE_REPORT");
            if (!string.IsNullOrEmpty(report)) File.WriteAllText(report, name);
        }

        [LiveFact]
        public async Task AConsumerAssessesWaitsForAndListsAnAccount()
        {
            var client = NoResponseClient.Builder()
                .WithApiKey(System.Environment.GetEnvironmentVariable("NORESPONSE_LIVE_API_KEY")!)
                .WithServerUrl(System.Environment.GetEnvironmentVariable("NORESPONSE_LIVE_URL")!)
                .Build();
            var run = System.Environment.GetEnvironmentVariable("NORESPONSE_LIVE_RUN") ?? Guid.NewGuid().ToString("N")[..8];
            var accountId = "sdk-live-csharp-" + run;
            var idempotencyKey = "sdk-live-" + Guid.NewGuid().ToString("N");
            var request = new AssessmentRequest()
            {
                AccountId = accountId,
                Flow = Flow.AccountCreation,
                Identifiers = new AssessmentRequestIdentifiers() { Email = "sdk-live@example.com" },
            };

            Step("create");
            var created = (await client.Assessments.CreateAsync(idempotencyKey: idempotencyKey, body: request)).Result;
            Assert.StartsWith("asm_", created.Id);
            Assert.Equal(accountId, created.Detail.AccountId);

            Step("replay");
            Assert.Equal(created.Id, (await client.Assessments.CreateAsync(idempotencyKey: idempotencyKey, body: request)).Result.Id);

            Step("get");
            Assert.Equal(created.Id, (await client.Assessments.GetAsync(created.Id)).Result.Id);

            Step("wait");
            var settled = await client.Assessments.WaitAsync(created.Id, TimeSpan.FromMinutes(1));
            Assert.Contains(settled.Status, new[] { Status.Complete, Status.Partial });
            var decided = Assert.IsType<Account>(settled.Account);

            Step("query");
            var history = (await client.Assessments.QueryAsync(new AssessmentQuery() { AccountId = accountId, Limit = 5 })).Result.Items;
            Assert.Contains(history, item => item.AssessmentId == created.Id);
            Assert.All(history, item => Assert.Equal(accountId, item.AccountId));

            Step("list_account_assessments");
            var page = (await client.Accounts.ListAssessmentsAsync(accountId, limit: 5)).Result.Items;
            Assert.Contains(page, item => item.AssessmentId == created.Id);

            Step("get_account");
            var account = (await client.Accounts.GetAsync(accountId)).Result;
            Assert.Equal(accountId, account.Id);
            Assert.Equal(decided.Risk.Decision.Value, account.Risk.Decision.Value);

            Step("not_found");
            var missing = await Assert.ThrowsAsync<NotFoundException>(() => client.Assessments.GetAsync("asm_" + new string('0', 26)));
            Assert.Equal(404, missing.Payload.Status);
            Assert.False(string.IsNullOrEmpty(missing.Payload.RequestId));

            var vectors = System.Environment.GetEnvironmentVariable("NORESPONSE_LIVE_WEBHOOK_VECTORS");
            if (!string.IsNullOrEmpty(vectors))
            {
                foreach (JObject vector in JArray.Parse(File.ReadAllText(vectors)))
                {
                    var type = (string)vector["type"]!;
                    Step("verify_webhook " + type);
                    var payload = (string)vector["payload"]!;
                    var headers = ((JObject)vector["headers"]!).Properties().ToDictionary(p => p.Name, p => (string)p.Value!);
                    var secrets = new Dictionary<string, string> { [(string)vector["key_id"]!] = (string)vector["secret_utf8"]! };
                    var now = DateTimeOffset.FromUnixTimeSeconds(long.Parse((string)vector["timestamp"]!));
                    Assert.Equal(type, Webhook.ConstructEvent(payload, headers, secrets, now: now).Type.Value);
                    var refused = Assert.Throws<WebhookVerificationException>(() => Webhook.ConstructEvent(payload + " ", headers, secrets, now: now));
                    Assert.Equal(WebhookVerificationException.InvalidSignature, refused.Code);
                }
            }
            Step("done");
        }
    }
}
