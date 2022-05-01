# Cadmus Re.Novella

## Submodels

Cross-parts submodels:

- `CitedPerson`
- `HistoricalDate`: referenced from Cadmus.Parts.

## Items

### TaleCollection Item

- `TaleInfoPart`\*
- `TaleStoryPart`\*
- `BibliographyPart`\*
- `PoemsInfoPart`
- `CategoriesPart`
- `IndexKeywordsPart`
- `ExternalIdsPart`
- `NotePart`

### Tale Item

- `TaleInfoPart`\*
- `TaleStoryPart`\*
- `BibliographyPart`\*
- `PoemsInfoPart`
- `DocReferencesPart`\* for mss, if just quoting the signature
- `CategoriesPart`
- `IndexKeywordsPart`
- `ExternalIdsPart`
- `NotePart`

## Parts

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
- `place`\* (`string`)
- `date`\* (`HistoricalDate`)
- `language`\* (`string`: thesaurus `tale-languages`)
- `genres`\* (`string[]`: thesaurus `tale-genres`)
- `structure` (string)
- `dedicatee` (`CitedPerson`)
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
- `characters`\* (`TaleCharacter[]`)
  - `name` (`string`)
  - `sex` (`string`; `M` or `F` or `-` when not applicable)
  - `role`\* (`string`: thesaurus `story-roles`)
  - `isGroup` (`boolean`): true if this is a collective designation for a group (e.g. "facchini")
- `date`\* (`HistoricalDate`)
- `places`\* (`StoryPlace`):
  - `type`\* (`string`, thesaurus `story-place-types`: e.g. city, country, etc)
  - `place` (`string`)
  - `location` (`string`): location inside place (e.g. "Chiesa di S.Anna")

### PoeticTextsPart

ID: `it.vedph.renovella.poetic-texts`.

- texts (`PoeticText[]`):
  - `metre`\* (string) T:poetic-text-metres
  - `incipit`\* (string)
  - `note` (string)

## History

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

### 2.0.0

- 2022-05-01: upgraded to NET6.0.

### 1.2.8

- 2022-04-21: updated packages.
