# ConflictErrorCode

Machine codes an HTTP 409 answer can carry.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = ConflictErrorCode.IdempotencyKeyReused;

// Open enum: use .Of() to create instances from custom string values
var custom = ConflictErrorCode.Of("custom_value");
```


## Values

| Name                           | Value                          |
| ------------------------------ | ------------------------------ |
| `IdempotencyKeyReused`         | idempotency_key_reused         |
| `KeyRotationInProgress`        | key_rotation_in_progress       |
| `WebhookEndpointLimitReached`  | webhook_endpoint_limit_reached |
| `WebhookEndpointUnverified`    | webhook_endpoint_unverified    |
| `TooManyIntegrations`          | too_many_integrations          |