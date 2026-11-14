# Rating

unique: an email or phone, which belongs to one person by design. distinctive: rare enough that a new search on it likely finds one person. common: shared by many people, so a search on it finds strangers. A new search is suggested for unique and distinctive values only; any value can still be searched.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = Rating.Unique;

// Open enum: use .Of() to create instances from custom string values
var custom = Rating.Of("custom_value");
```


## Values

| Name          | Value         |
| ------------- | ------------- |
| `Unique`      | unique        |
| `Distinctive` | distinctive   |
| `Common`      | common        |