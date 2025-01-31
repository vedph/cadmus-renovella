# Cadmus Re.Novella

## Submodels

Cross-parts submodels:

- `CitedPerson`:
  - `ids` (`DecoratedId[]`):
    - `id`
    - `rank`
    - `tag`
    - `sources` (`DocReference[]`):
      - `type`
      - `tag`
      - `citation`
      - `note`
  - `sources` (`DocReference[]`)
  - `name` (`ProperName`):
    - `language`
    - `tag`
    - `pieces` (`ProperNamePiece[]`):
      - `type`
      - `value`
  - `rank`
- `HistoricalDate`: referenced from `Cadmus.Parts`.

## Items

### TaleCollection Item

- `TaleInfoPart`\* (`it.vedph.renovella.tale-info`)
- `TaleStoryPart`\* (`it.vedph.renovella.tale-story`)
- `AvailableWitnessesPart` (`it.vedph.renovella.available-witnesses`)
- `BibliographyPart`\* (`it.vedph.bibliography`)
- `PoeticTextsPart` (`it.vedph.renovella.poetic-texts`)
- `CategoriesPart` (`it.vedph.categories`)
- `IndexKeywordsPart` (`it.vedph.index-keywords`)
- `ExternalIdsPart` (`it.vedph.external-ids`)
- `NotePart` (`it.vedph.note"`)

### Tale Item

- `TaleInfoPart`\* (`it.vedph.renovella.tale-info`)
- `TaleStoryPart`\* (`it.vedph.renovella.tale-story`)
- `AvailableWitnessesPart` (`it.vedph.renovella.available-witnesses`)
- `BibliographyPart`\* (`it.vedph.bibliography`)
- `PoeticTextsPart` (`it.vedph.renovella.poetic-texts`)
- `CategoriesPart` (`it.vedph.categories`)
- `IndexKeywordsPart` (`it.vedph.index-keywords`)
- `ExternalIdsPart` (`it.vedph.external-ids`)
- `NotePart` (`it.vedph.note"`)
- `DocReferencesPart`\* for mss, just quoting the signature (`it.vedph.doc-references`)

## Parts

### AvailableWitnessesPart

ID: `it.vedph.renovella.available-witnesses`

Information about witnesses (essentially manuscripts) available for the specified item (tale or tales collection).

- `witnesses` (`AvailableWitness`):
  - `id`\* (`string`)
  - `isPartial` (`boolean`)
  - `note` (`string`)
  - `externalIds` (`string[]`)

### TaleInfoPart

ID: `it.vedph.renovella.tale-info`

Essential information about a tale or a tales collection.

- `collectionId`: the human-readable ID of the collection. Empty if this is not a collection.
- `containerId`: the ID of the collection this tale belongs to.
- `ordinal` (`number`): the ordinal number of the tale in its container.
- `title`\* (`string`)
- `author` (`CitedPerson`):
  - `name` (`ProperName`):
    - `language` (`string`)
    - `tag` (`string`)
    - `pieces` (`ProperNamePiece[]`):
      - `type` (`string`)
      - `value` (`string`)
  - `rank` (`short`)
  - `ids` (`DecoratedId[]`):
    - `id` (`string`)
    - `rank` (`int`)
    - `tag` (`string`)
    - `sources` (`DocReference[]`)
  - `sources` (`DocReference[]`):
    - `type` (`string`)
    - `tag` (`string`)
    - `citation` (`string`)
    - `note` (`string`)
- `dedicatee` (`CitedPerson`)
- `place`\* (`string`)
- `date`\* (`HistoricalDate`)
- `language` (`string`: thesaurus `tale-languages`)
- `genres`\* (`string[]`: thesaurus `tale-genres`)
- `structure` (`string`)
- `rubric` (`string`)
- `incipit` (`string`)
- `explicit` (`string`)
- `narrator` (`string`)

### TaleStoryPart

ID: `it.vedph.renovella.tale-story`

Data about the tale's story.

- `summary`\* (`string`)
- `prologue` (`string`)
- `epilogue` (`string`)
- `characters`\* (`StoryCharacter[]`)
  - `name` (`string`)
  - `sex` (`string`; `M` or `F` or `-` when not applicable)
  - `role`\* (`string`: thesaurus `story-roles`)
  - `isGroup` (`boolean`): true if this is a collective designation for a group (e.g. "facchini")
- `age` (`string`)
- `date`\* (`HistoricalDate`)
- `places`\* (`StoryPlace`):
  - `type`\* (`string`, thesaurus `story-place-types`: e.g. city, country, etc)
  - `place` (`string`)
  - `location` (`string`): location inside place (e.g. "Chiesa di S.Anna")

### PoeticTextsPart

ID: `it.vedph.renovella.poetic-texts`.

- texts (`PoeticText[]`):
  - `incipit`\* (string)
  - `metre`\* (string) T:poetic-text-metres
  - `note` (string)

## History

### 7.0.1

- 2025-01-31: updated packages.

### 7.0.0

- 2024-11-26: ⚠️ upgraded to .NET 9.

### 6.0.4

- 2024-10-05: updated packages.

### 6.0.3

- 2024-07-19: updated packages.

### 6.0.2

- 2024-06-10: updated packages.

### 6.0.1

- 2024-05-24: updated packages.
- 2024-04-16: updated test packages.

### 6.0.0

- 2024-04-08: upgraded to .NET 8.

### 5.0.2

- 2023-09-29: updated packages.
- 2023-08-30: updated test packages.

### 5.0.1

- 2023-07-26:
  - updated packages.
  - removed `structure` pin from `TaleInfoPart` as it contains a summary rather than a short definition.

### 5.0.0

- 2023-06-23: updated packages.

### 4.0.1

- 2023-05-19: updated packages.

### 4.0.0

- 2023-03-15: migrated to [new backend configuration](https://myrmex.github.io/overview/cadmus/dev/history/b-config).

### 3.0.1

- 2022-12-23: updated packages.

### 3.0.0

- 2022-11-10: upgraded to NET 7.

### 2.1.0

- 2022-10-11: updated packages (`IRepositoryProvider`).

### 2.0.1

- 2022-02-18: completed PoeticTextsPart.
- 2022-02-08: upgraded packages.
- 2021-11-11: upgraded to NET 6.
- 2021-10-25: updated packages.
- 2021-10-16: refactored dependencies so that `DocReference` and `ProperName` (formerly `PersonName`) are from bricks; also, the sub-model cited person is no more from Itinera, but defined inside this project. This implies these changes in the database parts of type `TaleInfoPart`:
  
  - rename: `author` and `dedicatee` have: `name.pieces` instead of `name.parts`.
    - remodel: `ids.sources` have new `DocReference` instead of `tag`, `author`, `work`, `location`. We can merge the last 3 fields into `citation` following some convention. The new model has `type`, `tag`, `citation`, `note`.
    - remodel: `sources` as above.

Upgrade instructions:

(1) **TaleInfoPart** for author:

```js
// get records before update
db.parts.find({
    "typeId" : "it.vedph.renovella.tale-info",
    "content.author.name.parts" : {
        $exists : true
    }
});

// update renaming 'parts' into 'pieces'
db.parts.updateMany(
{
"typeId" : "it.vedph.renovella.tale-info",
"content.author.name.parts" : { $exists : true }
},
{
$rename: {
"content.author.name.parts": "content.author.name.pieces"
}
}, false, true);
```

(2) **TaleInfoPart** for dedicatee:

```js
// get records before update
db.parts.find({
    "typeId" : "it.vedph.renovella.tale-info",
    "content.dedicatee.name.parts" : {
        $exists : true
    }
});

// update renaming 'parts' into 'pieces'
db.parts.updateMany(
{
"typeId" : "it.vedph.renovella.tale-info",
"content.dedicatee.name.parts" : { $exists : true }
},
{
$rename: {
"content.dedicatee.name.parts": "content.dedicatee.name.pieces"
}
}, false, true);
```

(3) **TaleInfoPart** for references:

```js
// get all the author's sources
db.parts.find({
    "typeId" : "it.vedph.renovella.tale-info",
    "content.author.sources" : {
        $exists : true, $ne: []
    }
});
```

As we just have 1 case and it's not clear how the team prefers to handle this, we have better leave this to manual editing.

(4) **TaleInfoPart** for ID references:

```js
// get all the author's IDs sources
db.parts.find({
    "typeId" : "it.vedph.renovella.tale-info",
    "content.author.ids.sources" : {
        $exists : true, $ne: []
    }
});
```

As we just have 6 cases and it's not clear how the team prefers to handle this, we have better leave this to manual editing.

## History

### 2.0.1

- 2022-05-12: added `AvailableWitnessesPart`.

### 2.0.0

- 2022-05-01: upgraded to NET6.0.

### 1.2.8

- 2022-04-21: updated packages.
