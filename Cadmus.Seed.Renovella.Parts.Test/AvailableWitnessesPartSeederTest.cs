using Cadmus.Core;
using Cadmus.Renovella.Parts;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Renovella.Parts.Test
{
    public sealed class AvailableWitnessesPartSeederTest
    {
        private static readonly PartSeederFactory _factory = TestHelper.GetFactory();
        private static readonly SeedOptions _seedOptions = _factory.GetSeedOptions();
        private static readonly IItem _item = _factory.GetItemSeeder().GetItem(1, "facet");

        [Fact]
        public void TypeHasTagAttribute()
        {
            Type? t = typeof(AvailableWitnessesPartSeeder);
            TagAttribute? attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.it.vedph.renovella.available-witnesses", attr.Tag);
        }

        [Fact]
        public void Seed_Ok()
        {
            AvailableWitnessesPartSeeder seeder = new();
            seeder.SetSeedOptions(_seedOptions);

            IPart? part = seeder.GetPart(_item, null, _factory);

            Assert.NotNull(part);

            AvailableWitnessesPart? p = part as AvailableWitnessesPart;
            Assert.NotNull(p);

            TestHelper.AssertPartMetadata(p);

            Assert.NotEmpty(p.Witnesses);
        }
    }
}
