# AttendeeReport

Everything known about one attendee.


## Fields

| Field                                                          | Type                                                           | Required                                                       | Description                                                    |
| -------------------------------------------------------------- | -------------------------------------------------------------- | -------------------------------------------------------------- | -------------------------------------------------------------- |
| `Attendee`                                                     | [Attendee](../../Models/Shared/Attendee.md)                    | :heavy_check_mark:                                             | An attendee and where their screening and review stand.        |
| `Profiles`                                                     | List<[Profile](../../Models/Shared/Profile.md)>                | :heavy_check_mark:                                             | The accounts the screening matched, with how each was matched. |
| `Findings`                                                     | List<[Finding](../../Models/Shared/Finding.md)>                | :heavy_check_mark:                                             | What the screening flagged, with its evidence.                 |
| `Reviews`                                                      | List<[AttendeeReview](../../Models/Shared/AttendeeReview.md)>  | :heavy_check_mark:                                             | Every recorded decision, oldest first.                         |