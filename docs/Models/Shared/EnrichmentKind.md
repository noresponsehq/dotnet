# EnrichmentKind

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = EnrichmentKind.Username;

// Open enum: use .Of() to create instances from custom string values
var custom = EnrichmentKind.Of("custom_value");
```


## Values

| Name       | Value      |
| ---------- | ---------- |
| `Username` | username   |
| `Email`    | email      |
| `Phone`    | phone      |