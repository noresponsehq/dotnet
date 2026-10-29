# Source

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Source.Requests;

// Open enum: use .Of() to create instances from custom string values
var custom = Source.Of("custom_value");
```


## Values

| Name              | Value             |
| ----------------- | ----------------- |
| `Requests`        | requests          |
| `WebhookAttempts` | webhook_attempts  |