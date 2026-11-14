# PersonBatchItemOutcome

unchanged: the row matched a person with the same details.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = PersonBatchItemOutcome.Created;

// Open enum: use .Of() to create instances from custom string values
var custom = PersonBatchItemOutcome.Of("custom_value");
```


## Values

| Name        | Value       |
| ----------- | ----------- |
| `Created`   | created     |
| `Updated`   | updated     |
| `Unchanged` | unchanged   |