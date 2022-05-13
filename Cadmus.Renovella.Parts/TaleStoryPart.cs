using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cadmus.Core;
using Fusi.Antiquity.Chronology;
using Fusi.Tools.Config;

namespace Cadmus.Renovella.Parts
{
    /// <summary>
    /// Tale's story part.
    /// <para>Tag: <c>it.vedph.renovella.tale-story</c>.</para>
    /// </summary>
    [Tag("it.vedph.renovella.tale-story")]
    public sealed class TaleStoryPart : PartBase
    {
        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the prologue.
        /// </summary>
        public string Prologue { get; set; }

        /// <summary>
        /// Gets or sets the epilogue.
        /// </summary>
        public string Epilogue { get; set; }

        /// <summary>
        /// Gets or sets the characters.
        /// </summary>
        public List<StoryCharacter> Characters { get; set; }

        /// <summary>
        /// Gets or sets the optional generic age indication.
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// Gets or sets the date when the story is set in.
        /// </summary>
        public HistoricalDate Date { get; set; }

        /// <summary>
        /// Gets or sets the place(s) where the story is set in.
        /// </summary>
        public List<StoryPlace> Places { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaleStoryPart"/> class.
        /// </summary>
        public TaleStoryPart()
        {
            Characters = new List<StoryCharacter>();
            Places = new List<StoryPlace>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new(
                new StandardDataPinTextFilter());

            builder.Set("character", Characters?.Count ?? 0, false);

            if (Characters?.Count > 0)
            {
                builder.AddValues("character-name",
                    Characters.Select(c => c.Name).Distinct(), filter: true);
                builder.AddValues("character-role",
                    Characters.Select(c => c.Role).Distinct());
            }

            if (!string.IsNullOrEmpty(Age)) builder.AddValue("age", Age);

            if (Date != null)
                builder.AddValue("date-value", Date.GetSortValue());

            if (Places?.Count > 0)
            {
                builder.AddValues("place-type", Places.Select(p => p.Type)
                    .Distinct());
                builder.AddValues("place-name",
                    Places.Select(p => p.Name).Distinct(),
                    filter: true);
            }

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
                new DataPinDefinition(DataPinValueType.Integer,
                    "character-count",
                    "The total count of characters in the story."),
                new DataPinDefinition(DataPinValueType.String,
                    "character-name",
                    "Each unique character name in the story.",
                    "MF"),
                new DataPinDefinition(DataPinValueType.String,
                    "character-role",
                    "Each unique character role in the story.",
                    "MF"),
                new DataPinDefinition(DataPinValueType.String,
                    "age",
                    "The story's age."),
                new DataPinDefinition(DataPinValueType.Decimal,
                    "date-value",
                    "The story's date value."),
                new DataPinDefinition(DataPinValueType.String,
                    "place-type",
                    "The story's place type(s).",
                    "M"),
                new DataPinDefinition(DataPinValueType.String,
                    "place-name",
                    "The story's place name(s).",
                    "MF"),
                new DataPinDefinition(DataPinValueType.String,
                    "age",
                    "The story's age.")
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
            StringBuilder sb = new();

            sb.Append("[TaleStory] ");

            if (Date != null) sb.Append(Date);
            if (Places?.Count > 0)
                sb.Append(string.Join("; ", Places));

            if (Characters?.Count > 0)
            {
                sb.Append(string.Join("; ", from c in Characters
                                            select c.ToString()));
            }

            return sb.ToString();
        }
    }
}
