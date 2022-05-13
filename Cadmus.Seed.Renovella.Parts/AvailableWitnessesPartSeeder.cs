using Bogus;
using Cadmus.Core;
using Cadmus.Renovella.Parts;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Renovella.Parts
{
    /// <summary>
    /// AvailableWitnesses part seeder.
    /// Tag: <c>seed.it.vedph.renovella.available-witnesses</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.renovella.available-witnesses")]
    public sealed class AvailableWitnessesPartSeeder : PartSeederBase
    {
        private static char PickLetter(HashSet<char> excluded)
        {
            int limit = 100;
            char c;
            do
            {
                c = (char)('A' + Randomizer.Seed.Next(0, 26));
            } while (excluded.Contains(c) && --limit > 0);
            excluded.Add(c);
            return c;
        }

        private static List<string> GetExternalIds(Faker faker)
        {
            List<string> ids = new();
            for (int i = 0; i < faker.Random.Number(1, 3); i++)
                ids.Add(faker.Internet.Url());
            return ids;
        }

        private static IList<AvailableWitness> GenerateWitnesses(int count)
        {
            List<AvailableWitness> witnesses = new();
            HashSet<char> ids = new();

            for (int n = 1; n <= count; n++)
            {
                ids.Add(PickLetter(ids));
                witnesses.Add(new Faker<AvailableWitness>()
                    .RuleFor(w => w.Id, new string(PickLetter(ids), 1))
                    .RuleFor(w => w.IsPartial, f => f.Random.Bool(0.25f))
                    .RuleFor(w => w.Note, f => f.Random.Bool(0.25f)
                        ? f.Lorem.Sentence() : null)
                    .RuleFor(w => w.ExternalIds, f => GetExternalIds(f))
                    .Generate());
            }
            return witnesses;
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

            AvailableWitnessesPart part = new();
            SetPartMetadata(part, roleId, item);
            part.Witnesses.AddRange(
                GenerateWitnesses(Randomizer.Seed.Next(1, 5)));

            return part;
        }
    }
}
