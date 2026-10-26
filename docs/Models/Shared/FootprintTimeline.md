# FootprintTimeline

One identifier's dated footprint across every source: the first and last year any event was seen, the years between them, the latest year a source dated activity (omitted when none did), how many distinct sites and which kinds of evidence saw it.


## Fields

| Field                                                           | Type                                                            | Required                                                        | Description                                                     |
| --------------------------------------------------------------- | --------------------------------------------------------------- | --------------------------------------------------------------- | --------------------------------------------------------------- |
| `FirstSeenYear`                                                 | *long*                                                          | :heavy_check_mark:                                              | N/A                                                             |
| `LastSeenYear`                                                  | *long*                                                          | :heavy_check_mark:                                              | N/A                                                             |
| `SpanYears`                                                     | *long*                                                          | :heavy_check_mark:                                              | N/A                                                             |
| `LastActiveYear`                                                | *long*                                                          | :heavy_minus_sign:                                              | N/A                                                             |
| `Sites`                                                         | *long*                                                          | :heavy_check_mark:                                              | N/A                                                             |
| `Families`                                                      | List<[FootprintFamily](../../Models/Shared/FootprintFamily.md)> | :heavy_check_mark:                                              | N/A                                                             |