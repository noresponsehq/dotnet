# AssessmentAggregateMetric

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = AssessmentAggregateMetric.Count;

// Open enum: use .Of() to create instances from custom string values
var custom = AssessmentAggregateMetric.Of("custom_value");
```


## Values

| Name             | Value            |
| ---------------- | ---------------- |
| `Count`          | count            |
| `ScoreHistogram` | score_histogram  |
| `ScoreOutcomes`  | score_outcomes   |
| `LateFlips`      | late_flips       |