# ConsistencyValue

How a submitted identifier agrees with what the evidence holds for the other identifiers. unknown is a definite 'cannot tell' (for example a truncated or too-common breach result); absent means the comparison was not made.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = ConsistencyValue.Match;

// Open enum: use .Of() to create instances from custom string values
var custom = ConsistencyValue.Of("custom_value");
```


## Values

| Name       | Value      |
| ---------- | ---------- |
| `Match`    | match      |
| `Partial`  | partial    |
| `Mismatch` | mismatch   |
| `Unknown`  | unknown    |