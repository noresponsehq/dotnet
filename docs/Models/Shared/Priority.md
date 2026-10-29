# Priority

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Priority.Low;

// Open enum: use .Of() to create instances from custom string values
var custom = Priority.Of("custom_value");
```


## Values

| Name     | Value    |
| -------- | -------- |
| `Low`    | low      |
| `Normal` | normal   |
| `High`   | high     |
| `Urgent` | urgent   |