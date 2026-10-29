# ResultStatus

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = ResultStatus.NoChange;

// Open enum: use .Of() to create instances from custom string values
var custom = ResultStatus.Of("custom_value");
```


## Values

| Name                     | Value                    |
| ------------------------ | ------------------------ |
| `NoChange`               | no_change                |
| `ProposalRequiresReview` | proposal_requires_review |