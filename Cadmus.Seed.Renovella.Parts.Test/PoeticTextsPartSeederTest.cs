using Cadmus.Core;
using Cadmus.Renovella.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Seed.Renovella.Parts.Test
{
    public sealed class PoeticTextsPartSeederTest
    {
        private static PoeticTextsPart GetPart()
        {
            PoeticTextsPartSeeder seeder = new();
            IItem item = new Item
            {
                FacetId = "default",
                CreatorId = "zeus",
                UserId = "zeus",
                Description = "Test item",
                Title = "Test Item",
                SortKey = ""
            };
            return (PoeticTextsPart)seeder.GetPart(item, null, null)!;
        }

        private static PoeticTextsPart GetEmptyPart()
        {
            return new PoeticTextsPart
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
            PoeticTextsPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            PoeticTextsPart? part2 =
                TestHelper.DeserializePart<PoeticTextsPart>(json);

            Assert.NotNull(part2);
            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(part.Texts.Count, part2.Texts.Count);
        }

        [Fact]
        public void GetDataPins_NoEntries_Ok()
        {
            PoeticTextsPart part = GetPart();
            part.Texts.Clear();

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Single(pins);
            DataPin pin = pins[0];
            Assert.Equal("tot-count", pin.Name);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("0", pin.Value);
        }

        [Fact]
        public void GetDataPins_Entries_Ok()
        {
            PoeticTextsPart part = GetEmptyPart();

            for (int n = 1; n <= 3; n++)
            {
                part.Texts.Add(new PoeticText
                {
                    Incipit = $"incipit {(char)('A' - 1 + n)}",
                    Metre = "m" + n
                });
            }

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(7, pins.Count);

            DataPin? pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            pin = pins.Find(p => p.Name == "incipit" && p.Value == "incipit a");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "incipit" && p.Value == "incipit b");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "incipit" && p.Value == "incipit c");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            for (int n = 1; n <= 3; n++)
            {
                pin = pins.Find(p => p.Name == "metre" && p.Value == "m" + n);
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
            }
        }
    }
}
