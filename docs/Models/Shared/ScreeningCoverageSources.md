# ScreeningCoverageSources

How many sources this search asked and how many answered, across the lookup stage (sources asked whether an account exists for an identifier) and the platform stage (profiles read on each platform). A source that answered may have found nothing; one that did not answer failed or ran out of time.


## Fields

| Field                                             | Type                                              | Required                                          | Description                                       |
| ------------------------------------------------- | ------------------------------------------------- | ------------------------------------------------- | ------------------------------------------------- |
| `Asked`                                           | *long*                                            | :heavy_check_mark:                                | Lookup sources plus platforms asked.              |
| `Answered`                                        | *long*                                            | :heavy_check_mark:                                | Lookup sources plus platforms that answered.      |
| `Lookups`                                         | [SourceCount](../../Models/Shared/SourceCount.md) | :heavy_check_mark:                                | N/A                                               |
| `Platforms`                                       | [SourceCount](../../Models/Shared/SourceCount.md) | :heavy_check_mark:                                | N/A                                               |