# AuditEventTargetType

The kind of resource the change acted on.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = AuditEventTargetType.Workspace;

// Open enum: use .Of() to create instances from custom string values
var custom = AuditEventTargetType.Of("custom_value");
```


## Values

| Name              | Value             |
| ----------------- | ----------------- |
| `Workspace`       | workspace         |
| `Member`          | member            |
| `Invite`          | invite            |
| `Key`             | key               |
| `Setting`         | setting           |
| `WebhookEndpoint` | webhook_endpoint  |
| `Integration`     | integration       |
| `Billing`         | billing           |
| `Review`          | review            |