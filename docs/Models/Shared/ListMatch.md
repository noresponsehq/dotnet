# ListMatch

The action of the list entry that decided the account.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = ListMatch.Block;

// Open enum: use .Of() to create instances from custom string values
var custom = ListMatch.Of("custom_value");
```


## Values

| Name    | Value   |
| ------- | ------- |
| `Block` | block   |
| `Allow` | allow   |