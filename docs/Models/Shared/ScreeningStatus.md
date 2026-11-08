# ScreeningStatus

partial finished with some attendees not covered; attendees_failed counts them.

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