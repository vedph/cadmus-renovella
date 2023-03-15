using Bogus;
using Cadmus.Core;
using Cadmus.Renovella.Parts;
using Fusi.Antiquity.Chronology;
using Fusi.Tools.Configuration;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Renovella.Parts;

/// <summary>
/// Seeder for <see cref="TaleStoryPart"/>.
/// Tag: <c>seed.it.vedph.renovella.tale-story</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.renovella.tale-story")]
public sealed class TaleStoryPartSeeder : PartSeederBase
{
    private static List<StoryCharacter> GetCharacters(int min, int max)
    {
        List<StoryCharacter> characters = new();
        for (int n = 1; n <= Randomizer.Seed.Next(min, max + 1); n++)
        {
            bool isGroup = Randomizer.Seed.Next(0, 10) == 0;

            characters.Add(
                isGroup
                ? new Faker<StoryCharacter>()
                    .RuleFor(c => c.Name, f => f.PickRandom("crowd", "men"))
                    .RuleFor(c => c.Sex, '-')
                    .RuleFor(c => c.Role, f => f.PickRandom("student", "soldier"))
                    .RuleFor(c => c.IsGroup, true)
                : new Faker<StoryCharacter>()
                    .RuleFor(c => c.Name, f => f.Person.FirstName)
                    .RuleFor(c => c.Sex, f => f.PickRandom('M', 'F'))
                    .RuleFor(c => c.Role, f => f.PickRandom(
                        "soldier", "bishop"))
                    .RuleFor(c => c.IsGroup, false)
                .Generate());
        }
        return characters;
    }

    private static List<StoryPlace> GetPlaces(int min, int max)
    {
        List<StoryPlace> places = new();
        for (int n = 1; n <= Randomizer.Seed.Next(min, max + 1); n++)
        {
            places.Add(new Faker<StoryPlace>()
                .RuleFor(pl => pl.Type, f => f.PickRandom("city", "country"))
                .RuleFor(pl => pl.Name, f => f.Address.City())
                .RuleFor(pl => pl.Location, f => f.Address.StreetName()));
        }
        return places;
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
    public override IPart? GetPart(IItem item, string? roleId,
        PartSeederFactory? factory)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        TaleStoryPart part = new Faker<TaleStoryPart>()
           .RuleFor(p => p.Summary, f => f.Lorem.Sentence())
           .RuleFor(p => p.Prologue, f => f.Lorem.Sentence())
           .RuleFor(p => p.Epilogue, f => f.Lorem.Sentence())
           .RuleFor(p => p.Characters, GetCharacters(1, 3))
           .RuleFor(p => p.Date,
                f => HistoricalDate.Parse($"{Randomizer.Seed.Next(1100, 1151)} AD"))
           .RuleFor(p => p.Places, GetPlaces(1, 2))
           .Generate();
        SetPartMetadata(part, roleId, item);

        return part;
    }
}
