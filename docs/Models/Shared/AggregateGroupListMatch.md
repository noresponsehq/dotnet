# AggregateGroupListMatch

The list action captured with the first answer. none means no matching entry was found. For retained_first_answers an absent field means the older assessment did not capture list state; this remains a separate group.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = AggregateGroupListMatch.Block;

// Open enum: use .Of() to create instances from custom string values
var custom = AggregateGroupListMatch.Of("custom_value");
```


## Values

| Name    | Value   |
| ------- | ------- |
| `Block` | block   |
| `Allow` | allow   |
| `None`  | none    |