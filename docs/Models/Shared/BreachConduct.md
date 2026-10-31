# BreachConduct

What a breach table's rows record against the identifier: chargeback, one or more chargebacks; declined_payment, one or more declined payments; forum_sanction, a forum ban, warning or infraction. A category and not a value: the schema has no member for one.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = BreachConduct.Chargeback;

// Open enum: use .Of() to create instances from custom string values
var custom = BreachConduct.Of("custom_value");
```


## Values

| Name              | Value             |
| ----------------- | ----------------- |
| `Chargeback`      | chargeback        |
| `DeclinedPayment` | declined_payment  |
| `ForumSanction`   | forum_sanction    |