# AccountDecision

block when the score reached your block threshold (risk.block of the assessment), otherwise the assessment's recommendation. Your application applies it.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = AccountDecision.Allow;

// Open enum: use .Of() to create instances from custom string values
var custom = AccountDecision.Of("custom_value");
```


## Values

| Name     | Value    |
| -------- | -------- |
| `Allow`  | allow    |
| `StepUp` | step_up  |
| `Review` | review   |
| `Block`  | block    |