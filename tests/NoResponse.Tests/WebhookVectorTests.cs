namespace NoResponse.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json.Linq;
    using NoResponse.WebhookVerification;
    using Xunit;

    /// <summary>The contract's signature vector through the generated webhook verifier.</summary>
    public class WebhookVectorTests
    {
        private static readonly JObject Vector = Fixture("webhook-signature-vector.json");
        private static readonly string Payload = (string)Vector["payload"]!;
        private static readonly Dictionary<string, string> Headers = ((JObject)Vector["headers"]!).Properties().ToDictionary(p => p.Name, p => (string)p.Value!);
        private static readonly Dictionary<string, string> Secrets = new() { [(string)Vector["key_id"]!] = (string)Vector["secret_utf8"]! };
        private static readonly DateTimeOffset Now = DateTimeOffset.FromUnixTimeSeconds(long.Parse((string)Vector["timestamp"]!));

        internal static JObject Fixture(string name) =>
            JObject.Parse(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "fixtures", name)));

        [Fact]
        public void TheVectorVerifiesIntoATypedAssessmentEventFromAspNetCoreHeaders()
        {
            var request = new DefaultHttpContext().Request;
            foreach (var (name, value) in Headers) request.Headers[name] = value;
            var webhookEvent = Webhook.ConstructEvent(Encoding.UTF8.GetBytes(Payload), request.Headers, Secrets, now: Now);
            Assert.Equal("assessment.updated", webhookEvent.Type.Value);
            var assessment = Assert.IsType<NoResponse.Models.Shared.Assessment>(webhookEvent.AssessmentUpdatedEvent?.Data.Assessment);
            Assert.Equal("asm_01JA2Z8K6Q4V7W9XBCDEFGHJKM", assessment.Id);
            Assert.Equal(2, assessment.Detail.AssessmentVersion);
        }

        public static IEnumerable<object[]> Refusals()
        {
            var otherKey = new Dictionary<string, string> { ["whk_other0001"] = Secrets.Values.Single() };
            var repeated = Headers.Select(h => new KeyValuePair<string, string>(h.Key, h.Value)).Append(new("nr-webhook-signature", Headers["NR-Webhook-Signature"]));
            var missing = Headers.Where(h => h.Key != "NR-Webhook-Id");
            yield return new object[] { Payload + " ", Headers, Secrets, 0, WebhookVerificationException.InvalidSignature };
            yield return new object[] { Payload, Headers, Secrets, 301, WebhookVerificationException.InvalidTimestamp };
            yield return new object[] { Payload, Headers, otherKey, 0, WebhookVerificationException.UnknownKey };
            yield return new object[] { Payload, repeated.ToList(), Secrets, 0, WebhookVerificationException.DuplicateHeader };
            yield return new object[] { Payload, missing.ToList(), Secrets, 0, WebhookVerificationException.MissingHeader };
        }

        [Theory]
        [MemberData(nameof(Refusals))]
        public void ATamperedStaleUnknownOrRepeatedDeliveryIsRefused(string payload, IEnumerable<KeyValuePair<string, string>> headers, Dictionary<string, string> secrets, int laterSeconds, string code)
        {
            var refused = Assert.Throws<WebhookVerificationException>(() => Webhook.ConstructEvent(payload, headers, secrets, now: Now.AddSeconds(laterSeconds)));
            Assert.Equal(code, refused.Code);
        }

        public static IEnumerable<object[]> EventFixtures() =>
            Directory.GetFiles(Path.Combine(AppContext.BaseDirectory, "fixtures", "valid"), "webhook-*.json")
                .Select(Path.GetFileName)
                .Where(name => Fixture("valid/" + name)["type"] is not null)
                .OrderBy(name => name, StringComparer.Ordinal)
                .Select(name => new object[] { name! });

        [Theory]
        [MemberData(nameof(EventFixtures))]
        public void EveryContractEventFixtureParsesIntoItsTypedEvent(string name)
        {
            var body = Fixture("valid/" + name);
            var raw = Encoding.UTF8.GetBytes(body.ToString(Newtonsoft.Json.Formatting.None));
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes("fixture-secret"));
            var signature = "v1=" + Convert.ToHexString(hmac.ComputeHash(Encoding.ASCII.GetBytes(Now.ToUnixTimeSeconds() + ".").Concat(raw).ToArray())).ToLowerInvariant();
            var headers = new Dictionary<string, string>
            {
                ["NR-Webhook-Id"] = (string)body["id"]!,
                ["NR-Webhook-Timestamp"] = Now.ToUnixTimeSeconds().ToString(),
                ["NR-Webhook-Key-Id"] = "whk_fixture0001",
                ["NR-Webhook-Signature"] = signature,
            };
            var webhookEvent = Webhook.ConstructEvent(raw, headers, new Dictionary<string, string> { ["whk_fixture0001"] = "fixture-secret" }, now: Now);
            Assert.Equal((string)body["type"]!, webhookEvent.Type.Value);
            var members = new Dictionary<string, object?>
            {
                ["assessment.updated"] = webhookEvent.AssessmentUpdatedEvent,
                ["endpoint.verification"] = webhookEvent.EndpointVerificationEvent,
                ["account.deleted"] = webhookEvent.AccountDeletedEvent,
                ["workspace.deleted"] = webhookEvent.WorkspaceDeletedEvent,
                ["webhook_endpoint.disabled"] = webhookEvent.WebhookEndpointDisabledEvent,
                ["account.risk_changed"] = webhookEvent.AccountRiskChangedEvent,
                ["audit.event"] = webhookEvent.AuditEventEvent,
            };
            var member = Assert.Single(members, m => m.Value is not null);
            Assert.Equal((string)body["type"]!, member.Key);
            var typed = JObject.Parse(NoResponse.Utils.Utilities.SerializeJSON(member.Value!));
            Assert.True(JToken.DeepEquals(body, typed), $"{name}: typed event {typed} differs from the fixture");
        }
    }
}
