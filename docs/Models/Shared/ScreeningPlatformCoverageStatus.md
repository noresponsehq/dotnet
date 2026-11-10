# ScreeningPlatformCoverageStatus

covered read at least one profile; none_found looked and found no account; inaccessible found accounts that are private or locked; failed could not finish looking; unavailable has no resolver or collector enabled.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = ScreeningPlatformCoverageStatus.Covered;

// Open enum: use .Of() to create instances from custom string values
var custom = ScreeningPlatformCoverageStatus.Of("custom_value");
```


## Values

| Name           | Value          |
| -------------- | -------------- |
| `Covered`      | covered        |
| `NoneFound`    | none_found     |
| `Inaccessible` | inaccessible   |
| `Failed`       | failed         |
| `Unavailable`  | unavailable    |