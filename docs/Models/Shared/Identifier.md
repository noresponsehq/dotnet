# Identifier

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Identifier.Email;

// Open enum: use .Of() to create instances from custom string values
var custom = Identifier.Of("custom_value");
```


## Values

| Name       | Value      |
| ---------- | ---------- |
| `Email`    | email      |
| `Username` | username   |