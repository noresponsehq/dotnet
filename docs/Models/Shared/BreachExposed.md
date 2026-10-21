# BreachExposed

The kind of data a breach table held beside the identifier. password means a plaintext password was present and password_hash means a hash, salt or token was present. The value itself is not stored or returned; the schema has no member for one.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = BreachExposed.Email;

// Open enum: use .Of() to create instances from custom string values
var custom = BreachExposed.Of("custom_value");
```


## Values

| Name           | Value          |
| -------------- | -------------- |
| `Email`        | email          |
| `Username`     | username       |
| `Name`         | name           |
| `Password`     | password       |
| `PasswordHash` | password_hash  |
| `Phone`        | phone          |
| `Address`      | address        |
| `Dob`          | dob            |
| `Ip`           | ip             |
| `Payment`      | payment        |
| `SecurityQa`   | security_qa    |