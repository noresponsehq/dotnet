# LineType

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = LineType.Mobile;

// Open enum: use .Of() to create instances from custom string values
var custom = LineType.Of("custom_value");
```


## Values

| Name       | Value      |
| ---------- | ---------- |
| `Mobile`   | mobile     |
| `Voip`     | voip       |
| `Landline` | landline   |
| `TollFree` | toll_free  |
| `Other`    | other      |