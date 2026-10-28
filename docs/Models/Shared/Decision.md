# Decision

The decision of the assessment's latest stored revision: block when its stored risk.block is true, otherwise its recommendation. This is not the account's current decision or held-admission state. Combined with recommendation using AND.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Decision.Allow;
```


## Values

| Name     | Value    |
| -------- | -------- |
| `Allow`  | allow    |
| `StepUp` | step_up  |
| `Review` | review   |
| `Block`  | block    |