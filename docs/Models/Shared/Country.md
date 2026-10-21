# Country

IP country, phone country, breach countries and policy_context.region together.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Country.Consistent;

// Open enum: use .Of() to create instances from custom string values
var custom = Country.Of("custom_value");
```


## Values

| Name         | Value        |
| ------------ | ------------ |
| `Consistent` | consistent   |
| `Mismatch`   | mismatch     |
| `Unknown`    | unknown      |