# IdentifierKind

Which submitted identifier a signal concerns. ip is a selector of breach_exposure only.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = IdentifierKind.Email;

// Open enum: use .Of() to create instances from custom string values
var custom = IdentifierKind.Of("custom_value");
```


## Values

| Name       | Value      |
| ---------- | ---------- |
| `Email`    | email      |
| `Username` | username   |
| `Phone`    | phone      |
| `Ip`       | ip         |