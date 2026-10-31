# Country

IP country, phone country, breach countries and policy_context.region together. When the breach records give a country, the timezone and locale countries count too. The IP country of a proxy or hosting network does not count.

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