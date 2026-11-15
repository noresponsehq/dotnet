# InvestigationStatus

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = InvestigationStatus.Queued;

// Open enum: use .Of() to create instances from custom string values
var custom = InvestigationStatus.Of("custom_value");
```


## Values

| Name        | Value       |
| ----------- | ----------- |
| `Queued`    | queued      |
| `Running`   | running     |
| `Completed` | completed   |
| `Partial`   | partial     |
| `Failed`    | failed      |
| `Cancelled` | cancelled   |
| `Unknown`   | unknown     |