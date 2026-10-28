# AggregateDimension

decision, recommendation and band: the first answer's. flow: the assessment's. list_match: the action of the list entry that decided the account when it was first answered. reason: a reason the first answer fired, with its impact band. lane: an evidence lane the first answer listed in incomplete_sources. country and asn: the submitted IP's, from the IP data.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = AggregateDimension.Decision;

// Open enum: use .Of() to create instances from custom string values
var custom = AggregateDimension.Of("custom_value");
```


## Values

| Name             | Value            |
| ---------------- | ---------------- |
| `Decision`       | decision         |
| `Flow`           | flow             |
| `Recommendation` | recommendation   |
| `Band`           | band             |
| `Reason`         | reason           |
| `Lane`           | lane             |
| `Country`        | country          |
| `Asn`            | asn              |
| `ListMatch`      | list_match       |