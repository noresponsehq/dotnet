# ProfileCitationKind

record: a record in lookup.records or profiles[].record. post: an item in posts. check: an item in lookup.checks.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = ProfileCitationKind.Record;

// Open enum: use .Of() to create instances from custom string values
var custom = ProfileCitationKind.Of("custom_value");
```


## Values

| Name     | Value    |
| -------- | -------- |
| `Record` | record   |
| `Post`   | post     |
| `Check`  | check    |