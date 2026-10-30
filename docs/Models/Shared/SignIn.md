# SignIn

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = SignIn.Password;

// Open enum: use .Of() to create instances from custom string values
var custom = SignIn.Of("custom_value");
```


## Values

| Name        | Value       |
| ----------- | ----------- |
| `Password`  | password    |
| `Google`    | google      |
| `Facebook`  | facebook    |
| `Apple`     | apple       |
| `Microsoft` | microsoft   |
| `Sso`       | sso         |
| `Other`     | other       |