# PolicyEvaluationStatus

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = PolicyEvaluationStatus.Queued;

// Open enum: use .Of() to create instances from custom string values
var custom = PolicyEvaluationStatus.Of("custom_value");
```


## Values

| Name        | Value       |
| ----------- | ----------- |
| `Queued`    | queued      |
| `Running`   | running     |
| `Completed` | completed   |
| `Failed`    | failed      |
| `Cancelled` | cancelled   |