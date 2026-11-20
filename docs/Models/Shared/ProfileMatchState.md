# ProfileMatchState

How sure the screening is that a profile is the person's, from the likelihood its record scores. Confirmed reaches the join bar of 0.9. Probable reaches 0.85, which takes a shared value beyond the one that found the account. Possible is the rest.

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