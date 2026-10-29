# CustomerExportStatus

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = CustomerExportStatus.Queued;

// Open enum: use .Of() to create instances from custom string values
var custom = CustomerExportStatus.Of("custom_value");
```


## Values

| Name        | Value       |
| ----------- | ----------- |
| `Queued`    | queued      |
| `Running`   | running     |
| `Completed` | completed   |
| `Failed`    | failed      |
| `Cancelled` | cancelled   |
| `Expired`   | expired     |