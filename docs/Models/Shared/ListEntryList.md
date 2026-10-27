# ListEntryList

GET /v1/workspaces/{workspace_id}/lists/{environment}: every entry of the environment's list, newest first. An environment holds at most 10,000 entries.


## Fields

| Field                                               | Type                                                | Required                                            | Description                                         |
| --------------------------------------------------- | --------------------------------------------------- | --------------------------------------------------- | --------------------------------------------------- |
| `Items`                                             | List<[ListEntry](../../Models/Shared/ListEntry.md)> | :heavy_check_mark:                                  | N/A                                                 |