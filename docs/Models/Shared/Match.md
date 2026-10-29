# Match

exact compares the original submitted value; canonical uses identifier-specific normalisation; mailbox explicitly selects the email alias rules used by lists.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Match.Exact;

// Open enum: use .Of() to create instances from custom string values
var custom = Match.Of("custom_value");
```


## Values

| Name        | Value       |
| ----------- | ----------- |
| `Exact`     | exact       |
| `Canonical` | canonical   |
| `Mailbox`   | mailbox     |