# RecordConflictSignal

anchors_disagree: records found by two of the identifiers you gave clash with each other, so those identifiers may belong to different people.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = RecordConflictSignal.AnchorsDisagree;

// Open enum: use .Of() to create instances from custom string values
var custom = RecordConflictSignal.Of("custom_value");
```


## Values

| Name              | Value             |
| ----------------- | ----------------- |
| `AnchorsDisagree` | anchors_disagree  |