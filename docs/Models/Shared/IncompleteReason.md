# IncompleteReason

Why a lane did not settle. deferred means the lane is asynchronous and still running; a continuation settles it. timed_out means the lane missed its deadline. insufficient_evidence is a settled inconclusive signal rather than an incomplete lane.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = IncompleteReason.TimedOut;

// Open enum: use .Of() to create instances from custom string values
var custom = IncompleteReason.Of("custom_value");
```


## Values

| Name          | Value         |
| ------------- | ------------- |
| `TimedOut`    | timed_out     |
| `RateLimited` | rate_limited  |
| `Blocked`     | blocked       |
| `SourceError` | source_error  |
| `Unavailable` | unavailable   |
| `Deferred`    | deferred      |