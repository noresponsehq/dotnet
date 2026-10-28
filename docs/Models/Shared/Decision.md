# Decision

An assessment's own decision: block when its stored risk.block is true, otherwise its recommendation. It is not the account's decision or held-admission state.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Decision.Allow;

// Open enum: use .Of() to create instances from custom string values
var custom = Decision.Of("custom_value");
```


## Values

| Name     | Value    |
| -------- | -------- |
| `Allow`  | allow    |
| `StepUp` | step_up  |
| `Review` | review   |
| `Block`  | block    |