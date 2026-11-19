# Stage

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Stage.Lookup;

// Open enum: use .Of() to create instances from custom string values
var custom = Stage.Of("custom_value");
```


## Values

| Name            | Value           |
| --------------- | --------------- |
| `Lookup`        | lookup          |
| `LookupTyping`  | lookup_typing   |
| `Platforms`     | platforms       |
| `Written`       | written         |
| `LatePlatforms` | late_platforms  |
| `LateLookup`    | late_lookup     |
| `AiTyping`      | ai_typing       |
| `Analysis`      | analysis        |
| `Rating`        | rating          |