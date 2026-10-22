# BadRequestErrorCode

Machine codes an HTTP 400 answer can carry.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = BadRequestErrorCode.InvalidRequest;

// Open enum: use .Of() to create instances from custom string values
var custom = BadRequestErrorCode.Of("custom_value");
```


## Values

| Name                    | Value                   |
| ----------------------- | ----------------------- |
| `InvalidRequest`        | invalid_request         |
| `InvalidIdempotencyKey` | invalid_idempotency_key |
| `InvalidCursor`         | invalid_cursor          |
| `WebhookUrlRefused`     | webhook_url_refused     |