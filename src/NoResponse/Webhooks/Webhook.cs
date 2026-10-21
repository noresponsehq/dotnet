#nullable enable
namespace NoResponse.Webhooks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;

    /// <summary>Why a delivery was refused.</summary>
    public enum WebhookErrorCode
    {
        MissingHeader,
        DuplicateHeader,
        InvalidTimestamp,
        UnknownKey,
        InvalidSignature,
        InvalidPayload,
    }

    /// <summary>Thrown by <see cref="Webhook.ConstructEvent"/>. Answer 400 and do not process the delivery.</summary>
    public sealed class WebhookVerificationException : Exception
    {
        public WebhookVerificationException(WebhookErrorCode code, string message) : base(message)
        {
            Code = code;
        }

        public WebhookErrorCode Code { get; }
    }

    /// <summary>
    /// The final event for an assessment: it has expired and Get now answers 404.
    /// AssessmentVersion is the last served version; expiry is not a revision.
    /// </summary>
    public sealed record AssessmentExpired(string AssessmentId, string AccountId, long AssessmentVersion)
    {
        public string Status => "expired";

        public string ChangeReason => "expired";
    }

    /// <summary>The challenge NoResponse sends when an endpoint is registered.</summary>
    public sealed record EndpointVerification(string EndpointId, string Challenge);

    /// <summary>
    /// A verified delivery. Data is an <see cref="Assessment"/> (exactly as Assessments.GetAsync
    /// returns it) or an <see cref="AssessmentExpired"/> for type "assessment.updated", and an
    /// <see cref="EndpointVerification"/> for "endpoint.verification". Deliveries are at least
    /// once and unordered: deduplicate on Id and keep the highest AssessmentVersion.
    /// </summary>
    public sealed record WebhookEvent(string Id, string Type, string CreatedAt, object Data)
    {
        /// <summary>True for the terminal expiry event: delete what you hold and fetch nothing.</summary>
        public bool IsExpired => Data is AssessmentExpired;
    }

    /// <summary>
    /// Webhook verification. Hand-written beside the generated client. Verify the exact bytes
    /// received: the signature is HMAC-SHA256 over "&lt;timestamp&gt;.&lt;raw body&gt;", so
    /// re-serialising parsed JSON breaks it.
    /// </summary>
    public static class Webhook
    {
        public static readonly TimeSpan DefaultTolerance = TimeSpan.FromSeconds(300);

        private static readonly string[] Required = { "nr-webhook-id", "nr-webhook-timestamp", "nr-webhook-key-id", "nr-webhook-signature" };
        private static readonly Regex KeyId = new Regex("^whk_[A-Za-z0-9]{8,64}$", RegexOptions.CultureInvariant);
        private static readonly Regex Signature = new Regex("^v1=[0-9a-f]{64}$", RegexOptions.CultureInvariant);
        private static readonly Regex Timestamp = new Regex("^[0-9]{1,12}$", RegexOptions.CultureInvariant);
        private static readonly Regex Challenge = new Regex("^[A-Za-z0-9_-]{43}$", RegexOptions.CultureInvariant);
        private static readonly Regex EventId = new Regex("^evt_[0-9A-HJKMNP-TV-Z]{26}$", RegexOptions.CultureInvariant);
        private static readonly Regex AssessmentId = new Regex("^asm_[0-9A-HJKMNP-TV-Z]{26}$", RegexOptions.CultureInvariant);
        private static readonly Regex AccountId = new Regex("^[A-Za-z0-9][A-Za-z0-9._:|-]{0,127}$", RegexOptions.CultureInvariant);
        private static readonly Regex EndpointId = new Regex("^whe_[0-9A-HJKMNP-TV-Z]{26}$", RegexOptions.CultureInvariant);
        private static readonly Regex DateTime = new Regex("^\\d{4}-\\d{2}-\\d{2}T\\d{2}:\\d{2}:\\d{2}(?:\\.\\d+)?(?:Z|[+-]\\d{2}:\\d{2})$", RegexOptions.CultureInvariant);

        /// <summary>
        /// Verify one delivery and return the typed event. <paramref name="headers"/> maps header
        /// names to their values in any letter case (ASP.NET Core's Request.Headers works as is);
        /// each NR-Webhook-* header must appear exactly once. <paramref name="secrets"/> maps each
        /// whk_ key id NoResponse gave you to its secret; hold both during a rotation.
        /// </summary>
        /// <remarks>
        /// Generic over the value type because KeyValuePair is not covariant: ASP.NET Core's
        /// headers are KeyValuePair&lt;string, StringValues&gt;, HttpHeaders are
        /// KeyValuePair&lt;string, IEnumerable&lt;string&gt;&gt;, and both bind here.
        /// </remarks>
        /// <exception cref="WebhookVerificationException">The delivery must be refused.</exception>
        public static WebhookEvent ConstructEvent<TValues>(
            byte[] payload,
            IEnumerable<KeyValuePair<string, TValues>> headers,
            IReadOnlyDictionary<string, string> secrets,
            TimeSpan? tolerance = null,
            DateTimeOffset? now = null)
            where TValues : IEnumerable<string>
        {
            var found = Collect(headers);
            var timestamp = found["nr-webhook-timestamp"];
            var current = (now ?? DateTimeOffset.UtcNow).ToUnixTimeSeconds();
            var window = (long)(tolerance ?? DefaultTolerance).TotalSeconds;
            if (!Timestamp.IsMatch(timestamp) || Math.Abs(current - long.Parse(timestamp, System.Globalization.CultureInfo.InvariantCulture)) > window)
            {
                throw new WebhookVerificationException(WebhookErrorCode.InvalidTimestamp, "Webhook timestamp is outside the replay window");
            }
            var keyId = found["nr-webhook-key-id"];
            if (!KeyId.IsMatch(keyId) || !secrets.TryGetValue(keyId, out var secret))
            {
                throw new WebhookVerificationException(WebhookErrorCode.UnknownKey, "Webhook key id is not one this receiver holds");
            }
            var signature = found["nr-webhook-signature"];
            if (!Signature.IsMatch(signature))
            {
                throw new WebhookVerificationException(WebhookErrorCode.InvalidSignature, "Webhook signature is malformed");
            }
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
            {
                var signed = Encoding.ASCII.GetBytes(timestamp + ".").Concat(payload).ToArray();
                var expected = Encoding.ASCII.GetBytes(Convert.ToHexString(hmac.ComputeHash(signed)).ToLowerInvariant());
                if (!CryptographicOperations.FixedTimeEquals(expected, Encoding.ASCII.GetBytes(signature.Substring(3))))
                {
                    throw new WebhookVerificationException(WebhookErrorCode.InvalidSignature, "Webhook signature does not match");
                }
            }
            return Event(payload, found["nr-webhook-id"]);
        }

        /// <summary>The body to answer an endpoint.verification event with, as JSON, status 200.</summary>
        /// <exception cref="WebhookVerificationException">The event is not endpoint.verification.</exception>
        public static IReadOnlyDictionary<string, string> VerificationResponse(WebhookEvent webhookEvent)
        {
            if (webhookEvent.Data is EndpointVerification verification)
            {
                return new Dictionary<string, string> { ["challenge"] = verification.Challenge };
            }
            throw new WebhookVerificationException(WebhookErrorCode.InvalidPayload, "Not an endpoint.verification event");
        }

        private static Dictionary<string, string> Collect<TValues>(IEnumerable<KeyValuePair<string, TValues>> headers)
            where TValues : IEnumerable<string>
        {
            var found = new Dictionary<string, string>();
            foreach (var header in headers)
            {
                var key = header.Key.ToLowerInvariant();
                if (!Required.Contains(key))
                {
                    continue;
                }
                var values = header.Value.ToList();
                if (values.Count != 1 || found.ContainsKey(key))
                {
                    throw new WebhookVerificationException(WebhookErrorCode.DuplicateHeader, $"Webhook header {key} is repeated");
                }
                found[key] = values[0];
            }
            foreach (var key in Required)
            {
                if (!found.TryGetValue(key, out var value) || string.IsNullOrEmpty(value))
                {
                    throw new WebhookVerificationException(WebhookErrorCode.MissingHeader, $"Webhook header {key} is missing");
                }
            }
            return found;
        }

        private static WebhookEvent Event(byte[] payload, string headerId)
        {
            JObject body;
            try
            {
                // Keep timestamps as the strings received, not DateTime tokens.
                using var reader = new JsonTextReader(new System.IO.StringReader(Encoding.UTF8.GetString(payload))) { DateParseHandling = DateParseHandling.None };
                body = JObject.Load(reader);
            }
            catch (JsonException)
            {
                throw new WebhookVerificationException(WebhookErrorCode.InvalidPayload, "Webhook body is not JSON");
            }
            if (body["id"] is not JValue { Type: JTokenType.String } id
                || !EventId.IsMatch((string)id!) || (string?)id != headerId)
            {
                throw new WebhookVerificationException(WebhookErrorCode.InvalidPayload, "Webhook event id does not match NR-Webhook-Id");
            }
            if (body["created_at"] is not JValue { Type: JTokenType.String } createdAt
                || !DateTime.IsMatch((string)createdAt!)
                || !DateTimeOffset.TryParse((string)createdAt!, System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.RoundtripKind, out _)
                || body["type"] is not JValue { Type: JTokenType.String } type
                || body["data"] is not JObject data)
            {
                throw new WebhookVerificationException(WebhookErrorCode.InvalidPayload, "Webhook event envelope is malformed");
            }
            var eventType = (string)type!;
            return new WebhookEvent(headerId, eventType, (string)createdAt!, Data(eventType, data));
        }

        private static object Data(string type, JObject data)
        {
            if (type == "endpoint.verification")
            {
                if (data["endpoint_id"] is not JValue { Type: JTokenType.String } endpointId
                    || !EndpointId.IsMatch((string)endpointId!)
                    || data["challenge"] is not JValue { Type: JTokenType.String } challenge
                    || !Challenge.IsMatch((string)challenge!))
                {
                    throw new WebhookVerificationException(WebhookErrorCode.InvalidPayload, "Endpoint verification event is malformed");
                }
                return new EndpointVerification((string)endpointId!, (string)challenge!);
            }
            if (type != "assessment.updated")
            {
                throw new WebhookVerificationException(WebhookErrorCode.InvalidPayload, "Unknown webhook event type");
            }
            if (data["status"] is JValue { Type: JTokenType.String } status && (string?)status == "expired")
            {
                if (data["assessment_id"] is not JValue { Type: JTokenType.String } assessmentId
                    || !AssessmentId.IsMatch((string)assessmentId!)
                    || data["account_id"] is not JValue { Type: JTokenType.String } accountId
                    || !AccountId.IsMatch((string)accountId!)
                    || data["assessment_version"] is not JValue { Type: JTokenType.Integer } version
                    || (long)version < 1
                    || (string?)data["change_reason"] != "expired")
                {
                    throw new WebhookVerificationException(WebhookErrorCode.InvalidPayload, "Expired assessment event is malformed");
                }
                return new AssessmentExpired((string)assessmentId!, (string)accountId!, (long)version);
            }
            try
            {
                return ResponseBodyDeserializer.DeserializeNotNull<Assessment>(
                    data.ToString(Formatting.None), missingMemberHandling: MissingMemberHandling.Ignore);
            }
            catch (Exception e) when (e is JsonException || e is ArgumentException || e is FormatException || e.Message.StartsWith("Deserialization error", StringComparison.Ordinal))
            {
                throw new WebhookVerificationException(WebhookErrorCode.InvalidPayload, "Webhook data is not an assessment");
            }
        }
    }
}
