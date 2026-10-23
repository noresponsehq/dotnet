# WebhookDeliveryAttemptStatus

delivered when the endpoint answered 2xx; failed otherwise, with failure naming why.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = WebhookDeliveryAttemptStatus.Delivered;

// Open enum: use .Of() to create instances from custom string values
var custom = WebhookDeliveryAttemptStatus.Of("custom_value");
```


## Values

| Name        | Value       |
| ----------- | ----------- |
| `Delivered` | delivered   |
| `Failed`    | failed      |