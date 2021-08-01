using System;
using Xunit;
using Cadmus.Core;
using Cadmus.Seed.Renovella.Parts;
using System.Collections.Generic;
using System.Linq;
using Fusi.Antiquity.Chronology;
using Cadmus.Prosopa.Bricks;

namespace Cadmus.Renovella.Parts.Test
{
    public sealed class TaleInfoPartTest
    {
        private static TaleInfoPart GetPart()
        {
            TaleInfoPartSeeder seeder = new TaleInfoPartSeeder();
            IItem item = new Item
            {
                FacetId = "default",
                CreatorId = "zeus",
                UserId = "zeus",
                Description = "Test item",
                Title = "Test Item",
                SortKey = ""
            };
            return (TaleInfoPart)seeder.GetPart(item, null, null);
        }

        private static TaleInfoPart GetEmptyPart()
        {
            return new TaleInfoPart
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
            TaleInfoPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            TaleInfoPart part2 = TestHelper.DeserializePart<TaleInfoPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);
            // TODO: check parts data here...
        }

        private static CitedPerson GetCitedPerson(string name)
        {
            return new CitedPerson
            {
                Name = new PersonName
                {
                    Language = "en",
                    Parts = new List<PersonNamePart>
                    {
                        new PersonNamePart{ Type = "name", Value = name}
                    }
                }
            };
        }

        [Fact]
        public void GetDataPins_Ok()
        {
            TaleInfoPart part = GetEmptyPart();
            part.CollectionId = "coll-id";
            part.ContainerId = "cont-id";
            part.Title = "The -long- title";
            part.Author = GetCitedPerson("Paricò");
            part.Place = "Avalà";
            part.Date = HistoricalDate.Parse("1400 AD");
            part.Language = "ita";
            part.Genres.Add("g1");
            part.Genres.Add("g2");
            part.Dedicatee = GetCitedPerson("Cirò");
            part.Narrator = "Niccolò";

            List<DataPin> pins = part.GetDataPins(null).ToList();
            Assert.Equal(11, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "collectionId"
                && p.Value == "coll-id");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "containerId" && p.Value == "cont-id");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "title" && p.Value == "the long title");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "author" && p.Value == "parico");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "place" && p.Value == "avala");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "date-value" && p.Value == "1400");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "language" && p.Value == "ita");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "genre" && p.Value == "g1");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "genre" && p.Value == "g2");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "dedicatee" && p.Value == "ciro");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "narrator" && p.Value == "niccolo");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
        }
    }
}
