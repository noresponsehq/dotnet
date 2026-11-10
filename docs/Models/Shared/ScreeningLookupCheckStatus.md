# ScreeningLookupCheckStatus

found: a source found the identifier. not_found: every source answered and none found it. partial: the sources that answered found nothing and the others did not answer. not_covered: no source answered. A lookup that failed reads partial or not_covered, not not_found.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = ScreeningLookupCheckStatus.Found;

// Open enum: use .Of() to create instances from custom string values
var custom = ScreeningLookupCheckStatus.Of("custom_value");
```


## Values

| Name         | Value        |
| ------------ | ------------ |
| `Found`      | found        |
| `NotFound`   | not_found    |
| `Partial`    | partial      |
| `NotCovered` | not_covered  |