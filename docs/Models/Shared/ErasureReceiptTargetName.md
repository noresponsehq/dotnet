# ErasureReceiptTargetName

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = ErasureReceiptTargetName.Serving;

// Open enum: use .Of() to create instances from custom string values
var custom = ErasureReceiptTargetName.Of("custom_value");
```


## Values

| Name                   | Value                  |
| ---------------------- | ---------------------- |
| `Serving`              | serving                |
| `Outbox`               | outbox                 |
| `Cache`                | cache                  |
| `DownstreamProjection` | downstream_projection  |