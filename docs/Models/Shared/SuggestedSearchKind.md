# SuggestedSearchKind

The kind of the value: one of the kinds the environment's enrichment setting names.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = SuggestedSearchKind.Username;

// Open enum: use .Of() to create instances from custom string values
var custom = SuggestedSearchKind.Of("custom_value");
```


## Values

| Name       | Value      |
| ---------- | ---------- |
| `Username` | username   |
| `Email`    | email      |
| `Phone`    | phone      |