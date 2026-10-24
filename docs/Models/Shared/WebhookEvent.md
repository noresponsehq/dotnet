# WebhookEvent

The body of every webhook delivery: {id, type, created_at, data}, where type chooses the shape of data. Deliveries are at least once and unordered: deduplicate on id, and keep an assessment only when its detail.assessment_version is newer than the one you hold. GET /v1/assessments/{assessment_id} stays authoritative.


## Supported Types

### AssessmentUpdated

```csharp
WebhookEvent.CreateAssessmentUpdated(/* values here */);
```

### EndpointVerification

```csharp
WebhookEvent.CreateEndpointVerification(/* values here */);
```

### AccountDeleted

```csharp
WebhookEvent.CreateAccountDeleted(/* values here */);
```

### WorkspaceDeleted

```csharp
WebhookEvent.CreateWorkspaceDeleted(/* values here */);
```

### WebhookEndpointDisabled

```csharp
WebhookEvent.CreateWebhookEndpointDisabled(/* values here */);
```

### AccountRiskChanged

```csharp
WebhookEvent.CreateAccountRiskChanged(/* values here */);
```
