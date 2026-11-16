# WrittenProfileErrorCode

Present only when status is failed. no_writer: no profile writer serves this environment. refused: the model refused. not_written: the answer was not a profile, or no statement survived grounding. uncited_statement: a statement or lead cited a record the person does not hold. not_answered: the writer did not answer. worker_attempts_exhausted: the worker stopped retrying.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = WrittenProfileErrorCode.NoWriter;

// Open enum: use .Of() to create instances from custom string values
var custom = WrittenProfileErrorCode.Of("custom_value");
```


## Values

| Name                      | Value                     |
| ------------------------- | ------------------------- |
| `NoWriter`                | no_writer                 |
| `Refused`                 | refused                   |
| `NotWritten`              | not_written               |
| `UncitedStatement`        | uncited_statement         |
| `NotAnswered`             | not_answered              |
| `WorkerAttemptsExhausted` | worker_attempts_exhausted |