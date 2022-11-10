using Bogus;
using Cadmus.Core;
using Fusi.Tools.Config;
using System;
using Cadmus.Renovella.Parts;
using System.Collections.Generic;
using Fusi.Antiquity.Chronology;

namespace Cadmus.Seed.Renovella.Parts
{
    /// <summary>
    /// Seeder for <see cref="TaleInfoPart"/> part.
    /// Tag: <c>seed.it.vedph.renovella.tale-info</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.renovella.tale-info")]
    public sealed class TaleInfoPartSeeder : PartSeederBase
    {
        private int _nextCollId;
        private readonly Dictionary<string, short> _ordinals;

        public TaleInfoPartSeeder()
        {
            _nextCollId = 1;
            _ordinals = new Dictionary<string, short>();
        }

        private static List<string> GetRandomGenre() => new()
        {
                new[] { "comic", "tragic", "moral" }[Randomizer.Seed.Next(0, 3)]
            };

        /// <summary>
        /// Creates and seeds a new part.
        /// </summary>
        /// <param name="item">The item this part should belong to.</param>
        /// <param name="roleId">The optional part role ID.</param>
        /// <param name="factory">The part seeder factory. This is used
        /// for layer parts, which need to seed a set of fragments.</param>
        /// <returns>A new part.</returns>
        /// <exception cref="ArgumentNullException">item or factory</exception>
        public override IPart? GetPart(IItem item, string? roleId,
            PartSeederFactory? factory)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            // 1 of 10 is a collection named after c + N
            string? collectionId = null;
            if (Randomizer.Seed.Next(0, 10) == 0) collectionId = "c" + _nextCollId++;
            string? containerId = collectionId != null && _nextCollId > 1
                    ? null
                    : "c" + Randomizer.Seed.Next(1, _nextCollId);
            if (containerId != null)
            {
                if (!_ordinals.ContainsKey(containerId))
                    _ordinals[containerId] = 1;
                else
                    _ordinals[containerId]++;
            }

            TaleInfoPart part = new Faker<TaleInfoPart>()
               .RuleFor(p => p.CollectionId, collectionId)
               // if it's a collection it has no container, else it has a
               // random collection as its container
               .RuleFor(p => p.ContainerId, containerId)
               .RuleFor(p => p.Ordinal,
                    containerId != null? _ordinals[containerId] : (short)0)
               .RuleFor(p => p.Title, f => f.Lorem.Sentence())
               .RuleFor(p => p.Author, SeederHelper.GetCitedPerson())
               .RuleFor(p => p.Place, f => f.Address.City())
               .RuleFor(p => p.Date,
                    f => HistoricalDate.Parse($"{f.Random.Number(1400, 1500)} AD"))
               .RuleFor(p => p.Language, f => f.PickRandom("ita", "lat"))
               .RuleFor(p => p.Genres, GetRandomGenre())
               .RuleFor(p => p.Dedicatee, SeederHelper.GetCitedPerson())
               .RuleFor(p => p.Rubric, f => f.Lorem.Sentence())
               .RuleFor(p => p.Incipit, f => f.Lorem.Sentence())
               .RuleFor(p => p.Explicit, f => f.Lorem.Sentence())
               .RuleFor(p => p.Narrator, f => f.Person.FirstName);

            SetPartMetadata(part, roleId, item);

            return part;
        }
    }
}
