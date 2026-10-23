namespace NoResponse.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;
    using Newtonsoft.Json.Linq;
    using NoResponse.Models.Shared;
    using NoResponse.Webhooks;
    using Xunit;

    public class WebhookTests
    {
        private static readonly DateTimeOffset Now = DateTimeOffset.FromUnixTimeSeconds(1791968402);

        internal static JObject Fixture(string name) =>
            JObject.Parse(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "fixtures", name)));

        private static List<KeyValuePair<string, IEnumerable<string>>> Headers(IEnumerable<KeyValuePair<string, string>> single) =>
            single.Select(h => new KeyValuePair<string, IEnumerable<string>>(h.Key, new[] { h.Value })).ToList();

        private static (byte[] Payload, List<KeyValuePair<string, IEnumerable<string>>> Headers, Dictionary<string, string> Secrets) Vector()
        {
            var v = Fixture("webhook-signature-vector.json");
            var headers = ((JObject)v["headers"]!).Properties().Select(p => new KeyValuePair<string, string>(p.Name, (string)p.Value!));
            return (Encoding.UTF8.GetBytes((string)v["payload"]!), Headers(headers), new Dictionary<string, string> { [(string)v["key_id"]!] = (string)v["secret_utf8"]! });
        }

        private static (byte[] Payload, List<KeyValuePair<string, IEnumerable<string>>> Headers) Signed(JObject body, string secret, string id, string keyId = "whk_sdktest01")
        {
            var raw = Encoding.UTF8.GetBytes(body.ToString(Newtonsoft.Json.Formatting.None));
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
            var digest = Convert.ToHexString(hmac.ComputeHash(Encoding.ASCII.GetBytes("1791968402.").Concat(raw).ToArray())).ToLowerInvariant();
            return (raw, Headers(new Dictionary<string, string>
            {
                ["NR-Webhook-Id"] = id,
                ["NR-Webhook-Timestamp"] = "1791968402",
                ["NR-Webhook-Key-Id"] = keyId,
                ["NR-Webhook-Signature"] = "v1=" + digest,
            }));
        }

        private static void Refused(WebhookErrorCode code, byte[] payload, IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers, IReadOnlyDictionary<string, string> secrets, DateTimeOffset? now = null)
        {
            var e = Assert.Throws<WebhookVerificationException>(() => Webhook.ConstructEvent(payload, headers, secrets, now: now ?? Now));
            Assert.Equal(code, e.Code);
        }

        [Fact]
        public void SharedVectorYieldsTypedAssessment()
        {
            var (payload, headers, secrets) = Vector();
            var ev = Webhook.ConstructEvent(payload, headers, secrets, now: Now);
            Assert.Equal("assessment.updated", ev.Type);
            Assert.Equal("evt_01JA2ZB4N5P6Q7R8S9T0VWXYZ1", ev.Id);
            Assert.Equal("2026-10-14T09:00:02.000Z", ev.CreatedAt);
            var assessment = Assert.IsType<Assessment>(ev.Data);
            Assert.Equal("asm_01JA2Z8K6Q4V7W9XBCDEFGHJKM", assessment.Id);
            Assert.Equal(Status.Complete, assessment.Status);
            Assert.Equal(AccountDecision.StepUp, assessment.Account!.Risk.Decision);
            Assert.Equal(2, assessment.Detail.AssessmentVersion);
            Assert.Equal(Recommendation.StepUp, assessment.Detail.Risk!.Recommendation);
            Assert.False(ev.IsExpired);
        }

        [Fact]
        public void Refusals()
        {
            var (payload, headers, secrets) = Vector();
            Refused(WebhookErrorCode.InvalidSignature, payload.Concat(new[] { (byte)' ' }).ToArray(), headers, secrets);
            Refused(WebhookErrorCode.InvalidTimestamp, payload, headers, secrets, Now.AddSeconds(301));
            Refused(WebhookErrorCode.InvalidTimestamp, payload, headers, secrets, Now.AddSeconds(-301));
            Assert.Equal("evt_01JA2ZB4N5P6Q7R8S9T0VWXYZ1", Webhook.ConstructEvent(payload, headers, secrets, TimeSpan.FromSeconds(600), Now.AddSeconds(301)).Id);

            var id = headers.First(h => h.Key == "NR-Webhook-Id").Value.Single();
            Refused(WebhookErrorCode.DuplicateHeader, payload, headers.Append(new("nr-webhook-ID", new[] { id })), secrets);
            Refused(WebhookErrorCode.DuplicateHeader, payload, headers.Select(h => h.Key == "NR-Webhook-Id" ? new KeyValuePair<string, IEnumerable<string>>(h.Key, new[] { id, id }) : h), secrets);
            Refused(WebhookErrorCode.MissingHeader, payload, headers.Where(h => h.Key != "NR-Webhook-Signature"), secrets);
            Refused(WebhookErrorCode.MissingHeader, payload, headers.Select(h => h.Key == "NR-Webhook-Signature" ? new KeyValuePair<string, IEnumerable<string>>(h.Key, new[] { "" }) : h), secrets);
            Refused(WebhookErrorCode.MissingHeader, payload, headers.Select(h => h.Key == "NR-Webhook-Signature" ? new KeyValuePair<string, IEnumerable<string>>(h.Key, Array.Empty<string>()) : h), secrets);
            Refused(WebhookErrorCode.DuplicateHeader, payload, headers.Prepend(new KeyValuePair<string, IEnumerable<string>>("nr-webhook-signature", Array.Empty<string>())), secrets);
            Refused(WebhookErrorCode.UnknownKey, payload, headers, new Dictionary<string, string> { ["whk_other0001"] = "x" });
            Refused(WebhookErrorCode.InvalidSignature, payload, headers, new Dictionary<string, string> { [secrets.Keys.Single()] = "other-secret" });
            Refused(WebhookErrorCode.InvalidSignature, payload, headers.Select(h => h.Key == "NR-Webhook-Signature" ? new KeyValuePair<string, IEnumerable<string>>(h.Key, new[] { "v2=abc" }) : h), secrets);
        }

        [Fact]
        public void RotationAndEventId()
        {
            var complete = Fixture("valid/assessment-view-complete.json");
            var ev = new JObject { ["id"] = "evt_01JA2ZB4N5P6Q7R8S9T0VWXYZ2", ["type"] = "assessment.updated", ["created_at"] = "2026-10-18T11:00:04Z", ["data"] = complete };
            var both = new Dictionary<string, string> { ["whk_sdktest01"] = "old-secret", ["whk_sdktest02"] = "new-secret" };
            var (raw, headers) = Signed(ev, "new-secret", "evt_01JA2ZB4N5P6Q7R8S9T0VWXYZ2", "whk_sdktest02");
            Assert.Equal((string)complete["id"]!, Assert.IsType<Assessment>(Webhook.ConstructEvent(raw, headers, both, now: Now).Data).Id);
            (raw, headers) = Signed(ev, "new-secret", "evt_01JA2ZB4N5P6Q7R8S9T0VWXYZ9", "whk_sdktest02");
            Refused(WebhookErrorCode.InvalidPayload, raw, headers, both);
            var pointer = (JObject)ev.DeepClone();
            pointer["data"] = new JObject { ["assessment_id"] = complete["id"], ["assessment_url"] = "/v1/assessments/x" };
            (raw, headers) = Signed(pointer, "new-secret", "evt_01JA2ZB4N5P6Q7R8S9T0VWXYZ2", "whk_sdktest02");
            Refused(WebhookErrorCode.InvalidPayload, raw, headers, both);
        }

        [Fact]
        public void ExpiredAndVerificationEvents()
        {
            var secrets = new Dictionary<string, string> { ["whk_sdktest01"] = "secret" };
            var expired = Fixture("valid/webhook-assessment-expired.json");
            var (raw, headers) = Signed(expired, "secret", (string)expired["id"]!);
            var ev = Webhook.ConstructEvent(raw, headers, secrets, now: Now);
            Assert.True(ev.IsExpired);
            Assert.Equal(new AssessmentExpired((string)expired["data"]!["assessment_id"]!, (string)expired["data"]!["account_id"]!, 2), ev.Data);
            Assert.Throws<WebhookVerificationException>(() => Webhook.VerificationResponse(ev));

            var ping = Fixture("valid/webhook-endpoint-verification.json");
            (raw, headers) = Signed(ping, "secret", (string)ping["id"]!);
            var verification = Webhook.ConstructEvent(raw, headers, secrets, now: Now);
            Assert.Equal("endpoint.verification", verification.Type);
            Assert.Equal(new EndpointVerification((string)ping["data"]!["endpoint_id"]!, (string)ping["data"]!["challenge"]!), verification.Data);
            Assert.Equal((string)ping["data"]!["challenge"]!, Webhook.VerificationResponse(verification)["challenge"]);
        }

        [Fact]
        public void AccountDeletedEvent()
        {
            var secrets = new Dictionary<string, string> { ["whk_sdktest01"] = "secret" };
            var erased = Fixture("valid/webhook-account-deleted.json");
            var (raw, headers) = Signed(erased, "secret", (string)erased["id"]!);
            var ev = Webhook.ConstructEvent(raw, headers, secrets, now: Now);
            Assert.Equal("account.deleted", ev.Type);
            Assert.False(ev.IsExpired);
            Assert.False(ev.Test);
            var deleted = Assert.IsType<AccountDeleted>(ev.Data);
            Assert.Equal("acct_7f3a9c", deleted.AccountId);
            Assert.Equal(3, deleted.AssessmentsDeleted);
            Assert.Equal(DateTimeOffset.Parse("2026-10-23T09:00:00Z", System.Globalization.CultureInfo.InvariantCulture), DateTimeOffset.Parse(deleted.DeletedAt, System.Globalization.CultureInfo.InvariantCulture));
            Assert.Throws<WebhookVerificationException>(() => Webhook.VerificationResponse(ev));
            foreach (var mutate in new Action<JObject>[]
            {
                data => data["assessments_deleted"] = 0,
                data => data["assessments_deleted"] = 1.5,
                data => data["account_id"] = "jane@example.com",
                data => data["deleted_at"] = "tomorrow",
            })
            {
                var bad = Fixture("valid/webhook-account-deleted.json");
                mutate((JObject)bad["data"]!);
                var (payload, badHeaders) = Signed(bad, "secret", (string)bad["id"]!);
                Refused(WebhookErrorCode.InvalidPayload, payload, badHeaders, secrets);
            }
        }

        // ASP.NET Core's Request.Headers enumerates KeyValuePair<string, StringValues>,
        // a struct value type; this stands in for StringValues.
        private readonly struct HeaderValues : IEnumerable<string>
        {
            private readonly string[] values;

            public HeaderValues(params string[] values) => this.values = values;

            public IEnumerator<string> GetEnumerator() => ((IEnumerable<string>)values).GetEnumerator();

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Fact]
        public void HeadersWithAStructValueTypeVerify()
        {
            var (payload, headers, secrets) = Vector();
            var aspNetStyle = headers.ToDictionary(h => h.Key, h => new HeaderValues(h.Value.ToArray()));
            Assert.Equal(Webhook.ConstructEvent(payload, headers, secrets, now: Now).Id,
                Webhook.ConstructEvent(payload, aspNetStyle, secrets, now: Now).Id);
        }

        [Fact]
        public void AspNetCoreRequestHeadersVerify()
        {
            var (payload, headers, secrets) = Vector();
            var request = new DefaultHttpContext().Request;
            foreach (var header in headers)
            {
                request.Headers[header.Key] = new StringValues(header.Value.ToArray());
            }
            Assert.Equal("evt_01JA2ZB4N5P6Q7R8S9T0VWXYZ1", Webhook.ConstructEvent(payload, request.Headers, secrets, now: Now).Id);

            request.Headers["NR-Webhook-Signature"] = StringValues.Empty;
            var empty = Assert.Throws<WebhookVerificationException>(() => Webhook.ConstructEvent(payload, request.Headers, secrets, now: Now));
            Assert.Equal(WebhookErrorCode.MissingHeader, empty.Code);
            request.Headers["NR-Webhook-Signature"] = new StringValues((string?)null);
            var nullValue = Assert.Throws<WebhookVerificationException>(() => Webhook.ConstructEvent(payload, request.Headers, secrets, now: Now));
            Assert.Equal(WebhookErrorCode.MissingHeader, nullValue.Code);
        }

        [Fact]
        public void ATestEventSaysSoAndARealOneDoesNot()
        {
            var secrets = new Dictionary<string, string> { ["whk_sdktest01"] = "secret" };
            var sample = Fixture("valid/webhook-assessment-updated-test.json");
            var (raw, headers) = Signed(sample, "secret", (string)sample["id"]!);
            var ev = Webhook.ConstructEvent(raw, headers, secrets, now: Now);
            Assert.True(ev.Test);
            Assert.IsType<Assessment>(ev.Data);

            var real = Fixture("valid/webhook-assessment-updated.json");
            (raw, headers) = Signed(real, "secret", (string)real["id"]!);
            Assert.False(Webhook.ConstructEvent(raw, headers, secrets, now: Now).Test);

            var ping = Fixture("valid/webhook-endpoint-verification.json");
            (raw, headers) = Signed(ping, "secret", (string)ping["id"]!);
            Assert.False(Webhook.ConstructEvent(raw, headers, secrets, now: Now).Test);
        }

        // The API adds fields without a new version, so keys this SDK does not
        // know must not stop a signed event from verifying.
        [Fact]
        public void UnknownFieldsAreAccepted()
        {
            var secrets = new Dictionary<string, string> { ["whk_sdktest01"] = "secret" };
            foreach (var name in new[]
            {
                "invalid/webhook-tenant-id.json",
                "invalid/webhook-identifier-leak.json",
                "invalid/webhook-expired-with-assessment-url.json",
                "invalid/webhook-account-deleted-assessment-id.json",
            })
            {
                var body = Fixture(name);
                var (payload, headers) = Signed(body, "secret", (string)body["id"]!);
                Assert.Equal((string)body["id"]!, Webhook.ConstructEvent(payload, headers, secrets, now: Now).Id);
            }
        }

        [Fact]
        public void ContractInvalidFixturesAndMalformedIdentifiersAreRefused()
        {
            var secrets = new Dictionary<string, string> { ["whk_sdktest01"] = "secret" };
            foreach (var name in new[]
            {
                "invalid/webhook-update-names-source.json",
                "invalid/webhook-unknown-event-type.json",
                "invalid/webhook-update-pointer-only.json",
                "invalid/webhook-update-with-expired-status.json",
                "invalid/webhook-verification-without-challenge.json",
                "invalid/webhook-account-deleted-none.json",
            })
            {
                var body = Fixture(name);
                var (payload, headers) = Signed(body, "secret", (string)body["id"]!);
                Refused(WebhookErrorCode.InvalidPayload, payload, headers, secrets);
            }

            var endpoint = Fixture("valid/webhook-endpoint-verification.json");
            endpoint["id"] = "evt_bad";
            var signed = Signed(endpoint, "secret", "evt_bad");
            Refused(WebhookErrorCode.InvalidPayload, signed.Payload, signed.Headers, secrets);

            endpoint = Fixture("valid/webhook-endpoint-verification.json");
            endpoint["data"]!["endpoint_id"] = "whe_bad";
            signed = Signed(endpoint, "secret", (string)endpoint["id"]!);
            Refused(WebhookErrorCode.InvalidPayload, signed.Payload, signed.Headers, secrets);

            endpoint = Fixture("valid/webhook-endpoint-verification.json");
            endpoint["created_at"] = "tomorrow";
            signed = Signed(endpoint, "secret", (string)endpoint["id"]!);
            Refused(WebhookErrorCode.InvalidPayload, signed.Payload, signed.Headers, secrets);

            endpoint = Fixture("valid/webhook-endpoint-verification.json");
            endpoint["created_at"] = "2026-10-14";
            signed = Signed(endpoint, "secret", (string)endpoint["id"]!);
            Refused(WebhookErrorCode.InvalidPayload, signed.Payload, signed.Headers, secrets);
        }
    }
}
