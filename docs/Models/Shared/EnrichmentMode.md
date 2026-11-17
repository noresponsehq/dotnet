# EnrichmentMode

off: enrichment starts no search. suggest: enrichment starts no search by itself. automatic: after a workspace person's own investigation, and each time a search on that person finishes, enrichment starts searches by itself.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = EnrichmentMode.Off;

// Open enum: use .Of() to create instances from custom string values
var custom = EnrichmentMode.Of("custom_value");
```


## Values

| Name        | Value       |
| ----------- | ----------- |
| `Off`       | off         |
| `Suggest`   | suggest     |
| `Automatic` | automatic   |