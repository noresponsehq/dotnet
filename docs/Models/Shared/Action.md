# Action

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Action.Allow;

// Open enum: use .Of() to create instances from custom string values
var custom = Action.Of("custom_value");
```


## Values

| Name    | Value   |
| ------- | ------- |
| `Allow` | allow   |
| `Block` | block   |