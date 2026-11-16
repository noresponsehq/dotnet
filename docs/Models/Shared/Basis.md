# Basis

stated: read from a record. inferred: reasoned from several facts. absence: every source answered and none found it.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Basis.Stated;

// Open enum: use .Of() to create instances from custom string values
var custom = Basis.Of("custom_value");
```


## Values

| Name       | Value      |
| ---------- | ---------- |
| `Stated`   | stated     |
| `Inferred` | inferred   |
| `Absence`  | absence    |