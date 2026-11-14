# RecordRole

Which common field a value is, where many sources share it: a join date and a birth date are both dates, with the roles joined and born. A platform account's field and a breach row's column with the same meaning carry the same kind and role. A linked_account field is named by its network. Absent for a field only its source returns.

## Example Usage

```csharp
using NoResponse.Models.Shared;

var value = RecordRole.AccountId;

// Open enum: use .Of() to create instances from custom string values
var custom = RecordRole.Of("custom_value");
```


## Values

| Name              | Value             |
| ----------------- | ----------------- |
| `AccountId`       | account_id        |
| `AccountStatus`   | account_status    |
| `AccountType`     | account_type      |
| `AlternatePhone`  | alternate_phone   |
| `Balance`         | balance           |
| `Banned`          | banned            |
| `Bio`             | bio               |
| `Born`            | born              |
| `Bot`             | bot               |
| `CallerCategory`  | caller_category   |
| `CoverPicture`    | cover_picture     |
| `Deleted`         | deleted           |
| `DeletedOn`       | deleted_on        |
| `Department`      | department        |
| `DisplayName`     | display_name      |
| `EmailVerified`   | email_verified    |
| `Expires`         | expires           |
| `FirstName`       | first_name        |
| `Followers`       | followers         |
| `Following`       | following         |
| `Friends`         | friends           |
| `FullAddress`     | full_address      |
| `FullName`        | full_name         |
| `Handle`          | handle            |
| `Headline`        | headline          |
| `Interests`       | interests         |
| `JobTitle`        | job_title         |
| `Joined`          | joined            |
| `LastActive`      | last_active       |
| `LastIp`          | last_ip           |
| `LastName`        | last_name         |
| `LastPost`        | last_post         |
| `LastUpdated`     | last_updated      |
| `Level`           | level             |
| `LikesGiven`      | likes_given       |
| `LikesReceived`   | likes_received    |
| `LineType`        | line_type         |
| `LinkedAccount`   | linked_account    |
| `Location`        | location          |
| `MarkedSpam`      | marked_spam       |
| `MiddleName`      | middle_name       |
| `Nationality`     | nationality       |
| `Organisation`    | organisation      |
| `OtherIp`         | other_ip          |
| `Paid`            | paid              |
| `PasswordChanged` | password_changed  |
| `Picture`         | picture           |
| `Points`          | points            |
| `Posts`           | posts             |
| `Private`         | private           |
| `ProfileLink`     | profile_link      |
| `ProfileViews`    | profile_views     |
| `Rating`          | rating            |
| `Referrals`       | referrals         |
| `Referrer`        | referrer          |
| `ReportCount`     | report_count      |
| `Reputation`      | reputation        |
| `ReputationLabel` | reputation_label  |
| `Residence`       | residence         |
| `Salutation`      | salutation        |
| `School`          | school            |
| `Signature`       | signature         |
| `SignupIp`        | signup_ip         |
| `SiteRole`        | site_role         |
| `SpamScore`       | spam_score        |
| `Street`          | street            |
| `Verified`        | verified          |
| `Warnings`        | warnings          |
| `Website`         | website           |