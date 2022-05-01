using System;
using Xunit;
using Cadmus.Core;
using System.Collections.Generic;
using System.Linq;
using Cadmus.Seed.Renovella.Parts;
using Fusi.Antiquity.Chronology;

namespace Cadmus.Renovella.Parts.Test
{
    public sealed class TaleStoryPartTest
    {
        private static TaleStoryPart GetPart()
        {
            TaleStoryPartSeeder seeder = new();
            IItem item = new Item
            {
                FacetId = "default",
                CreatorId = "zeus",
                UserId = "zeus",
                Description = "Test item",
                Title = "Test Item",
                SortKey = ""
            };
            return (TaleStoryPart)seeder.GetPart(item, null, null);
        }

        private static TaleStoryPart GetEmptyPart()
        {
            return new TaleStoryPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
            };
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            TaleStoryPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            TaleStoryPart part2 = TestHelper.DeserializePart<TaleStoryPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);
            // TODO: check parts data here...
            Assert.Equal(part.Summary, part2.Summary);
        }

        [Fact]
        public void GetDataPins_Ok()
        {
            TaleStoryPart part = GetEmptyPart();
            part.Characters.Add(new StoryCharacter
            {
                Name = "Niccolò",
                Role = "peasant"
            });
            part.Characters.Add(new StoryCharacter
            {
                Name = "Aldo",
                Role = "peasant"
            });
            part.Date = HistoricalDate.Parse("1100 AD");
            part.Places = new List<StoryPlace>(
            new[]
            {
                new StoryPlace
                {
                    Type = "city",
                    Name = "Naples",
                    Location = "S. Anna"
                }
            });

            List<DataPin> pins = part.GetDataPins(null).ToList();
            Assert.Equal(7, pins.Count);

            DataPin? pin = pins.Find(p => p.Name == "character-count"
                && p.Value == "2");
            Assert.NotNull(pin!);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "character-name"
                && p.Value == "niccolo");
            Assert.NotNull(pin!);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "character-name"
                && p.Value == "aldo");
            Assert.NotNull(pin!);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "character-role"
                && p.Value == "peasant");
            Assert.NotNull(pin!);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "date-value" && p.Value == "1100");
            Assert.NotNull(pin!);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "place-type" && p.Value == "city");
            Assert.NotNull(pin!);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "place-name" && p.Value == "naples");
            Assert.NotNull(pin!);
            TestHelper.AssertPinIds(part, pin!);
        }
    }
}
