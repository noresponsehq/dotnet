# GroupType

What the group's people share: an event, an organisation, a place or another attribute.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = GroupType.Event;

// Open enum: use .Of() to create instances from custom string values
var custom = GroupType.Of("custom_value");
```


## Values

| Name           | Value          |
| -------------- | -------------- |
| `Event`        | event          |
| `Organisation` | organisation   |
| `Place`        | place          |
| `Custom`       | custom         |