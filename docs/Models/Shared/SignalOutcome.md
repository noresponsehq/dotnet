# SignalOutcome

Per-signal outcome. A source that timed out, was blocked or errored reports blocked, unavailable or inconclusive with a reason and is listed in incomplete_sources; not_found is a definite absence.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = SignalOutcome.Found;

// Open enum: use .Of() to create instances from custom string values
var custom = SignalOutcome.Of("custom_value");
```


## Values

| Name           | Value          |
| -------------- | -------------- |
| `Found`        | found          |
| `NotFound`     | not_found      |
| `Inconclusive` | inconclusive   |
| `Blocked`      | blocked        |
| `Unavailable`  | unavailable    |