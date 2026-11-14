# SearchStatus

A password or hash search is counting, then counted: rows says how many breach rows hold the value, and nothing runs until a person runs it (POST .../searches/{search_id}/run). Any other search is queued, then running. completed: every source it asked answered. partial: some did not. failed: none answered or none could be asked, which says nothing about whether the value is held anywhere.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = SearchStatus.Counting;

// Open enum: use .Of() to create instances from custom string values
var custom = SearchStatus.Of("custom_value");
```


## Values

| Name        | Value       |
| ----------- | ----------- |
| `Counting`  | counting    |
| `Counted`   | counted     |
| `Queued`    | queued      |
| `Running`   | running     |
| `Completed` | completed   |
| `Partial`   | partial     |
| `Failed`    | failed      |