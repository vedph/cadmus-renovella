using System;
using System.Reflection;
using Cadmus.Core;
using Cadmus.Core.Config;
using Cadmus.Core.Storage;
using Cadmus.General.Parts;
using Cadmus.Mongo;
using Cadmus.Renovella.Parts;
using Fusi.Tools.Config;

namespace Cadmus.Renovella.Services
{
    /// <summary>
    /// Cadmus Re.Novella repository provider.
    /// Tag: <c>repository-provider.renovella</c>.
    /// </summary>
    /// <seealso cref="IRepositoryProvider" />
    [Tag("repository-provider.renovella")]
    public sealed class RenovellaRepositoryProvider : IRepositoryProvider
    {
        private readonly IPartTypeProvider _partTypeProvider;

        /// <summary>
        /// The connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StandardRepositoryProvider"/>
        /// class.
        /// </summary>
        /// <exception cref="ArgumentNullException">configuration</exception>
        public RenovellaRepositoryProvider()
        {
            ConnectionString = "";
            TagAttributeToTypeMap map = new();
            map.Add(new[]
            {
                // Cadmus.General.Parts
                typeof(NotePart).GetTypeInfo().Assembly,
                // Cadmus.Renovella.Parts
                typeof(TaleInfoPart).GetTypeInfo().Assembly,
            });

            _partTypeProvider = new StandardPartTypeProvider(map);
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
            MongoCadmusRepository repository = new(_partTypeProvider,
                    new StandardItemSortKeyBuilder());

            repository.Configure(new MongoCadmusRepositoryOptions
            {
                ConnectionString = ConnectionString ??
                throw new InvalidOperationException(
                    "No connection string set for IRepositoryProvider implementation")
            });

            return repository;
        }
    }
}
