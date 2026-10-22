# WebhookTestRequestEvent

endpoint.verification sends a fresh challenge now, in this call, and the endpoint is verified if it echoes it. assessment.updated queues an event whose data is a fixed example assessment, with test true, delivered and retried like any other; the endpoint must be verified.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = WebhookTestRequestEvent.EndpointVerification;

// Open enum: use .Of() to create instances from custom string values
var custom = WebhookTestRequestEvent.Of("custom_value");
```


## Values

| Name                   | Value                  |
| ---------------------- | ---------------------- |
| `EndpointVerification` | endpoint.verification  |
| `AssessmentUpdated`    | assessment.updated     |