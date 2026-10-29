# Population

first_answers uses anonymous counters that can include erased detail. retained_first_answers computes exact intersections over retained first answers within 31 days. Requests exceeding the 100000-record work budget are refused.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Population.FirstAnswers;

// Open enum: use .Of() to create instances from custom string values
var custom = Population.Of("custom_value");
```


## Values

| Name                   | Value                  |
| ---------------------- | ---------------------- |
| `FirstAnswers`         | first_answers          |
| `RetainedFirstAnswers` | retained_first_answers |