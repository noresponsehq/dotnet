# ListEntryAction

block decides block for the accounts the entry matches; allow decides allow. Block wins when both match.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = ListEntryAction.Block;

// Open enum: use .Of() to create instances from custom string values
var custom = ListEntryAction.Of("custom_value");
```


## Values

| Name    | Value   |
| ------- | ------- |
| `Block` | block   |
| `Allow` | allow   |