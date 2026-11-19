# RecordEvidenceSignal

found_by: what found the record, which sets where its likelihood starts. agrees: a value the record shares with what the investigator gave or with a joined record. clashes: a birth date, full name, gender or country that differs from theirs.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = RecordEvidenceSignal.FoundBy;

// Open enum: use .Of() to create instances from custom string values
var custom = RecordEvidenceSignal.Of("custom_value");
```


## Values

| Name      | Value     |
| --------- | --------- |
| `FoundBy` | found_by  |
| `Agrees`  | agrees    |
| `Clashes` | clashes   |