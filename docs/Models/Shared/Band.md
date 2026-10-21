# Band

low 0-29, medium 30-59, high 60-84, critical 85-100.

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