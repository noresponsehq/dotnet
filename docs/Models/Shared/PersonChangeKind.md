# PersonChangeKind

account_added and account_removed: an account or profile appeared or disappeared. breach_added and breach_removed: a breach record appeared or disappeared. identifier_added and identifier_removed: an email, phone, username, name or account id on an account the person already held. field_changed: another field on such an account. finding_added: a new finding. posts_added: new posts, counted in count.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = PersonChangeKind.AccountAdded;

// Open enum: use .Of() to create instances from custom string values
var custom = PersonChangeKind.Of("custom_value");
```


## Values

| Name                | Value               |
| ------------------- | ------------------- |
| `AccountAdded`      | account_added       |
| `AccountRemoved`    | account_removed     |
| `BreachAdded`       | breach_added        |
| `BreachRemoved`     | breach_removed      |
| `IdentifierAdded`   | identifier_added    |
| `IdentifierRemoved` | identifier_removed  |
| `FieldChanged`      | field_changed       |
| `FindingAdded`      | finding_added       |
| `PostsAdded`        | posts_added         |