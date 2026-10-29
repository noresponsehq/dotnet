# Status

Processing state, separate from evidence coverage. complete: final; nothing is still running, but unsupported, failed or unanswered lanes remain in incomplete_sources. partial: a recommendation exists and evidence is incomplete; assessment_url identifies continuing work when present. pending: no recommendation yet; poll or wait for the webhook. Finished processing does not mean all sources answered.

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