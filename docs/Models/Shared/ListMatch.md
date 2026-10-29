# ListMatch

The list action captured at the first answer. Requires revision first. Older answers without captured list state do not match, including none.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = ListMatch.Allow;
```


## Values

| Name    | Value   |
| ------- | ------- |
| `Allow` | allow   |
| `Block` | block   |
| `None`  | none    |