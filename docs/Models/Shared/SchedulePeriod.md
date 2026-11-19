# SchedulePeriod

How often the person is investigated again: daily, weekly or monthly, counted from when the schedule was set; off runs no scheduled investigation.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = SchedulePeriod.Daily;

// Open enum: use .Of() to create instances from custom string values
var custom = SchedulePeriod.Of("custom_value");
```


## Values

| Name      | Value     |
| --------- | --------- |
| `Daily`   | daily     |
| `Weekly`  | weekly    |
| `Monthly` | monthly   |
| `Off`     | off       |