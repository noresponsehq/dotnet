# AttendeeDecision

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = AttendeeDecision.Cleared;

// Open enum: use .Of() to create instances from custom string values
var custom = AttendeeDecision.Of("custom_value");
```


## Values

| Name        | Value       |
| ----------- | ----------- |
| `Cleared`   | cleared     |
| `Watch`     | watch       |
| `Escalated` | escalated   |