Testlet Randomizer
==================

## Description
Candidates are given a set of items they need to answer. This set is called a Testlet.

- There is a Testlet with a fixed set of 10 items. 6 of the items are operational and 4 of them are pretest items.
- The requirement is that the _order_ of these items should be randomized such that -
    - The first 2 items are always pretest items selected randomly from the 4 pretest items.
    - The next 8 items are mix of pretest and operational items ordered randomly from the remaining 8
items.


## Build Application
```bash
$ dotnet build .\TestletRandomizer.sln --configuration Release
```

## Test
```bash
$ dotnet test .\TestletRandomizer.sln --configuration Release
```


