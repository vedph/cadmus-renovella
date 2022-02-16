using System;
using System.Reflection;
using Cadmus.Core;
using Cadmus.Core.Config;
using Cadmus.Core.Storage;
using Cadmus.General.Parts;
using Cadmus.Mongo;
using Cadmus.Renovella.Parts;
using Microsoft.Extensions.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Cadmus.Renovella.Services
{
    /// <summary>
    /// Cadmus Re.Novella repository provider.
    /// </summary>
    /// <seealso cref="IRepositoryProvider" />
    public sealed class RenovellaRepositoryProvider : IRepositoryProvider
    {
        private readonly IConfiguration _configuration;
        private readonly TagAttributeToTypeMap _map;
        private readonly IPartTypeProvider _partTypeProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="StandardRepositoryProvider"/>
        /// class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <exception cref="ArgumentNullException">configuration</exception>
        public RenovellaRepositoryProvider(IConfiguration configuration)
        {
            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));

            _map = new TagAttributeToTypeMap();
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
        /// Gets the part type provider.
        /// </summary>
        /// <returns>part type provider</returns>
        public IPartTypeProvider GetPartTypeProvider()
        {
            return _partTypeProvider;
        }

        /// <summary>
        /// Creates a Cadmus repository.
        /// </summary>
        /// <returns>repository</returns>
        public ICadmusRepository CreateRepository()
        {
            // create the repository (no need to use container here)
            MongoCadmusRepository repository =
                new MongoCadmusRepository(
                    _partTypeProvider,
                    new StandardItemSortKeyBuilder());

            repository.Configure(new MongoCadmusRepositoryOptions
            {
                ConnectionString = string.Format(
                    _configuration.GetConnectionString("Default"),
                    _configuration.GetValue<string>("DatabaseNames:Data"))
            });

            return repository;
        }
    }
}
