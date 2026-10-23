# EventType

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = EventType.AssessmentUpdated;

// Open enum: use .Of() to create instances from custom string values
var custom = EventType.Of("custom_value");
```


## Values

| Name                | Value               |
| ------------------- | ------------------- |
| `AssessmentUpdated` | assessment.updated  |