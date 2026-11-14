# SearchKind

The kind of the value searched.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = SearchKind.Email;

// Open enum: use .Of() to create instances from custom string values
var custom = SearchKind.Of("custom_value");
```


## Values

| Name         | Value        |
| ------------ | ------------ |
| `Email`      | email        |
| `Phone`      | phone        |
| `Username`   | username     |
| `PersonName` | person_name  |
| `Password`   | password     |
| `Hash`       | hash         |
| `Ip`         | ip           |