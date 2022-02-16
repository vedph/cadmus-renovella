using Cadmus.Cli.Core;
using Cadmus.Core.Config;
using Cadmus.Seed;
using Fusi.Microsoft.Extensions.Configuration.InMemoryJson;
using Fusi.Tools.Config;
using Microsoft.Extensions.Configuration;
using SimpleInjector;
using System.Reflection;
using Cadmus.Seed.Renovella.Parts;
using Cadmus.Seed.General.Parts;

namespace Cadmus.Cli.Plugin.Renovella
{
    /// <summary>
    /// CLI part seeder factory provider for Renovella.
    /// Tag: <c>cli-seeder-factory-provider.renovella</c>.
    /// </summary>
    /// <seealso cref="ICliPartSeederFactoryProvider" />
    [Tag("cli-seeder-factory-provider.renovella")]
    public sealed class RenovellaCliPartSeederFactoryProvider :
        ICliPartSeederFactoryProvider
    {
        public PartSeederFactory GetFactory(string profile)
        {
            if (profile == null)
                throw new ArgumentNullException(nameof(profile));

            // build the tags to types map for parts/fragments
            Assembly[] seedAssemblies = new[]
            {
                // Cadmus.Seed.General.Parts
                typeof(NotePartSeeder).Assembly,
                // Cadmus.Seed.Renovella.Parts
                typeof(TaleInfoPartSeeder).GetTypeInfo().Assembly,
            };
            TagAttributeToTypeMap map = new();
            map.Add(seedAssemblies);

            // build the container for seeders
            Container container = new Container();
            PartSeederFactory.ConfigureServices(
                container,
                new StandardPartTypeProvider(map),
                seedAssemblies);

            container.Verify();

            // load seed configuration
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddInMemoryJson(profile);
            var configuration = builder.Build();

            return new PartSeederFactory(container, configuration);
        }
    }
}
