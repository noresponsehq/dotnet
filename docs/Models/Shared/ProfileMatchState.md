# ProfileMatchState

How sure the screening is that a profile is the person's, from its evidence: confirmed is a handle on the person row, or two or more hard identifiers (email, phone, a supplied handle, or a fact an existing source returned); probable is one identifier beyond the name; possible is the name alone.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = ProfileMatchState.Confirmed;

// Open enum: use .Of() to create instances from custom string values
var custom = ProfileMatchState.Of("custom_value");
```


## Values

| Name        | Value       |
| ----------- | ----------- |
| `Confirmed` | confirmed   |
| `Probable`  | probable    |
| `Possible`  | possible    |