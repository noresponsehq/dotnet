# ScreeningMatchEvidenceKind

What matched. email, phone, supplied_handle and source_fact are hard identifiers; company, city and school are soft. supplied_handle is a handle on the person row; supplied_username is an account at the person's platform-free username, which alone makes a possible match; source_fact is an account a profile found by the person's lookups lists.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = ScreeningMatchEvidenceKind.Email;

// Open enum: use .Of() to create instances from custom string values
var custom = ScreeningMatchEvidenceKind.Of("custom_value");
```


## Values

| Name               | Value              |
| ------------------ | ------------------ |
| `Email`            | email              |
| `Phone`            | phone              |
| `SuppliedHandle`   | supplied_handle    |
| `SuppliedUsername` | supplied_username  |
| `SourceFact`       | source_fact        |
| `Name`             | name               |
| `Company`          | company            |
| `City`             | city               |
| `School`           | school             |