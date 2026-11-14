# PersonRole

The person's role in the group: guest (a ticket holder), crew, vendor or press. Leave it out when you do not know it: a person without one has no role, not guest. A later row naming the same person replaces it, and a change of role alone keeps the person's screening results.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = PersonRole.Guest;

// Open enum: use .Of() to create instances from custom string values
var custom = PersonRole.Of("custom_value");
```


## Values

| Name     | Value    |
| -------- | -------- |
| `Guest`  | guest    |
| `Crew`   | crew     |
| `Vendor` | vendor   |
| `Press`  | press    |