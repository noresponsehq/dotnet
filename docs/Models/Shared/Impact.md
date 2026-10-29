# Impact

The selected reason's served impact band. Requires reason.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Impact.None;

// Open enum: use .Of() to create instances from custom string values
var custom = Impact.Of("custom_value");
```


## Values

| Name     | Value    |
| -------- | -------- |
| `None`   | none     |
| `Low`    | low      |
| `Medium` | medium   |
| `High`   | high     |