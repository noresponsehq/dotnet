# FootprintFamily

The source-neutral kind of evidence: account_presence (an account found on a site), auth_provider (the account at your auth provider), breach (breach and credential tables), breach_abuse (abuse and fraud communities' tables) and code_hosting (a code hosting profile).

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = FootprintFamily.AccountPresence;

// Open enum: use .Of() to create instances from custom string values
var custom = FootprintFamily.Of("custom_value");
```


## Values

| Name              | Value             |
| ----------------- | ----------------- |
| `AccountPresence` | account_presence  |
| `AuthProvider`    | auth_provider     |
| `Breach`          | breach            |
| `BreachAbuse`     | breach_abuse      |
| `CodeHosting`     | code_hosting      |