# WebhookEndpointState

unverified until the endpoint echoes an endpoint.verification challenge (send one with POST /v1/webhooks/{endpoint_id}/test); verified receives assessment.updated events; disabled after 72 hours of continuous delivery failure, and verified again by the same challenge.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = WebhookEndpointState.Unverified;

// Open enum: use .Of() to create instances from custom string values
var custom = WebhookEndpointState.Of("custom_value");
```


## Values

| Name         | Value        |
| ------------ | ------------ |
| `Unverified` | unverified   |
| `Verified`   | verified     |
| `Disabled`   | disabled     |