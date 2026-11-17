# EnrichmentMode

off: enrichment starts no search and suggests none. suggest: enrichment starts no search by itself, and the person report lists the searches it would start in suggested_searches. automatic: after a workspace person's own investigation or a group's screening, and each time a search on that person finishes, enrichment starts searches by itself. A group takes suggest while the workspace has not set a mode.

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