# ErrorCode

Why a failed search found nothing: its sources did not answer, none could be asked, or it could not run before its attempts were spent.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = ErrorCode.NotAnswered;

// Open enum: use .Of() to create instances from custom string values
var custom = ErrorCode.Of("custom_value");
```


## Values

| Name                      | Value                     |
| ------------------------- | ------------------------- |
| `NotAnswered`             | not_answered              |
| `NoSource`                | no_source                 |
| `WorkerAttemptsExhausted` | worker_attempts_exhausted |