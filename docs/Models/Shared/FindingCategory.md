# FindingCategory

What a finding is about: a direct threat to the principal or the event, intent to harm without a named target, fixation on the principal, a grievance against the host, research into the venue, route or security, weapons tied to any of these, disruption or protest planning toward this event, violent extremist content or identification, prior harassment or stalking of staff or principals, or posting the principal's addresses or movements.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = FindingCategory.DirectThreat;

// Open enum: use .Of() to create instances from custom string values
var custom = FindingCategory.Of("custom_value");
```


## Values

| Name               | Value              |
| ------------------ | ------------------ |
| `DirectThreat`     | direct_threat      |
| `HarmIntent`       | harm_intent        |
| `Fixation`         | fixation           |
| `Grievance`        | grievance          |
| `SecurityResearch` | security_research  |
| `Weapons`          | weapons            |
| `Disruption`       | disruption         |
| `ViolentExtremism` | violent_extremism  |
| `Harassment`       | harassment         |
| `MovementExposure` | movement_exposure  |