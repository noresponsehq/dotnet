# FindingBand

How severe a finding is, most severe first.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = FindingBand.Critical;

// Open enum: use .Of() to create instances from custom string values
var custom = FindingBand.Of("custom_value");
```


## Values

| Name       | Value      |
| ---------- | ---------- |
| `Critical` | critical   |
| `High`     | high       |
| `Moderate` | moderate   |
| `Low`      | low        |
| `Info`     | info       |