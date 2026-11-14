# PostsStopped

Why fewer posts were read than the screening's depth asks for while the account holds more. platform_limit: the platform shows the screening no more of them. page_limit: the pages a screening reads ran out first. rate_limited or unavailable: a later page was held back or failed; the posts before it are kept and the person is partial. Absent when the account was read to its end or to the depth.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = PostsStopped.PlatformLimit;

// Open enum: use .Of() to create instances from custom string values
var custom = PostsStopped.Of("custom_value");
```


## Values

| Name            | Value           |
| --------------- | --------------- |
| `PlatformLimit` | platform_limit  |
| `PageLimit`     | page_limit      |
| `RateLimited`   | rate_limited    |
| `Unavailable`   | unavailable     |