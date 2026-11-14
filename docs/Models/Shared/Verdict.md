# Verdict

A reviewer's overrule (POST .../people/{person_id}/records), which decides joined whatever the likelihood and sticks for as long as a lookup finds the same record. Absent when nobody overruled it.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Verdict.Joined;

// Open enum: use .Of() to create instances from custom string values
var custom = Verdict.Of("custom_value");
```


## Values

| Name       | Value      |
| ---------- | ---------- |
| `Joined`   | joined     |
| `Rejected` | rejected   |