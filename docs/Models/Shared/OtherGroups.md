# OtherGroups

Where else in the environment the person appears, read live from those groups: deleting one removes it here.


## Fields

| Field                                                                           | Type                                                                            | Required                                                                        | Description                                                                     |
| ------------------------------------------------------------------------------- | ------------------------------------------------------------------------------- | ------------------------------------------------------------------------------- | ------------------------------------------------------------------------------- |
| `Total`                                                                         | *long*                                                                          | :heavy_check_mark:                                                              | How many people of other groups match, of which items lists the 20 most recent. |
| `Items`                                                                         | List<[OtherGroup](../../Models/Shared/OtherGroup.md)>                           | :heavy_check_mark:                                                              | The 20 people of the most recent other groups, newest group first.              |