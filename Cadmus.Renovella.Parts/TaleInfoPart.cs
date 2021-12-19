using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Fusi.Antiquity.Chronology;
using Fusi.Tools.Config;

namespace Cadmus.Renovella.Parts
{
    /// <summary>
    /// Tale info part.
    /// <para>Tag: <c>it.vedph.renovella.tale-info</c>.</para>
    /// </summary>
    [Tag("it.vedph.renovella.tale-info")]
    public sealed class TaleInfoPart : PartBase
    {
        /// <summary>
        /// Gets or sets the human-readable ID of the collection. Empty or null
        /// if this is not a collection.
        /// </summary>
        public string CollectionId { get; set; }

        /// <summary>
        /// Gets or sets the container identifier, i.e. the ID of the collection
        /// this tale belongs to.
        /// </summary>
        public string ContainerId { get; set; }

        /// <summary>
        /// Gets or sets the ordinal number of this tale in its collection, if
        /// any.
        /// </summary>
        public short Ordinal { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        public CitedPerson Author { get; set; }

        /// <summary>
        /// Gets or sets the dedicatee.
        /// </summary>
        public CitedPerson Dedicatee { get; set; }

        /// <summary>
        /// Gets or sets the place of composition.
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Gets or sets the date of composition.
        /// </summary>
        public HistoricalDate Date { get; set; }

        /// <summary>
        /// Gets or sets the language of this tale.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the genre(s) this tale belongs to.
        /// </summary>
        public List<string> Genres { get; set; }

        /// <summary>
        /// Gets or sets the structure.
        /// </summary>
        public string Structure { get; set; }

        /// <summary>
        /// Gets or sets the rubric.
        /// </summary>
        public string Rubric { get; set; }

        /// <summary>
        /// Gets or sets the incipit.
        /// </summary>
        public string Incipit { get; set; }

        /// <summary>
        /// Gets or sets the explicit.
        /// </summary>
        public string Explicit { get; set; }

        /// <summary>
        /// Gets or sets the narrator of this tale.
        /// </summary>
        public string Narrator { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaleInfoPart"/> class.
        /// </summary>
        public TaleInfoPart()
        {
            Genres = new List<string>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item = null)
        {
            DataPinBuilder builder = new DataPinBuilder(
                new StandardDataPinTextFilter());

            if (!string.IsNullOrEmpty(CollectionId))
                builder.AddValue("collectionId", CollectionId);
            if (!string.IsNullOrEmpty(ContainerId))
                builder.AddValue("containerId", ContainerId);
            if (!string.IsNullOrEmpty(Title))
                builder.AddValue("title", Title, filter: true);
            if (Author?.Name != null)
                builder.AddValue("author", Author.Name.GetFullName(), filter: true);
            if (!string.IsNullOrEmpty(Place))
                builder.AddValue("place", Place, filter: true);
            if (Date != null)
                builder.AddValue("date-value", Date.GetSortValue());
            if (!string.IsNullOrEmpty(Language))
                builder.AddValue("language", Language);
            if (Genres?.Count > 0)
                builder.AddValues("genre", Genres);
            if (!string.IsNullOrEmpty(Structure))
                builder.AddValue("structure", Structure);
            if (Dedicatee?.Name != null)
            {
                builder.AddValue("dedicatee", Dedicatee.Name.GetFullName(),
                    filter: true);
            }
            if (!string.IsNullOrEmpty(Narrator))
                builder.AddValue("narrator", Narrator, filter: true);

            return builder.Build(this);
        }

        /// <summary>
        /// Gets the definitions of data pins used by the implementor.
        /// </summary>
        /// <returns>Data pins definitions.</returns>
        public override IList<DataPinDefinition> GetDataPinDefinitions()
        {
            return new List<DataPinDefinition>(new[]
            {
                 new DataPinDefinition(DataPinValueType.String,
                    "collectionId",
                    "The ID of the collection represented by this part."),
                 new DataPinDefinition(DataPinValueType.String,
                    "containerId",
                    "The ID of the collection containing this part."),
                 new DataPinDefinition(DataPinValueType.String,
                    "title",
                    "The tale's title.",
                    "F"),
                 new DataPinDefinition(DataPinValueType.String,
                    "author",
                    "The tale's author full name.",
                    "F"),
                 new DataPinDefinition(DataPinValueType.String,
                    "place",
                    "The tale's composition place.",
                    "F"),
                 new DataPinDefinition(DataPinValueType.Decimal,
                    "date-value",
                    "The tale's composition date value."),
                 new DataPinDefinition(DataPinValueType.String,
                    "language",
                    "The tale's language."),
                 new DataPinDefinition(DataPinValueType.String,
                    "genre",
                    "The tale's genre.",
                    "M"),
                 new DataPinDefinition(DataPinValueType.String,
                    "structure",
                    "The tale's structure."),
                 new DataPinDefinition(DataPinValueType.String,
                    "dedicatee",
                    "The tale's dedicatee full name.",
                    "F"),
                 new DataPinDefinition(DataPinValueType.String,
                    "narrator",
                    "The tale's narrator if any.",
                    "F")
            });
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[TaleInfo]");

            if (Author?.Name != null)
                sb.Append(Author.Name.GetFullName()).Append(", ");
            if (!string.IsNullOrEmpty(Title)) sb.Append(Title);
            if (!string.IsNullOrEmpty(ContainerId))
            {
                sb.Append(" (").Append(ContainerId)
                    .Append(" - ").Append(Ordinal).Append(')');
            }

            return sb.ToString();
        }
    }
}