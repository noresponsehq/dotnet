# ReviewStatus

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = ReviewStatus.Open;

// Open enum: use .Of() to create instances from custom string values
var custom = ReviewStatus.Of("custom_value");
```


## Values

| Name         | Value        |
| ------------ | ------------ |
| `Open`       | open         |
| `InProgress` | in_progress  |
| `Resolved`   | resolved     |