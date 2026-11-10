# AttendeeRole

The attendee's role at the event: guest (a ticket holder), crew, vendor or press. Leave it out when you do not know it: an attendee without one has no role, not guest. A later row naming the same attendee replaces it, and a change of role alone keeps the attendee's screening results.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = AttendeeRole.Guest;

// Open enum: use .Of() to create instances from custom string values
var custom = AttendeeRole.Of("custom_value");
```


## Values

| Name     | Value    |
| -------- | -------- |
| `Guest`  | guest    |
| `Crew`   | crew     |
| `Vendor` | vendor   |
| `Press`  | press    |