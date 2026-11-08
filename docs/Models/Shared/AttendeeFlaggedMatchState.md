# AttendeeFlaggedMatchState

How sure the screening is that the accounts it read are the attendee's. A finding on a possible match is at most moderate, on a probable match at most high.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = AttendeeFlaggedMatchState.Confirmed;
```


## Values

| Name           | Value          |
| -------------- | -------------- |
| `Confirmed`    | confirmed      |
| `Probable`     | probable       |
| `Possible`     | possible       |
| `NoneFound`    | none_found     |
| `Inaccessible` | inaccessible   |