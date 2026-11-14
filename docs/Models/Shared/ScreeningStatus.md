# ScreeningStatus

partial finished with some people or platforms not covered; people_failed and people_partial count them.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = ScreeningStatus.Queued;

// Open enum: use .Of() to create instances from custom string values
var custom = ScreeningStatus.Of("custom_value");
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