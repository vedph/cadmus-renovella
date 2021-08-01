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
- `CategoriesPart`
- `IndexKeywordsPart`
- `ExternalIdsPart`
- `NotePart`

### Tale Item

- `TaleInfoPart`\*
- `TaleStoryPart`\*
- `BibliographyPart`\*
- `DocReferencesPart`\* for mss, if just quoting the signature
- `CategoriesPart`
- `IndexKeywordsPart`
- `ExternalIdsPart`
- `NotePart`

## Parts

### TaleInfoPart

Essential information about a tale or a tales collection.

- `collectionId`: the human-readable ID of the collection. Empty if this is not a collection.
- `containerId`: the ID of the collection this tale belongs to.
- `ordinal` (`number`): the ordinal number of the tale in its container.
- `title`\* (`string`)
- `author` (`CitedPerson`)
- `place`\* (`string`)
- `date`\* (`HistoricalDate`)
- `language`\* (`string`: thesaurus `tale-languages`)
- `genres`\* (`string[]`: thesaurus `tale-genres`)
- `dedicatee` (`CitedPerson`)
- `rubric` (`string`)
- `incipit` (`string`)
- `explicit` (`string`)
- `narrator` (`string`)

### TaleStoryPart

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

