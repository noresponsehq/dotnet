# Recommendation

The recommended action: allow, step_up or review. Your application applies it.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Recommendation.Allow;

// Open enum: use .Of() to create instances from custom string values
var custom = Recommendation.Of("custom_value");
```


## Values

| Name     | Value    |
| -------- | -------- |
| `Allow`  | allow    |
| `StepUp` | step_up  |
| `Review` | review   |