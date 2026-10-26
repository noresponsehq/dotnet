# Agreement

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Agreement.Match;

// Open enum: use .Of() to create instances from custom string values
var custom = Agreement.Of("custom_value");
```


## Values

| Name       | Value      |
| ---------- | ---------- |
| `Match`    | match      |
| `Partial`  | partial    |
| `Mismatch` | mismatch   |