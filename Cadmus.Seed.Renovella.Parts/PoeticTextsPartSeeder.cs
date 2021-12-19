using Bogus;
using Cadmus.Core;
using Cadmus.Renovella.Parts;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Renovella.Parts
{
    /// <summary>
    /// Seeder for <see cref="PoeticTextsPart"/>.
    /// Tag: <c>seed.it.vedph.renovella.poetic-texts</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.renovella.poetic-texts")]
    public sealed class PoeticTextsPartSeeder : PartSeederBase
    {
        private static IList<PoeticText> GetTexts(int count)
        {
            var metres = new string[] { "sonetto", "ballata" };
            List<PoeticText> texts = new List<PoeticText>(count);

            for (int n = 1; n <= count; n++)
            {
                texts.Add(new Faker<PoeticText>()
                    .RuleFor(t => t.Incipit, f => f.Lorem.Sentence())
                    .RuleFor(t => t.Metre, f => f.PickRandom(metres))
                    .RuleFor(t => t.Note,
                        f => f.Random.Bool(0.25f)? f.Lorem.Sentence() : null)
                    .Generate());
            }

            return texts;
        }

        /// <summary>
        /// Creates and seeds a new part.
        /// </summary>
        /// <param name="item">The item this part should belong to.</param>
        /// <param name="roleId">The optional part role ID.</param>
        /// <param name="factory">The part seeder factory. This is used
        /// for layer parts, which need to seed a set of fragments.</param>
        /// <returns>A new part.</returns>
        /// <exception cref="ArgumentNullException">item or factory</exception>
        public override IPart GetPart(IItem item, string roleId,
            PartSeederFactory factory)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            PoeticTextsPart part = new Faker<PoeticTextsPart>()
                .RuleFor(p => p.Texts, f => GetTexts(f.Random.Number(1, 3)))
                .Generate();
            SetPartMetadata(part, roleId, item);

            return part;
        }
    }
}
