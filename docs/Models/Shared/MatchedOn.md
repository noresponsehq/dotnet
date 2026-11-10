# MatchedOn

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = MatchedOn.Email;

// Open enum: use .Of() to create instances from custom string values
var custom = MatchedOn.Of("custom_value");
```


## Values

| Name         | Value        |
| ------------ | ------------ |
| `Email`      | email        |
| `Phone`      | phone        |
| `ExternalId` | external_id  |