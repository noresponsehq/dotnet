# AttendeeBatchItemOutcome

unchanged: the row matched an attendee with the same details.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = AttendeeBatchItemOutcome.Created;

// Open enum: use .Of() to create instances from custom string values
var custom = AttendeeBatchItemOutcome.Of("custom_value");
```


## Values

| Name        | Value       |
| ----------- | ----------- |
| `Created`   | created     |
| `Updated`   | updated     |
| `Unchanged` | unchanged   |