# BreachSelector

The submitted email or username whose own search matched the table. A submitted full name is searched as well, and what it finds feeds the identity's reasons rather than this list.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = BreachSelector.Email;

// Open enum: use .Of() to create instances from custom string values
var custom = BreachSelector.Of("custom_value");
```


## Values

| Name       | Value      |
| ---------- | ---------- |
| `Email`    | email      |
| `Username` | username   |
| `Ip`       | ip         |