#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using NoResponse.Models.Shared;
    using NoResponse.Utils;
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using System.Reflection;

    public class WebhookEventType
    {
        private WebhookEventType(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public static WebhookEventType AssessmentUpdated
        {
            get {
                return new WebhookEventType("assessment.updated");
            }
        }

        public static WebhookEventType EndpointVerification
        {
            get {
                return new WebhookEventType("endpoint.verification");
            }
        }

        public static WebhookEventType AccountDeleted
        {
            get {
                return new WebhookEventType("account.deleted");
            }
        }

        public static WebhookEventType WorkspaceDeleted
        {
            get {
                return new WebhookEventType("workspace.deleted");
            }
        }

        public static WebhookEventType WebhookEndpointDisabled
        {
            get {
                return new WebhookEventType("webhook_endpoint.disabled");
            }
        }

        public static WebhookEventType AccountRiskChanged
        {
            get {
                return new WebhookEventType("account.risk_changed");
            }
        }

        public override string ToString()
        {
            return Value;
        }
        public static implicit operator String(WebhookEventType v)
        {
            return v.Value;
        }
        public static WebhookEventType FromString(string v)
        {
            switch (v)
            {
                case "assessment.updated":
                    return AssessmentUpdated;
                case "endpoint.verification":
                    return EndpointVerification;
                case "account.deleted":
                    return AccountDeleted;
                case "workspace.deleted":
                    return WorkspaceDeleted;
                case "webhook_endpoint.disabled":
                    return WebhookEndpointDisabled;
                case "account.risk_changed":
                    return AccountRiskChanged;
                default:
                    throw new ArgumentException("Invalid value for WebhookEventType");
            }
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Value.Equals(((WebhookEventType)obj).Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }

    /// <summary>
    /// The body of every webhook delivery: {id, type, created_at, data}, where type chooses the shape of data. Deliveries are at least once and unordered: deduplicate on id, and keep an assessment only when its detail.assessment_version is newer than the one you hold. GET /v1/assessments/{assessment_id} stays authoritative.
    /// </summary>
    [JsonConverter(typeof(WebhookEvent.WebhookEventConverter))]
    public class WebhookEvent
    {
        public WebhookEvent(WebhookEventType type)
        {
            Type = type;
        }

        [UnionVariant]
        [NoResponseMetadata("form:explode=true")]
        public AssessmentUpdatedEvent? AssessmentUpdatedEvent { get; set; }

        [UnionVariant]
        [NoResponseMetadata("form:explode=true")]
        public EndpointVerificationEvent? EndpointVerificationEvent { get; set; }

        [UnionVariant]
        [NoResponseMetadata("form:explode=true")]
        public AccountDeletedEvent? AccountDeletedEvent { get; set; }

        [UnionVariant]
        [NoResponseMetadata("form:explode=true")]
        public WorkspaceDeletedEvent? WorkspaceDeletedEvent { get; set; }

        [UnionVariant]
        [NoResponseMetadata("form:explode=true")]
        public WebhookEndpointDisabledEvent? WebhookEndpointDisabledEvent { get; set; }

        [UnionVariant]
        [NoResponseMetadata("form:explode=true")]
        public AccountRiskChangedEvent? AccountRiskChangedEvent { get; set; }

        public WebhookEventType Type { get; set; }

        public static WebhookEvent CreateAssessmentUpdated(AssessmentUpdatedEvent assessmentUpdated)
        {
            WebhookEventType typ = WebhookEventType.AssessmentUpdated;
            WebhookEvent res = new WebhookEvent(typ);
            res.AssessmentUpdatedEvent = assessmentUpdated;
            return res;
        }

        public static WebhookEvent CreateEndpointVerification(EndpointVerificationEvent endpointVerification)
        {
            WebhookEventType typ = WebhookEventType.EndpointVerification;
            WebhookEvent res = new WebhookEvent(typ);
            res.EndpointVerificationEvent = endpointVerification;
            return res;
        }

        public static WebhookEvent CreateAccountDeleted(AccountDeletedEvent accountDeleted)
        {
            WebhookEventType typ = WebhookEventType.AccountDeleted;
            WebhookEvent res = new WebhookEvent(typ);
            res.AccountDeletedEvent = accountDeleted;
            return res;
        }

        public static WebhookEvent CreateWorkspaceDeleted(WorkspaceDeletedEvent workspaceDeleted)
        {
            WebhookEventType typ = WebhookEventType.WorkspaceDeleted;
            WebhookEvent res = new WebhookEvent(typ);
            res.WorkspaceDeletedEvent = workspaceDeleted;
            return res;
        }

        public static WebhookEvent CreateWebhookEndpointDisabled(WebhookEndpointDisabledEvent webhookEndpointDisabled)
        {
            WebhookEventType typ = WebhookEventType.WebhookEndpointDisabled;
            WebhookEvent res = new WebhookEvent(typ);
            res.WebhookEndpointDisabledEvent = webhookEndpointDisabled;
            return res;
        }

        public static WebhookEvent CreateAccountRiskChanged(AccountRiskChangedEvent accountRiskChanged)
        {
            WebhookEventType typ = WebhookEventType.AccountRiskChanged;
            WebhookEvent res = new WebhookEvent(typ);
            res.AccountRiskChangedEvent = accountRiskChanged;
            return res;
        }

        public class WebhookEventConverter : JsonConverter
        {
            public override bool CanConvert(System.Type objectType) => objectType == typeof(WebhookEvent);

            public override bool CanRead => true;

            public override object? ReadJson(JsonReader reader, System.Type objectType, object? existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null)
                {

                    throw new JsonSerializationException("Received unexpected null JSON value");
                }

                JObject jo = JObject.Load(reader);
                string discriminator = jo.GetValue("type")?.ToString() ?? throw new ArgumentNullException("Could not find discriminator field.");
                if (discriminator == WebhookEventType.AssessmentUpdated.ToString())
                {
                    AssessmentUpdatedEvent assessmentUpdatedEvent = ResponseBodyDeserializer.DeserializeNotNull<AssessmentUpdatedEvent>(jo.ToString());
                    return CreateAssessmentUpdated(assessmentUpdatedEvent);
                }
                if (discriminator == WebhookEventType.EndpointVerification.ToString())
                {
                    EndpointVerificationEvent endpointVerificationEvent = ResponseBodyDeserializer.DeserializeNotNull<EndpointVerificationEvent>(jo.ToString());
                    return CreateEndpointVerification(endpointVerificationEvent);
                }
                if (discriminator == WebhookEventType.AccountDeleted.ToString())
                {
                    AccountDeletedEvent accountDeletedEvent = ResponseBodyDeserializer.DeserializeNotNull<AccountDeletedEvent>(jo.ToString());
                    return CreateAccountDeleted(accountDeletedEvent);
                }
                if (discriminator == WebhookEventType.WorkspaceDeleted.ToString())
                {
                    WorkspaceDeletedEvent workspaceDeletedEvent = ResponseBodyDeserializer.DeserializeNotNull<WorkspaceDeletedEvent>(jo.ToString());
                    return CreateWorkspaceDeleted(workspaceDeletedEvent);
                }
                if (discriminator == WebhookEventType.WebhookEndpointDisabled.ToString())
                {
                    WebhookEndpointDisabledEvent webhookEndpointDisabledEvent = ResponseBodyDeserializer.DeserializeNotNull<WebhookEndpointDisabledEvent>(jo.ToString());
                    return CreateWebhookEndpointDisabled(webhookEndpointDisabledEvent);
                }
                if (discriminator == WebhookEventType.AccountRiskChanged.ToString())
                {
                    AccountRiskChangedEvent accountRiskChangedEvent = ResponseBodyDeserializer.DeserializeNotNull<AccountRiskChangedEvent>(jo.ToString());
                    return CreateAccountRiskChanged(accountRiskChangedEvent);
                }

                throw new InvalidOperationException("Could not deserialize into any supported types.");
            }

            public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
            {
                if (value == null)
                {
                    throw new InvalidOperationException("Unexpected null JSON value.");
                }

                WebhookEvent res = (WebhookEvent)value;

                if (res.AssessmentUpdatedEvent != null)
                {
                    writer.WriteRawValue(Utilities.SerializeJSON(res.AssessmentUpdatedEvent));
                    return;
                }

                if (res.EndpointVerificationEvent != null)
                {
                    writer.WriteRawValue(Utilities.SerializeJSON(res.EndpointVerificationEvent));
                    return;
                }

                if (res.AccountDeletedEvent != null)
                {
                    writer.WriteRawValue(Utilities.SerializeJSON(res.AccountDeletedEvent));
                    return;
                }

                if (res.WorkspaceDeletedEvent != null)
                {
                    writer.WriteRawValue(Utilities.SerializeJSON(res.WorkspaceDeletedEvent));
                    return;
                }

                if (res.WebhookEndpointDisabledEvent != null)
                {
                    writer.WriteRawValue(Utilities.SerializeJSON(res.WebhookEndpointDisabledEvent));
                    return;
                }

                if (res.AccountRiskChangedEvent != null)
                {
                    writer.WriteRawValue(Utilities.SerializeJSON(res.AccountRiskChangedEvent));
                    return;
                }

                throw new InvalidOperationException(
                    "Could not serialize union to JSON: no variant value was set. " +
                    "Construct this union using one of the Create* factory methods."
                );
            }
        }
    }
}