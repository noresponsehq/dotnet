# BreachSelector

The submitted identifier a breach table matched on: email or username. Phone and name are not breach selectors.

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