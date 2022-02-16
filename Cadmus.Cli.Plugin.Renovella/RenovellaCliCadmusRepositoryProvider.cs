using Cadmus.Cli.Core;
using Cadmus.Core;
using Cadmus.Core.Config;
using Cadmus.Core.Storage;
using Cadmus.General.Parts;
using Cadmus.Mongo;
using Cadmus.Renovella.Parts;
using Fusi.Tools.Config;
using System.Reflection;

namespace Cadmus.Cli.Plugin.Renovella
{
    /// <summary>
    /// CLI Cadmus repository provider for Renovella.
    /// Tag: <c>cli-repository-provider.renovella</c>.
    /// </summary>
    /// <seealso cref="ICliCadmusRepositoryProvider" />
    [Tag("cli-repository-provider.renovella")]
    public sealed class RenovellaCliCadmusRepositoryProvider :
        ICliCadmusRepositoryProvider
    {
        private readonly IPartTypeProvider _partTypeProvider;

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        public string? ConnectionString { get; set; }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="RenovellaCliCadmusRepositoryProvider"/> class.
        /// </summary>
        public RenovellaCliCadmusRepositoryProvider()
        {
            TagAttributeToTypeMap _map = new();
            _map.Add(new[]
            {
                // Cadmus.General.Parts
                typeof(NotePart).GetTypeInfo().Assembly,
                // Cadmus.Renovella.Parts
                typeof(TaleInfoPart).GetTypeInfo().Assembly,
            });

            _partTypeProvider = new StandardPartTypeProvider(_map);
        }

        /// <summary>
        /// Creates the repository.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <returns>Repository.</returns>
        /// <exception cref="ArgumentNullException">database</exception>
        public ICadmusRepository CreateRepository(string database)
        {
            if (database == null)
                throw new ArgumentNullException(nameof(database));

            // create the repository (no need to use container here)
            MongoCadmusRepository repository =
                new(_partTypeProvider, new StandardItemSortKeyBuilder());

            repository.Configure(new MongoCadmusRepositoryOptions
            {
                ConnectionString = string.Format(ConnectionString!, database)
            });

            return repository;
        }
    }
}