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
- `author` (`CitedPerson`):
	- name (`ProperName`):
		- language (`string`)
		- tag (`string`)
		- pieces (ProperNamePiece[]):
		  - type (`string`)
		  - value (`string`)
	- rank (`short`)
	- ids (`DecoratedId[]`):
		- id (`string`)
		- rank (`int`)
		- tag (`string`)
		- sources (`DocReference[]`)
	- sources (`DocReference[]`):
		- type (`string`)
		- tag (`string`)
		- citation (`string`)
		- note (`string`)
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

## History

- 2021-11-11: upgraded to NET 6.
- 2021-10-25: updated packages.
- 2021-10-16: refactored dependencies so that `DocReference` and `ProperName` (formerly `PersonName`) are from bricks; also, the sub-model cited person is no more from Itinera, but defined inside this project. This implies these changes in the database parts of type `TaleInfoPart`:
  
  - rename: `author` and `dedicatee` have: `name.pieces` instead of `name.parts`.
	- remodel: `ids.sources` have new DocReference instead of `tag`, `author`, `work`, `location`. We can merge the last 3 fields into `citation` following some convention.
	- remodel: `sources` as above.
