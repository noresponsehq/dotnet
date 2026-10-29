# RateLimitErrorCode

Machine codes an HTTP 429 answer can carry.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = RateLimitErrorCode.RateLimited;

// Open enum: use .Of() to create instances from custom string values
var custom = RateLimitErrorCode.Of("custom_value");
```


## Values

| Name                        | Value                       |
| --------------------------- | --------------------------- |
| `RateLimited`               | rate_limited                |
| `AssessmentBudgetExhausted` | assessment_budget_exhausted |
| `ExportLimitReached`        | export_limit_reached        |