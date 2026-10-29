# Band

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Band.Low;

// Open enum: use .Of() to create instances from custom string values
var custom = Band.Of("custom_value");
```


## Values

| Name       | Value      |
| ---------- | ---------- |
| `Low`      | low        |
| `Medium`   | medium     |
| `High`     | high       |
| `Critical` | critical   |