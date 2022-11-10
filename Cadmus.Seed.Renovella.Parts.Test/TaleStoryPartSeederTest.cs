using Cadmus.Core;
using Cadmus.Renovella.Parts;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Renovella.Parts.Test
{
    public sealed class TaleStoryPartSeederTest
    {
        private static readonly PartSeederFactory _factory;
        private static readonly SeedOptions _seedOptions;
        private static readonly IItem _item;

        static TaleStoryPartSeederTest()
        {
            _factory = TestHelper.GetFactory();
            _seedOptions = _factory.GetSeedOptions();
            _item = _factory.GetItemSeeder().GetItem(1, "facet")!;
        }

        [Fact]
        public void TypeHasTagAttribute()
        {
            Type t = typeof(TaleStoryPartSeeder);
            TagAttribute? attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.it.vedph.renovella.tale-story", attr.Tag);
        }

        [Fact]
        public void Seed_Ok()
        {
            TaleStoryPartSeeder seeder = new TaleStoryPartSeeder();
            seeder.SetSeedOptions(_seedOptions);

            IPart? part = seeder.GetPart(_item, null, _factory);

            Assert.NotNull(part);

            TaleStoryPart? p = part as TaleStoryPart;
            Assert.NotNull(p);

            TestHelper.AssertPartMetadata(p);

            // TODO: assert properties like:
            Assert.NotNull(p.Summary);
            // Assert.NotEmpty(p.Works);
        }
    }
}
