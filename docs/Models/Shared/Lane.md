# Lane

An evidence lane: accounts (account presence for email and username), phone, breach and ip.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Lane.Accounts;

// Open enum: use .Of() to create instances from custom string values
var custom = Lane.Of("custom_value");
```


## Values

| Name       | Value      |
| ---------- | ---------- |
| `Accounts` | accounts   |
| `Phone`    | phone      |
| `Breach`   | breach     |
| `Ip`       | ip         |