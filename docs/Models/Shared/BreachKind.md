# BreachKind

Who is in the table: site_breach, a site's own users; credential_list, a combolist or compilation; stealer_log, malware or botnet victims; scrape; hacking_forum, a criminal hacking, cracking or carding community; abuse_service, the customers of DDoS-for-hire, account generators, dump or card shops, darknet markets, stalkerware or breach-lookup services; darkweb_site, an onion site of unknown purpose.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = BreachKind.SiteBreach;

// Open enum: use .Of() to create instances from custom string values
var custom = BreachKind.Of("custom_value");
```


## Values

| Name             | Value            |
| ---------------- | ---------------- |
| `SiteBreach`     | site_breach      |
| `CredentialList` | credential_list  |
| `StealerLog`     | stealer_log      |
| `Scrape`         | scrape           |
| `HackingForum`   | hacking_forum    |
| `AbuseService`   | abuse_service    |
| `DarkwebSite`    | darkweb_site     |