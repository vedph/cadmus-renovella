﻿using Bogus;
using Cadmus.Bricks;
using Cadmus.Parts;
using Cadmus.Prosopa.Bricks;
using Cadmus.Renovella.Parts;
using System.Collections.Generic;
using DocReference = Cadmus.Bricks.DocReference;

namespace Cadmus.Seed.Renovella.Parts
{
    /// <summary>
    /// Seeder helper.
    /// </summary>
    internal static class SeederHelper
    {
        /// <summary>
        /// Gets a random number of document references.
        /// </summary>
        /// <param name="min">The min number of references to get.</param>
        /// <param name="max">The max number of references to get.</param>
        /// <returns>References.</returns>
        public static List<DocReference> GetDocReferences(int min, int max)
        {
            List<DocReference> refs = new List<DocReference>();

            for (int n = 1; n <= Randomizer.Seed.Next(min, max + 1); n++)
            {
                refs.Add(new Faker<DocReference>()
                    .RuleFor(r => r.Tag, f => f.PickRandom(null, "tag"))
                    .RuleFor(r => r.Author, f => f.Lorem.Word())
                    .RuleFor(r => r.Work, f => f.Lorem.Word())
                    .RuleFor(r => r.Location,
                        f => $"{f.Random.Number(1, 24)}.{f.Random.Number(1, 1000)}")
                    .RuleFor(r => r.Note, f => f.Lorem.Sentence())
                    .Generate());
            }

            return refs;
        }

        public static List<DecoratedId> GetDecoratedIds(int min, int max)
        {
            List<DecoratedId> ids = new List<DecoratedId>();

            for (int n = 1; n <= Randomizer.Seed.Next(min, max + 1); n++)
            {
                ids.Add(new Faker<DecoratedId>()
                    .RuleFor(i => i.Id, f => f.Lorem.Word())
                    .RuleFor(i => i.Rank, f => f.Random.Short(1, 3))
                    .RuleFor(i => i.Tag, f => f.PickRandom(null, f.Lorem.Word()))
                    .RuleFor(i => i.Sources, GetDocReferences(min, max))
                    .Generate());
            }

            return ids;
        }

        public static CitedPerson GetCitedPerson()
        {
            return new Faker<CitedPerson>()
                .RuleFor(t => t.Ids, GetDecoratedIds(1, 2))
                .RuleFor(t => t.Sources, GetDocReferences(1, 3))
                .RuleFor(t => t.Name, new Faker<PersonName>()
                    .RuleFor(pn => pn.Language, "lat")
                    .RuleFor(pn => pn.Parts, f =>
                        new List<PersonNamePart>(new[]
                        {
                            new PersonNamePart
                            {
                                Type = "first",
                                Value = f.Lorem.Word(),
                            },
                            new PersonNamePart
                            {
                                Type = "last",
                                Value = f.Lorem.Word(),
                            }
                        })))
                .RuleFor(t => t.Rank, f => f.Random.Short(0, 3))
                .Generate();
        }
    }
}
