# KeyEnvironment

The environment the key belongs to. A live key assesses with live sources; a test key answers from the deterministic simulator.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = KeyEnvironment.Live;

// Open enum: use .Of() to create instances from custom string values
var custom = KeyEnvironment.Of("custom_value");
```


## Values

| Name   | Value  |
| ------ | ------ |
| `Live` | live   |
| `Test` | test   |