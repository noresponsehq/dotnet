# From

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = From.Allow;

// Open enum: use .Of() to create instances from custom string values
var custom = From.Of("custom_value");
```


## Values

| Name     | Value    |
| -------- | -------- |
| `Allow`  | allow    |
| `StepUp` | step_up  |
| `Review` | review   |
| `Block`  | block    |
| `Held`   | held     |