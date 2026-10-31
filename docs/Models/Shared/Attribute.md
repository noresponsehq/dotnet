# Attribute

name_gender is the records' gender against the submitted first name's usual one.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Attribute.BirthDate;

// Open enum: use .Of() to create instances from custom string values
var custom = Attribute.Of("custom_value");
```


## Values

| Name          | Value         |
| ------------- | ------------- |
| `BirthDate`   | birth_date    |
| `Gender`      | gender        |
| `Nationality` | nationality   |
| `NameGender`  | name_gender   |