# MatchState

How sure the latest screening is that it found the attendee's accounts. unscreened until a screening reaches the attendee, and after one that had no profile to check (no_profile_to_check).

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = MatchState.Unscreened;

// Open enum: use .Of() to create instances from custom string values
var custom = MatchState.Of("custom_value");
```


## Values

| Name           | Value          |
| -------------- | -------------- |
| `Unscreened`   | unscreened     |
| `Confirmed`    | confirmed      |
| `Probable`     | probable       |
| `Possible`     | possible       |
| `NoneFound`    | none_found     |
| `Inaccessible` | inaccessible   |