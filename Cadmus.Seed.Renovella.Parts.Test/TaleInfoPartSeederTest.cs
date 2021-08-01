using Cadmus.Core;
using Cadmus.Renovella.Parts;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Renovella.Parts.Test
{
    public sealed class TaleInfoPartSeederTest
    {
        private static readonly PartSeederFactory _factory;
        private static readonly SeedOptions _seedOptions;
        private static readonly IItem _item;

        static TaleInfoPartSeederTest()
        {
            _factory = TestHelper.GetFactory();
            _seedOptions = _factory.GetSeedOptions();
            _item = _factory.GetItemSeeder().GetItem(1, "facet");
        }

        [Fact]
        public void TypeHasTagAttribute()
        {
            Type t = typeof(TaleInfoPartSeeder);
            TagAttribute attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.it.vedph.renovella.tale-info", attr.Tag);
        }

        [Fact]
        public void Seed_Ok()
        {
            TaleInfoPartSeeder seeder = new TaleInfoPartSeeder();
            seeder.SetSeedOptions(_seedOptions);

            IPart part = seeder.GetPart(_item, null, _factory);

            Assert.NotNull(part);

            TaleInfoPart p = part as TaleInfoPart;
            Assert.NotNull(p);

            TestHelper.AssertPartMetadata(p);

            // TODO: assert properties like:
            // Assert.NotEmpty(p.Works);
        }
    }
}
