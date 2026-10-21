# Status

complete: final; nothing is still running. A lane that failed or missed its 10 s deadline stays listed in incomplete_sources, and confidence is lower. partial: a recommendation exists and at least one lane is still running or did not settle (see incomplete_sources). pending: nothing usable before the deadline; no recommendation yet, poll or wait for the webhook.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Status.Complete;

// Open enum: use .Of() to create instances from custom string values
var custom = Status.Of("custom_value");
```


## Values

| Name       | Value      |
| ---------- | ---------- |
| `Complete` | complete   |
| `Partial`  | partial    |
| `Pending`  | pending    |