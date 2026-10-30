# AccountType

personal, or business: an account a company or school manages.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = AccountType.Personal;

// Open enum: use .Of() to create instances from custom string values
var custom = AccountType.Of("custom_value");
```


## Values

| Name       | Value      |
| ---------- | ---------- |
| `Personal` | personal   |
| `Business` | business   |